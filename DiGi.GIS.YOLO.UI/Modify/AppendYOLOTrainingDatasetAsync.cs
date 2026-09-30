using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.WebAPI.Classes;
using DiGi.GIS.YOLO.UI.Classes;
using DiGi.GIS.YOLO.UI.Enums;
using DiGi.WebAPI.Classes;
using DiGi.YOLO.Classes;
using DiGi.YOLO.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Builds a YOLO training dataset of every labelled building of the named county parts from the deployed data: labels from the building data, footprints and orthophotos from their tables, read through the Web API.
        /// <para><b>Labels.</b> The stored <c>User year built</c> column (<see cref="Query.UserYearBuiltsAsync"/>) - the same exact user year the Year Built regressor trains on. A building without one is not in the dataset. A reference filed under several named county parts is built once, under the lowest identifier.</para>
        /// <para><b>Split.</b> A reference <see cref="GIS.IO.Query.Holdout(string?, int)"/> holds out goes to Test and only there - the same buildings the regressor holds out. The rest, sorted ordinally, are split by a seeded draw into Validate (<see cref="YOLOTrainingDatasetOptions.ValidateWeight"/>) and Train, so the same labels and seed give the same split.</para>
        /// <para><b>Images.</b> One per orthophoto year - the first of a year in date order - saved by <see cref="SavePredictionImage"/>, the encoder the inference export uses, as <c>{reference}_{year}.jpeg</c>. A year at or after the label carries one box of class <c>Building</c> (index 0): the footprint&apos;s bounding box grown by <see cref="YOLOTrainingDatasetOptions.Offset"/>, projected onto the image and clamped to it (<see cref="Query.PixelBoundingBox"/>). A year before the label is kept with an empty label file - it teaches the boundary. A positive year whose box has no area inside the image is not written at all. Identical photo bytes under two years are kept once when their labels agree and dropped when they conflict.</para>
        /// <para><b>Legacy.</b> Each Test building is checked against the two records of what the lost <c>train8</c> dataset held (<see cref="Query.LegacySource"/>), so the detector evaluation can report a subset <c>train8</c> cannot have seen. A county part whose stored history cannot be read in full has the decision refused for all of its Test buildings - they are <see cref="LegacySource.Unknown"/>, never clean.</para>
        /// <para><b>Resume.</b> The <c>dataset_references.tsv</c> manifest is a journal: a building&apos;s images and label files are written first and its row appended after, so a building the manifest names is complete and is skipped, and the files of one it does not name - left half-written by a stopped run - are removed and rebuilt. <c>conf.yaml</c> is written before the first building and again at the end.</para>
        /// <para><b>CountOnly</b> stops before any orthophoto is requested and reports, per county part, the labelled buildings, the split, the Legacy agreement table and bounded entries of the Test buildings, the cross-part reference duplicates and an estimate of a build&apos;s size.</para>
        /// <para>A building that fails is logged and stepped over; a county part whose labels cannot be read is refused while the others are built. <see cref="YOLOTrainingDatasetResult.FailedStepNames"/> is what says whether the run did everything it set out to do.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="yOLOTrainingDatasetOptions">The options describing the dataset.</param>
        /// <param name="progress">An optional progress reporter carrying the running total of buildings the run has carried through.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task returning what the run did, or null when it could not be attempted at all - no manager, no options, no county named, or no absolute output directory.</returns>
        [SupportedOSPlatform("windows")]
        public static async Task<YOLOTrainingDatasetResult?> AppendYOLOTrainingDatasetAsync(this GISWebAPIManager? gisWebAPIManager, YOLOTrainingDatasetOptions? yOLOTrainingDatasetOptions, IProgress<long>? progress = null, CancellationToken cancellationToken = default)
        {
            if (gisWebAPIManager is null || yOLOTrainingDatasetOptions is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: no {Manager} or no options - nothing can be read", nameof(AppendYOLOTrainingDatasetAsync), nameof(GISWebAPIManager));
                return null;
            }

            List<int> countyIds = yOLOTrainingDatasetOptions.CountyIds is null ? [] : [.. yOLOTrainingDatasetOptions.CountyIds.Where(x => x > 0).Distinct().OrderBy(x => x)];
            if (countyIds.Count == 0)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: no county was named - the dataset cannot be scoped", nameof(AppendYOLOTrainingDatasetAsync));
                return null;
            }

            string? outputDirectory = yOLOTrainingDatasetOptions.OutputDirectory;
            if (string.IsNullOrWhiteSpace(outputDirectory) || !Path.IsPathRooted(outputDirectory))
            {
                // conf.yaml carries the dataset path, and ultralytics resolves a relative one against its own
                // datasets folder - an absolute root is the only kind that means the same thing to every reader.
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: the output directory must be an absolute path - {OutputDirectory}", nameof(AppendYOLOTrainingDatasetAsync), outputDirectory ?? string.Empty);
                return null;
            }

            outputDirectory = Path.GetFullPath(outputDirectory);

            bool countOnly = yOLOTrainingDatasetOptions.CountOnly;
            int referenceBatchSize = yOLOTrainingDatasetOptions.ReferenceBatchSize < 1 ? 1 : Math.Min(yOLOTrainingDatasetOptions.ReferenceBatchSize, Constants.Count.BuildingDataReference_Maximum);
            int maxConcurrentRequests = yOLOTrainingDatasetOptions.MaxConcurrentRequests < 1 ? 1 : yOLOTrainingDatasetOptions.MaxConcurrentRequests;
            int holdoutDenominator = yOLOTrainingDatasetOptions.HoldoutDenominator;
            double offset = yOLOTrainingDatasetOptions.Offset;
            DateTimeOffset legacyCutoff = yOLOTrainingDatasetOptions.LegacyCutoff;

            DateTimeOffset start = DateTimeOffset.Now;
            bool cancelled = false;

            List<string> failedStepNames = [];
            List<string> messages = [];

            Dictionary<int, YOLOTrainingDatasetCount> yOLOTrainingDatasetCounts = [];
            foreach (int countyId in countyIds)
            {
                yOLOTrainingDatasetCounts[countyId] = new YOLOTrainingDatasetCount(countyId);
            }

            void Fail(string stepName, string message)
            {
                if (!failedStepNames.Contains(stepName))
                {
                    failedStepNames.Add(stepName);
                }

                messages.Add(message);

                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "{Method}: step {Step} did not complete - {Message}", nameof(AppendYOLOTrainingDatasetAsync), stepName, message);
            }

            YOLOTrainingDatasetResult Result()
            {
                YOLOTrainingDatasetCount total = new((int?)null);
                foreach (YOLOTrainingDatasetCount yOLOTrainingDatasetCount in yOLOTrainingDatasetCounts.Values)
                {
                    total.Add(yOLOTrainingDatasetCount);
                }

                YOLOTrainingDatasetResult result = new(countyIds, outputDirectory, countOnly, total, yOLOTrainingDatasetCounts.Values.OrderBy(x => x.CountyId), failedStepNames, messages, start, DateTimeOffset.Now, cancelled);

                Serilog.Modify.Log("{Method} finished: {BuildingCount} buildings, {ImageCount} images, {FailedStepCount} failed step(s), cancelled {Cancelled}", nameof(AppendYOLOTrainingDatasetAsync), total.BuildingCount, total.ImageCount, failedStepNames.Count, cancelled);

                return result;
            }

            // One item read is small, but a page of labels, footprints or history is sized against the server's
            // command timeout. Sixty seconds is the whole budget there is: the manager's HttpClient times out there.
            PostOptions postOptions_Item = new() { RequestResult = true };
            PostOptions postOptions_Bulk = new() { RequestResult = true, Delay = TimeSpan.FromSeconds(60) };
            // The cross-part reference duplicates is a global read - no county filter - sized against the server's 600 s
            // commandtimeout, so it gets its own longer budget rather than the 60 s bulk page budget.
            PostOptions postOptions_ReferenceDuplicates = new() { RequestResult = true, Delay = TimeSpan.FromSeconds(600) };

            Serilog.Modify.Log("{Method} started: {CountyCount} county part(s) {CountyIds}, output {OutputDirectory}, count only {CountOnly}, resume {Resume}", nameof(AppendYOLOTrainingDatasetAsync), countyIds.Count, string.Join(", ", countyIds), outputDirectory, countOnly, yOLOTrainingDatasetOptions.Resume);

            // The scope is checked first: an identifier that is no county row - most often a county code - matches no
            // building, and every tally below would then report a legitimate zero.
            List<PostgreSQL.Classes.AdministrativeAreal2DReference>? administrativeAreal2DReferences = await Query.CountyReferencesAsync(gisWebAPIManager, postOptions_Item);
            if (administrativeAreal2DReferences is null)
            {
                messages.Add("The county rows could not be read, so the scope could not be checked.");
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "{Method}: the county rows could not be read - the scope was not checked", nameof(AppendYOLOTrainingDatasetAsync));
            }
            else
            {
                Dictionary<int, List<int>> countyIds_Unknown = Query.UnknownCountyIds(administrativeAreal2DReferences, countyIds);
                if (countyIds_Unknown.Count != 0)
                {
                    foreach (KeyValuePair<int, List<int>> keyValuePair in countyIds_Unknown)
                    {
                        string message = keyValuePair.Value.Count == 0
                            ? string.Format(CultureInfo.InvariantCulture, "County {0} is not a county row. A county is named by its identifier, never by its four character code.", keyValuePair.Key)
                            : string.Format(CultureInfo.InvariantCulture, "County {0} is not a county row - it is the code of a county held as {1} polygon part(s), whose identifiers are {2}. A county is named by its identifier, never by its code.", keyValuePair.Key, keyValuePair.Value.Count, string.Join(", ", keyValuePair.Value));

                        Fail(nameof(Query.UnknownCountyIds), message);
                    }

                    return Result();
                }
            }

            // Refused, never defaulted: an empty list would report every held-out building as clean.
            string? path_LegacyReferences = Query.ModelPath(yOLOTrainingDatasetOptions.LegacyReferencesFilePath);
            HashSet<string>? legacyReferences = Query.LegacyReferences(path_LegacyReferences);
            if (legacyReferences is null)
            {
                Fail(nameof(Query.LegacyReferences), string.Format(CultureInfo.InvariantCulture, "The legacy reference list could not be read - {0}. Copy Data_2025.05.27.tsv from DiGi.GIS.ML/Data and name it in LegacyReferencesFilePath.", path_LegacyReferences ?? yOLOTrainingDatasetOptions.LegacyReferencesFilePath ?? "no path was named"));
                return Result();
            }

            string path_Configuration = Path.Combine(outputDirectory, DiGi.YOLO.Constants.FileName.Conf);
            string path_DatasetReferences = Path.Combine(outputDirectory, Constants.FileName.DatasetReferences);

            // The journal of an existing dataset. Read before the labels, so the history of a building that is
            // already complete is not read again.
            Dictionary<string, DatasetReference> datasetReferences_Journal = new(StringComparer.Ordinal);
            if (!countOnly && Directory.Exists(outputDirectory) && Directory.EnumerateFileSystemEntries(outputDirectory).Any())
            {
                bool exists_Configuration = File.Exists(path_Configuration);
                bool exists_DatasetReferences = File.Exists(path_DatasetReferences);

                if (!exists_Configuration || !exists_DatasetReferences)
                {
                    // A folder that holds something other than a dataset this builder started is not written into:
                    // a stray conf.yaml or image would be taken as part of the dataset.
                    Fail(nameof(YOLOTrainingDatasetOptions.OutputDirectory), string.Format(CultureInfo.InvariantCulture, "The output directory {0} is not empty and is not a dataset this builder started (conf.yaml {1}, {2} {3}). Name an empty or new directory.", outputDirectory, exists_Configuration ? "present" : "missing", Constants.FileName.DatasetReferences, exists_DatasetReferences ? "present" : "missing"));
                    return Result();
                }

                if (!yOLOTrainingDatasetOptions.Resume)
                {
                    Fail(nameof(YOLOTrainingDatasetOptions.Resume), string.Format(CultureInfo.InvariantCulture, "The output directory {0} already holds a dataset and Resume is off. Turn Resume on to continue it, or name a new directory.", outputDirectory));
                    return Result();
                }

                List<DatasetReference>? datasetReferences = Query.DatasetReferences(path_DatasetReferences);
                if (datasetReferences is null)
                {
                    Fail(nameof(Query.DatasetReferences), string.Format(CultureInfo.InvariantCulture, "The dataset manifest {0} could not be read.", path_DatasetReferences));
                    return Result();
                }

                foreach (DatasetReference datasetReference in datasetReferences)
                {
                    datasetReferences_Journal[datasetReference.Reference!] = datasetReference;
                }
            }

            // Labels, per county part in ascending identifier order. A reference found under a second part is dropped
            // there: it is one building, and built twice it could land in two splits.
            Dictionary<string, (int CountyId, short Label)> labels = new(StringComparer.Ordinal);
            List<int> countyIds_Labelled = [];
            foreach (int countyId in countyIds)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Dictionary<string, short>? years = await Query.UserYearBuiltsAsync(gisWebAPIManager, countyId, referenceBatchSize, postOptions_Bulk, cancellationToken);
                if (years is null)
                {
                    Fail(nameof(Query.UserYearBuiltsAsync), string.Format(CultureInfo.InvariantCulture, "The labels of county {0} could not be read in full - the county is not built.", countyId));
                    continue;
                }

                countyIds_Labelled.Add(countyId);

                YOLOTrainingDatasetCount yOLOTrainingDatasetCount = yOLOTrainingDatasetCounts[countyId];
                yOLOTrainingDatasetCount.LabelledCount += years.Count;

                foreach (KeyValuePair<string, short> keyValuePair in years)
                {
                    if (labels.TryGetValue(keyValuePair.Key, out (int CountyId, short Label) label))
                    {
                        yOLOTrainingDatasetCount.DuplicateReferenceCount++;
                        if (label.Label != keyValuePair.Value)
                        {
                            yOLOTrainingDatasetCount.LabelConflictCount++;
                        }

                        continue;
                    }

                    labels[keyValuePair.Key] = (countyId, keyValuePair.Value);
                }
            }

            // The split. Holdout is a hash of the reference, so it is the same whatever else is in the dataset; the
            // Train / Validate draw runs over the ordinally sorted rest, one draw per reference.
            Dictionary<string, Category> categories = new(StringComparer.Ordinal);
            List<string> references_Sorted = [.. labels.Keys.OrderBy(x => x, StringComparer.Ordinal)];
            Random random = new(yOLOTrainingDatasetOptions.Seed);
            foreach (string reference in references_Sorted)
            {
                if (GIS.IO.Query.Holdout(reference, holdoutDenominator))
                {
                    categories[reference] = Category.Test;
                    continue;
                }

                categories[reference] = random.NextDouble() < yOLOTrainingDatasetOptions.ValidateWeight ? Category.Validate : Category.Train;
            }

            foreach (string reference in references_Sorted)
            {
                YOLOTrainingDatasetCount yOLOTrainingDatasetCount = yOLOTrainingDatasetCounts[labels[reference].CountyId];
                yOLOTrainingDatasetCount.BuildingCount++;

                switch (categories[reference])
                {
                    case Category.Train:
                        yOLOTrainingDatasetCount.TrainCount++;
                        break;

                    case Category.Validate:
                        yOLOTrainingDatasetCount.ValidateCount++;
                        break;

                    case Category.Test:
                        yOLOTrainingDatasetCount.TestCount++;
                        break;
                }
            }

            // Legacy, for the Test buildings. A Train or Validate building's decision feeds no metric, so it carries the
            // list only and its history is not read. A building the journal already holds keeps the decision it was
            // written with.
            Dictionary<string, LegacySource> legacySources = new(StringComparer.Ordinal);
            foreach (int countyId in countyIds_Labelled)
            {
                cancellationToken.ThrowIfCancellationRequested();

                List<string> references_Test = [];
                foreach (string reference in references_Sorted)
                {
                    if (labels[reference].CountyId != countyId)
                    {
                        continue;
                    }

                    if (datasetReferences_Journal.TryGetValue(reference, out DatasetReference? datasetReference) && datasetReference is not null)
                    {
                        legacySources[reference] = datasetReference.LegacySource;
                        continue;
                    }

                    if (categories[reference] != Category.Test)
                    {
                        legacySources[reference] = legacyReferences.Contains(reference) ? LegacySource.Tsv : LegacySource.None;
                        continue;
                    }

                    references_Test.Add(reference);
                }

                if (references_Test.Count == 0)
                {
                    continue;
                }

                List<string> references_Failed = [];
                Dictionary<string, List<YearBuiltData>>? yearBuiltDatas_ByReference = await Query.StoredYearBuiltDatasAsync(gisWebAPIManager, countyId, references_Test, Constants.Count.YearBuiltDataReference_Maximum, references_Failed, postOptions_Bulk, cancellationToken);

                if (yearBuiltDatas_ByReference is null || references_Failed.Count != 0)
                {
                    // Refused for the whole county part rather than for the failed page only: a part reported as partly
                    // clean on a partial read is exactly the result this rule exists to prevent.
                    Fail(nameof(Query.StoredYearBuiltDatasAsync), string.Format(CultureInfo.InvariantCulture, "The stored history of county {0} could not be read in full - its {1} Test building(s) are Legacy {2}, never clean.", countyId, references_Test.Count, Core.Query.Description(LegacySource.Unknown)));

                    foreach (string reference in references_Test)
                    {
                        legacySources[reference] = LegacySource.Unknown;
                    }

                    continue;
                }

                YOLOTrainingDatasetCount yOLOTrainingDatasetCount = yOLOTrainingDatasetCounts[countyId];
                foreach (string reference in references_Test)
                {
                    yearBuiltDatas_ByReference.TryGetValue(reference, out List<YearBuiltData>? yearBuiltDatas);

                    legacySources[reference] = Query.LegacySource(legacyReferences, reference, yearBuiltDatas, legacyCutoff);

                    if (yearBuiltDatas is not null && yearBuiltDatas.Any(x => x?.GetUserYearBuilt() is UserYearBuilt userYearBuilt && userYearBuilt.YearBuiltRelation != GIS.Enums.YearBuiltRelation.Exact))
                    {
                        yOLOTrainingDatasetCount.BoundedEntryCount++;
                    }
                }
            }

            foreach (string reference in references_Sorted)
            {
                if (categories[reference] != Category.Test || !legacySources.TryGetValue(reference, out LegacySource legacySource))
                {
                    continue;
                }

                YOLOTrainingDatasetCount yOLOTrainingDatasetCount = yOLOTrainingDatasetCounts[labels[reference].CountyId];
                switch (legacySource)
                {
                    case LegacySource.None:
                        yOLOTrainingDatasetCount.LegacyNoneCount++;
                        break;

                    case LegacySource.Tsv:
                        yOLOTrainingDatasetCount.LegacyTsvCount++;
                        break;

                    case LegacySource.Timestamp:
                        yOLOTrainingDatasetCount.LegacyTimestampCount++;
                        break;

                    case LegacySource.Both:
                        yOLOTrainingDatasetCount.LegacyBothCount++;
                        break;

                    case LegacySource.Unknown:
                        yOLOTrainingDatasetCount.LegacyUnknownCount++;
                        break;
                }
            }

            if (countOnly)
            {
                // Global endpoint: read once, filtered to the parts of this run.
                List<PostgreSQL.Classes.Building2DReferenceDuplicate>? building2DReferenceDuplicates = await Query.ReferenceDuplicatesAsync(gisWebAPIManager, yOLOTrainingDatasetOptions.ReferenceDuplicateLimit, postOptions_ReferenceDuplicates, cancellationToken);
                if (building2DReferenceDuplicates is null)
                {
                    Fail(nameof(Query.ReferenceDuplicatesAsync), "The cross-part reference duplicates could not be read.");
                }
                else
                {
                    foreach (PostgreSQL.Classes.Building2DReferenceDuplicate building2DReferenceDuplicate in building2DReferenceDuplicates)
                    {
                        List<int>? countyIds_Duplicate = building2DReferenceDuplicate?.CountyIds;
                        if (countyIds_Duplicate is null)
                        {
                            continue;
                        }

                        foreach (int countyId in countyIds_Duplicate.Distinct())
                        {
                            if (yOLOTrainingDatasetCounts.TryGetValue(countyId, out YOLOTrainingDatasetCount? yOLOTrainingDatasetCount) && yOLOTrainingDatasetCount is not null)
                            {
                                yOLOTrainingDatasetCount.ReferenceDuplicateCount++;
                            }
                        }
                    }

                    if (building2DReferenceDuplicates.Count >= yOLOTrainingDatasetOptions.ReferenceDuplicateLimit)
                    {
                        messages.Add(string.Format(CultureInfo.InvariantCulture, "The cross-part reference duplicates reached the limit of {0} rows - the counts are a lower bound. Raise ReferenceDuplicateLimit.", yOLOTrainingDatasetOptions.ReferenceDuplicateLimit));
                    }
                }

                foreach (YOLOTrainingDatasetCount yOLOTrainingDatasetCount in yOLOTrainingDatasetCounts.Values)
                {
                    long buildingCount = yOLOTrainingDatasetCount.BuildingCount;

                    yOLOTrainingDatasetCount.EstimatedRequestCount = buildingCount + ((buildingCount + referenceBatchSize - 1) / referenceBatchSize);
                    yOLOTrainingDatasetCount.EstimatedImageCount = buildingCount * Constants.Count.ImagePerBuilding_Estimate;
                    yOLOTrainingDatasetCount.EstimatedByteCount = yOLOTrainingDatasetCount.EstimatedImageCount * Constants.Count.ImageByte_Estimate;
                }

                return Result();
            }

            // The build.
            Directory.CreateDirectory(outputDirectory);

            bool exists_Dataset = File.Exists(path_Configuration);

            // The files of a building the journal does not name were left by a stopped run. They are removed before the
            // dataset is read back, so they are neither registered nor mistaken for a complete building.
            if (exists_Dataset)
            {
                YOLOModel yOLOModel_Directories = new(outputDirectory);
                foreach (Category category in Enum.GetValues(typeof(Category)))
                {
                    string? directory_Images = yOLOModel_Directories.GetDirectory_Images(category);
                    string? directory_Labels = yOLOModel_Directories.GetDirectory_Labels(category);
                    if (string.IsNullOrWhiteSpace(directory_Images) || !Directory.Exists(directory_Images))
                    {
                        continue;
                    }

                    foreach (string path_Image in Directory.GetFiles(directory_Images, "*.jpeg"))
                    {
                        if (Query.TryParseImageFileName(path_Image, out string? reference, out _) && datasetReferences_Journal.ContainsKey(reference!))
                        {
                            continue;
                        }

                        File.Delete(path_Image);

                        if (!string.IsNullOrWhiteSpace(directory_Labels))
                        {
                            string path_Label = Path.ChangeExtension(Path.Combine(directory_Labels, Path.GetFileName(path_Image)), ".txt");
                            if (File.Exists(path_Label))
                            {
                                File.Delete(path_Label);
                            }
                        }
                    }
                }
            }

            YOLOModel? yOLOModel = exists_Dataset ? DiGi.YOLO.Modify.Read(path_Configuration) : new YOLOModel(outputDirectory);
            if (yOLOModel is null)
            {
                Fail(nameof(DiGi.YOLO.Modify.Read), string.Format(CultureInfo.InvariantCulture, "The existing dataset {0} could not be read.", path_Configuration));
                return Result();
            }

            yOLOModel.Add(Constants.LabelName.Building);
            if (yOLOModel.LabelIndex(Constants.LabelName.Building) != 0)
            {
                // The production weights report the building as class 0; a dataset that files it under another index
                // would train a detector whose output the pipeline misreads.
                Fail(nameof(Constants.LabelName), string.Format(CultureInfo.InvariantCulture, "The dataset {0} does not name {1} as class 0.", path_Configuration, Constants.LabelName.Building));
                return Result();
            }

            if (!exists_Dataset)
            {
                if (!DiGi.YOLO.Modify.Write(yOLOModel))
                {
                    Fail(nameof(DiGi.YOLO.Modify.Write), string.Format(CultureInfo.InvariantCulture, "The dataset could not be started in {0}.", outputDirectory));
                    return Result();
                }
            }

            if (!File.Exists(path_DatasetReferences))
            {
                File.WriteAllText(path_DatasetReferences, Constants.Header.DatasetReferences + Environment.NewLine);
            }

            Dictionary<Category, string> directories_Images = [];
            Dictionary<Category, string> directories_Labels = [];
            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                string directory_Images = yOLOModel.GetDirectory_Images(category) ?? Path.Combine(outputDirectory, DiGi.YOLO.Constants.DirectoryName.Images, DiGi.YOLO.Query.DirectoryName(category) ?? string.Empty);
                string directory_Labels = yOLOModel.GetDirectory_Labels(category) ?? Path.Combine(outputDirectory, DiGi.YOLO.Constants.DirectoryName.Labels, DiGi.YOLO.Query.DirectoryName(category) ?? string.Empty);

                Directory.CreateDirectory(directory_Images);
                Directory.CreateDirectory(directory_Labels);

                directories_Images[category] = directory_Images;
                directories_Labels[category] = directory_Labels;
            }

            // One building: its orthophotos read, one image per year saved, and its boxes worked out. Runs concurrently
            // with its batch, so it touches nothing shared - the model, the label files and the journal are written on
            // the calling thread once the batch is back.
            async Task<(string Reference, List<(string Path, Category Category, DiGi.YOLO.Classes.BoundingBox? BoundingBox)> Images, YOLOTrainingDatasetCount YOLOTrainingDatasetCount)> Building(string reference, int countyId, Category category, short label, Building2D? building2D)
            {
                YOLOTrainingDatasetCount yOLOTrainingDatasetCount = new(countyId);
                List<(string Path, Category Category, DiGi.YOLO.Classes.BoundingBox? BoundingBox)> images = [];

                BoundingBox2D? boundingBox2D = building2D is null ? null : GIS.Create.Building2DGeometryCalculationResult(building2D)?.BoundingBox;
                if (boundingBox2D is null)
                {
                    yOLOTrainingDatasetCount.WithoutFootprintCount++;
                    return (reference, images, yOLOTrainingDatasetCount);
                }

                GIS.Classes.OrtoDatas? ortoDatas = await Query.OrtoDatasAsync(gisWebAPIManager, countyId, reference, postOptions_Item, cancellationToken);

                // One photo per year: the first of a year in date order, as the inference export keeps it.
                List<(short Year, OrtoData OrtoData, string Hash)> ortoDatas_Year = [];
                if (ortoDatas is not null)
                {
                    HashSet<int> years = [];
                    foreach (OrtoData ortoData in ortoDatas)
                    {
                        if (ortoData?.Bytes is not byte[] bytes || bytes.Length == 0)
                        {
                            continue;
                        }

                        int year = ortoData.DateTime.Year;
                        if (!years.Add(year))
                        {
                            yOLOTrainingDatasetCount.SameYearImageCount++;
                            continue;
                        }

                        ortoDatas_Year.Add(((short)year, ortoData, System.Convert.ToHexString(SHA256.HashData(bytes))));
                    }
                }

                if (ortoDatas_Year.Count == 0)
                {
                    yOLOTrainingDatasetCount.WithoutImageryCount++;
                    return (reference, images, yOLOTrainingDatasetCount);
                }

                // Identical bytes under two years: one photo filed twice. Kept once when the labels agree, dropped when
                // one year is positive and the other negative - the same pixels cannot both hold and lack the building.
                HashSet<short> years_Dropped = [];
                foreach (IGrouping<string, (short Year, OrtoData OrtoData, string Hash)> grouping in ortoDatas_Year.GroupBy(x => x.Hash, StringComparer.Ordinal))
                {
                    List<(short Year, OrtoData OrtoData, string Hash)> tuples = [.. grouping.OrderBy(x => x.Year)];
                    if (tuples.Count < 2)
                    {
                        continue;
                    }

                    bool positive = tuples.Any(x => x.Year >= label);
                    bool negative = tuples.Any(x => x.Year < label);
                    if (positive && negative)
                    {
                        foreach ((short Year, OrtoData OrtoData, string Hash) tuple in tuples)
                        {
                            years_Dropped.Add(tuple.Year);
                        }

                        yOLOTrainingDatasetCount.IdenticalImageDroppedCount += tuples.Count;
                        continue;
                    }

                    for (int i = 1; i < tuples.Count; i++)
                    {
                        years_Dropped.Add(tuples[i].Year);
                    }

                    yOLOTrainingDatasetCount.IdenticalImageMergedCount += tuples.Count - 1;
                }

                try
                {
                    foreach ((short Year, OrtoData OrtoData, string Hash) tuple in ortoDatas_Year)
                    {
                        if (years_Dropped.Contains(tuple.Year))
                        {
                            continue;
                        }

                        cancellationToken.ThrowIfCancellationRequested();

                        string fileName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}.jpeg", reference, tuple.Year);
                        string path = Path.Combine(directories_Images[category], fileName);

                        if (!tuple.OrtoData.SavePredictionImage(path, out int width, out int height))
                        {
                            continue;
                        }

                        if (tuple.Year < label)
                        {
                            images.Add((path, category, null));
                            yOLOTrainingDatasetCount.NegativeImageCount++;
                            yOLOTrainingDatasetCount.ImageCount++;
                            continue;
                        }

                        BoundingBox2D? boundingBox2D_Pixel = Query.PixelBoundingBox(tuple.OrtoData, boundingBox2D, offset, width, height, out bool clamped);
                        DiGi.YOLO.Classes.BoundingBox? boundingBox = boundingBox2D_Pixel is null ? null : DiGi.YOLO.Create.BoundingBox(width, height, boundingBox2D_Pixel.Min.X, boundingBox2D_Pixel.Min.Y, boundingBox2D_Pixel.Width, boundingBox2D_Pixel.Height);
                        if (boundingBox is null)
                        {
                            // A positive year with no box would be registered as background and teach the detector that
                            // the building is absent from a photo it is in, so the image is not kept at all.
                            File.Delete(path);
                            yOLOTrainingDatasetCount.DroppedBoxCount++;
                            continue;
                        }

                        if (clamped)
                        {
                            yOLOTrainingDatasetCount.ClampedBoxCount++;
                        }

                        images.Add((path, category, boundingBox));
                        yOLOTrainingDatasetCount.PositiveImageCount++;
                        yOLOTrainingDatasetCount.ImageCount++;
                    }
                }
                catch
                {
                    // Nothing of a failed building may stay on disk: an image with no label file is read by ultralytics
                    // as background, which for a positive year is a wrong label.
                    foreach ((string Path, Category Category, DiGi.YOLO.Classes.BoundingBox? BoundingBox) image in images)
                    {
                        if (File.Exists(image.Path))
                        {
                            File.Delete(image.Path);
                        }
                    }

                    throw;
                }

                return (reference, images, yOLOTrainingDatasetCount);
            }

            long progressCount = 0;

            try
            {
                foreach (int countyId in countyIds_Labelled)
                {
                    List<string> references_County = [];
                    foreach (string reference in references_Sorted)
                    {
                        if (labels[reference].CountyId != countyId)
                        {
                            continue;
                        }

                        if (datasetReferences_Journal.ContainsKey(reference))
                        {
                            yOLOTrainingDatasetCounts[countyId].ResumedCount++;
                            continue;
                        }

                        references_County.Add(reference);
                    }

                    for (int i = 0; i < references_County.Count; i += referenceBatchSize)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        List<string> references_Page = references_County.GetRange(i, Math.Min(referenceBatchSize, references_County.Count - i));

                        Dictionary<string, Building2D>? building2Ds = await Query.Building2DsAsync(gisWebAPIManager, countyId, references_Page, referenceBatchSize, postOptions_Bulk, cancellationToken);
                        if (building2Ds is null)
                        {
                            Fail(nameof(Query.Building2DsAsync), string.Format(CultureInfo.InvariantCulture, "The footprints of county {0} could not be read.", countyId));
                            break;
                        }

                        for (int j = 0; j < references_Page.Count; j += maxConcurrentRequests)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            List<string> references_Batch = references_Page.GetRange(j, Math.Min(maxConcurrentRequests, references_Page.Count - j));

                            List<Task<(string Reference, List<(string Path, Category Category, DiGi.YOLO.Classes.BoundingBox? BoundingBox)> Images, YOLOTrainingDatasetCount YOLOTrainingDatasetCount)>> tasks = [];
                            foreach (string reference in references_Batch)
                            {
                                building2Ds.TryGetValue(reference, out Building2D? building2D);

                                tasks.Add(Task.Run(() => Building(reference, countyId, categories[reference], labels[reference].Label, building2D), cancellationToken));
                            }

                            try
                            {
                                await Task.WhenAll(tasks);
                            }
                            catch (Exception)
                            {
                                // Each task is looked at below - a failed one is counted and stepped over, and the finished
                                // ones of a cancelled batch are still journaled, so nothing they wrote is left unregistered.
                            }

                            for (int k = 0; k < tasks.Count; k++)
                            {
                                Task<(string Reference, List<(string Path, Category Category, DiGi.YOLO.Classes.BoundingBox? BoundingBox)> Images, YOLOTrainingDatasetCount YOLOTrainingDatasetCount)> task = tasks[k];
                                string reference = references_Batch[k];

                                if (task.Status == TaskStatus.Canceled || (task.Exception?.GetBaseException() is OperationCanceledException && cancellationToken.IsCancellationRequested))
                                {
                                    continue;
                                }

                                if (task.Status != TaskStatus.RanToCompletion)
                                {
                                    yOLOTrainingDatasetCounts[countyId].FailedBuildingCount++;
                                    Serilog.Modify.Log(task.Exception?.GetBaseException() ?? new Exception("The building did not complete"), "{Method}: building {Reference} of county {CountyId} failed and is stepped over", nameof(AppendYOLOTrainingDatasetAsync), reference, countyId);
                                    continue;
                                }

                                (string Reference, List<(string Path, Category Category, DiGi.YOLO.Classes.BoundingBox? BoundingBox)> Images, YOLOTrainingDatasetCount YOLOTrainingDatasetCount) building = task.Result;

                                yOLOTrainingDatasetCounts[countyId].Add(building.YOLOTrainingDatasetCount);

                                if (building.Images.Count == 0)
                                {
                                    // Not journaled: a building with nothing written is not in the dataset, and a later
                                    // run may find the footprint or the imagery this one did not.
                                    continue;
                                }

                                // The label files first, the journal row last: a row in the journal is the promise that
                                // everything of the building is on disk.
                                foreach ((string Path, Category Category, DiGi.YOLO.Classes.BoundingBox? BoundingBox) image in building.Images)
                                {
                                    yOLOModel.Add(image.Path, image.Category);
                                    if (image.BoundingBox is not null)
                                    {
                                        yOLOModel.Add(image.Path, Constants.LabelName.Building, image.BoundingBox);
                                    }

                                    string path_Label = Path.ChangeExtension(Path.Combine(directories_Labels[image.Category], Path.GetFileName(image.Path)), ".txt");
                                    File.WriteAllText(path_Label, yOLOModel.GetLabelFile(image.Path)?.ToString() ?? string.Empty);
                                }

                                DatasetReference datasetReference = new(reference, countyId, categories[reference], labels[reference].Label, legacySources.TryGetValue(reference, out LegacySource legacySource) ? legacySource : LegacySource.Unknown);

                                File.AppendAllText(path_DatasetReferences, Convert.ToTSV(datasetReference) + Environment.NewLine);
                                datasetReferences_Journal[reference] = datasetReference;
                            }

                            progressCount += references_Batch.Count;
                            progress?.Report(progressCount);
                        }
                    }
                }
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                cancelled = true;
            }

            // Written even after a cancellation: every building the journal names is registered, so conf.yaml and the
            // label files stay consistent with it and the next run resumes from there.
            if (!DiGi.YOLO.Modify.Write(yOLOModel))
            {
                Fail(nameof(DiGi.YOLO.Modify.Write), string.Format(CultureInfo.InvariantCulture, "The dataset could not be written to {0}.", outputDirectory));
            }

            return Result();
        }
    }
}
