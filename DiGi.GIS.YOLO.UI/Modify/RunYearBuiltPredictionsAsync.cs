using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.IO.Interfaces;
using DiGi.GIS.YOLO.UI.Classes;
using DiGi.GIS.WebAPI.Classes;
using DiGi.PostgreSQL.Table;
using DiGi.WebAPI.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Versioning;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Runs the Year Built prediction pipeline over the counties named in the options, from the stored orthophoto imagery through to the stored prediction.
        /// <para>Six steps per county: export the imagery, score it with the frozen detector, turn the detections into objects, write them into the building data, read the feature columns back and score them into a construction year, and store that year twice - dated into the year built data, and latest into the building data column.</para>
        /// <para>Each step carries its own flag, so a run can be resumed without repeating the expensive ones, and a first pass over a county can be made harmless by turning the three write steps off. Each step is idempotent: the scratch paths are derived from the county identifier, the detector overwrites its results file rather than appending to it, and a stored year built datum is read back and added to rather than replaced.</para>
        /// <para>Only a building the detector fired on at least once is scored. A building it never fired on carries no per-year confidence series, which is the feature the regressor was built around, so scoring it would be scoring a row of absent features. The consequence is that the run predicts a year for fewer buildings than the file based workflow it replaces, which scored every row of its table - worth knowing before comparing the two reference by reference.</para>
        /// <para>A county that fails is logged and stepped over, so one unreachable county cannot cost the run the counties behind it. The result therefore comes back either way - <see cref="YearBuiltPredictionResult.FailedStepNames"/> is what says whether the run did everything it set out to do.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI. It also carries the key the write steps authorize with.</param>
        /// <param name="yearBuiltPredictor">The regressor that turns building features into a construction year. Required only when the options ask for the scoring step.</param>
        /// <param name="yearBuiltPredictionPipelineOptions">The options describing the run. Null uses the defaults, which name no county and therefore do nothing.</param>
        /// <param name="progress">An optional progress reporter carrying the running total of buildings the run has carried through a step. A building is counted once per step it clears.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task returning what the run did, or null when the run could not be attempted at all - no manager, no county named, or no scratch directory.</returns>
        [SupportedOSPlatform("windows")]
        public static async Task<YearBuiltPredictionResult?> RunYearBuiltPredictionsAsync(this GISWebAPIManager? gisWebAPIManager, IYearBuiltPredictor? yearBuiltPredictor, YearBuiltPredictionPipelineOptions? yearBuiltPredictionPipelineOptions = null, IProgress<long>? progress = null, CancellationToken cancellationToken = default)
        {
            if (gisWebAPIManager is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: no {Manager} - nothing can be read or written", nameof(RunYearBuiltPredictionsAsync), nameof(GISWebAPIManager));
                return null;
            }

            yearBuiltPredictionPipelineOptions ??= new YearBuiltPredictionPipelineOptions();

            List<int> countyIds = yearBuiltPredictionPipelineOptions.CountyIds is null ? [] : [.. yearBuiltPredictionPipelineOptions.CountyIds.Where(x => x > 0).Distinct().OrderBy(x => x)];
            if (countyIds.Count == 0)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: no county was named - the run cannot be scoped", nameof(RunYearBuiltPredictionsAsync));
                return null;
            }

            string? scratchDirectory = yearBuiltPredictionPipelineOptions.ScratchDirectory;
            if (string.IsNullOrWhiteSpace(scratchDirectory))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: no scratch directory - the imagery has nowhere to go", nameof(RunYearBuiltPredictionsAsync));
                return null;
            }

            int batchSize = yearBuiltPredictionPipelineOptions.BatchSize < 1 ? 1 : yearBuiltPredictionPipelineOptions.BatchSize;
            int maxConcurrentRequests = yearBuiltPredictionPipelineOptions.MaxConcurrentRequests < 1 ? 1 : yearBuiltPredictionPipelineOptions.MaxConcurrentRequests;

            // The endpoint refuses more than this many references in one request, so a larger page would fail
            // the whole county rather than just being slower.
            int referenceBatchSize = yearBuiltPredictionPipelineOptions.ReferenceBatchSize;
            referenceBatchSize = referenceBatchSize < 1 ? 1 : Math.Min(referenceBatchSize, Constants.Count.BuildingDataReference_Maximum);

            DateTimeOffset start = DateTimeOffset.Now;

            // One stamp for the whole run. The stored entries are keyed by it, so a stamp taken per building
            // would leave one history entry per building instead of one per run.
            DateTimeOffset runTimestamp = start;

            long buildingCount = 0;
            long buildingDataUpdatedCount = 0;
            long detectionCount = 0;
            long featureRowCount = 0;
            long imageCount = 0;
            long predictionCount = 0;
            long progressCount = 0;
            long yearBuiltDataUpdatedCount = 0;
            bool cancelled = false;

            List<string> failedStepNames = [];
            List<string> messages = [];

            void Fail(string stepName, int countyId)
            {
                if (!failedStepNames.Contains(stepName))
                {
                    failedStepNames.Add(stepName);
                }

                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Year built prediction step {Step} did not complete - county {CountyId}", stepName, countyId);
            }

            YearBuiltPredictionResult Result()
            {
                return new YearBuiltPredictionResult(countyIds, runTimestamp, start, DateTimeOffset.Now, imageCount, detectionCount, buildingCount, featureRowCount, predictionCount, yearBuiltDataUpdatedCount, buildingDataUpdatedCount, failedStepNames, messages, cancelled);
            }

            // One item read is small and twenty seconds of it is plenty, but the bulk reads and writes are sized
            // against a six hundred second server side command timeout - a county of buildings against ninety odd
            // detection columns - and PostOptions.Delay bounds each attempt. Left at its default those requests are
            // cut off by the client rather than by anything the server said.
            // Sixty seconds is the whole budget there is: the manager registers its HttpClient with that timeout,
            // so a larger value here cannot take effect.
            PostOptions postOptions_Item = new() { RequestResult = true };
            PostOptions postOptions_Bulk = new() { RequestResult = true, Delay = TimeSpan.FromSeconds(60) };

            Serilog.Modify.Log(
                "{Method} started: {CountyCount} counties, scratch {ScratchDirectory}, export {ExportImages}, predict {RunPrediction}, score {Score}, write detections {UpdateDetections}, write year built data {UpdateYearBuiltData}, write predicted year {UpdatePredictedYearBuilt}",
                nameof(RunYearBuiltPredictionsAsync), countyIds.Count, scratchDirectory, yearBuiltPredictionPipelineOptions.ExportImages, yearBuiltPredictionPipelineOptions.RunPrediction, yearBuiltPredictionPipelineOptions.Score, yearBuiltPredictionPipelineOptions.UpdateDetections, yearBuiltPredictionPipelineOptions.UpdateYearBuiltData, yearBuiltPredictionPipelineOptions.UpdatePredictedYearBuilt);

            if (yearBuiltPredictionPipelineOptions.RunPrediction)
            {
                DiGi.YOLO.Classes.YOLOEnvironmentResult yOLOEnvironmentResult = DiGi.YOLO.Query.YOLOEnvironmentResult(yearBuiltPredictionPipelineOptions.PythonPath, yearBuiltPredictionPipelineOptions.ModelPath, yearBuiltPredictionPipelineOptions.WorkingDirectory, cancellationToken);
                if (!yOLOEnvironmentResult.Runnable)
                {
                    // Learning this from the preflight rather than from the detector's standard error is the
                    // whole point of it: an unattended run on a machine with no Python otherwise exports a
                    // county of imagery first and fails afterwards.
                    if (yOLOEnvironmentResult.Messages is List<string> messages_Environment)
                    {
                        messages.AddRange(messages_Environment);
                    }

                    failedStepNames.Add(nameof(DiGi.YOLO.Query.YOLOEnvironmentResult));

                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: this machine cannot run the detector - {Messages}", nameof(RunYearBuiltPredictionsAsync), string.Join("; ", yOLOEnvironmentResult.Messages ?? []));

                    return Result();
                }
            }

            Dictionary<int, List<int>> countyIds_Siblings = await Query.SiblingCountyIdsAsync(gisWebAPIManager, countyIds, postOptions_Item);
            if (countyIds_Siblings.Count == 0)
            {
                messages.Add("The county rows could not be read, so each county was written as though it were a single polygon part.");
            }

            List<string> columnUniqueIds = [];
            foreach (Column column in DiGi.GIS.IO.Query.YearBuiltPredictionInputColumns(yearBuiltPredictionPipelineOptions.Years))
            {
                if (column.UniqueId() is string columnUniqueId && !columnUniqueIds.Contains(columnUniqueId))
                {
                    columnUniqueIds.Add(columnUniqueId);
                }
            }

            // The allow-list is features only, and the rows have to be identifiable, so the reference is asked
            // for on top of it. It is an identifier rather than a feature, so it leaks nothing back into the
            // model - unlike the predicted year, which is this pipeline's own output and is never projected.
            if (DiGi.GIS.IO.Constants.Column.Reference.UniqueId() is string columnUniqueId_Reference && !columnUniqueIds.Contains(columnUniqueId_Reference))
            {
                columnUniqueIds.Insert(0, columnUniqueId_Reference);
            }

            foreach (int countyId in countyIds)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    cancelled = true;
                    break;
                }

                List<int> countyIds_County = countyIds_Siblings.TryGetValue(countyId, out List<int>? countyIds_Temp) && countyIds_Temp is not null && countyIds_Temp.Count != 0 ? countyIds_Temp : [countyId];

                string directory_County = Path.Combine(scratchDirectory, countyId.ToString(System.Globalization.CultureInfo.InvariantCulture));
                string directory_Images = Path.Combine(directory_County, Constants.DirectoryName.PredictionImages);
                string path_Results = Path.Combine(directory_County, Constants.FileName.PredictionResults);

                try
                {
                    if (!Directory.Exists(directory_Images))
                    {
                        Directory.CreateDirectory(directory_Images);
                    }

                    if (yearBuiltPredictionPipelineOptions.ExportImages)
                    {
                        bool exported = await gisWebAPIManager.ExportPredictionImagesAsync(countyId, directory_Images, maxConcurrentRequests, yearBuiltPredictionPipelineOptions.Resume, cancellationToken);
                        if (!exported)
                        {
                            Fail(nameof(ExportPredictionImagesAsync), countyId);
                        }
                    }

                    long imageCount_County = Directory.EnumerateFiles(directory_Images, "*.jpeg").LongCount();
                    imageCount += imageCount_County;

                    List<Building2DYearBuiltPredictions>? building2DYearBuiltPredictions;
                    if (yearBuiltPredictionPipelineOptions.RunPrediction)
                    {
                        string? modelPath_Resolved = Query.ModelPath(yearBuiltPredictionPipelineOptions.ModelPath);

                        DiGi.YOLO.Classes.YOLOPredictionOptions? yOLOPredictionOptions = DiGi.YOLO.Create.YOLOPredictionOptions(yearBuiltPredictionPipelineOptions.PythonPath, modelPath_Resolved, directory_Images, path_Results, yearBuiltPredictionPipelineOptions.WorkingDirectory, yearBuiltPredictionPipelineOptions.Confidence);
                        if (yOLOPredictionOptions is null)
                        {
                            Fail(nameof(DiGi.YOLO.Create.YOLOPredictionOptions), countyId);
                            continue;
                        }

                        DiGi.YOLO.Classes.YOLOPredictionResult? yOLOPredictionResult = DiGi.YOLO.Modify.Predict(yOLOPredictionOptions, cancellationToken);
                        if (yOLOPredictionResult is null)
                        {
                            Fail(nameof(DiGi.YOLO.Modify.Predict), countyId);
                            continue;
                        }

                        // An empty source directory is not a failure - the script writes no output file at all,
                        // and there is simply nothing in this county to score.
                        if (!yOLOPredictionResult.Succeeded && yOLOPredictionResult.ImageCount != 0)
                        {
                            if (yOLOPredictionResult.StandardError is List<string> standardError && standardError.Count != 0)
                            {
                                messages.Add(string.Join("; ", standardError));
                            }

                            Fail(nameof(DiGi.YOLO.Modify.Predict), countyId);
                            continue;
                        }

                        building2DYearBuiltPredictions = DiGi.GIS.YOLO.Create.Building2DYearBuiltPredictions(yOLOPredictionResult);
                    }
                    else
                    {
                        building2DYearBuiltPredictions = DiGi.GIS.YOLO.Create.Building2DYearBuiltPredictions(DiGi.YOLO.Create.BoundingBoxResultFile(path_Results));
                    }

                    if (building2DYearBuiltPredictions is null || building2DYearBuiltPredictions.Count == 0)
                    {
                        Serilog.Modify.Log("No year built detections for county {CountyId} over {ImageCount} images", countyId, imageCount_County);
                        continue;
                    }

                    buildingCount += building2DYearBuiltPredictions.Count;
                    foreach (Building2DYearBuiltPredictions building2DYearBuiltPredictions_Temp in building2DYearBuiltPredictions)
                    {
                        detectionCount += building2DYearBuiltPredictions_Temp.Years?.Count ?? 0;
                    }

                    progressCount += building2DYearBuiltPredictions.Count;
                    progress?.Report(progressCount);

                    if (yearBuiltPredictionPipelineOptions.UpdateDetections)
                    {
                        bool updated = await gisWebAPIManager.UpdateBuildingDataYearBuiltPredictionsAsync(countyIds_County, building2DYearBuiltPredictions, batchSize, postOptions_Bulk);
                        if (updated)
                        {
                            buildingDataUpdatedCount += building2DYearBuiltPredictions.Count;
                        }
                        else
                        {
                            Fail(nameof(UpdateBuildingDataYearBuiltPredictionsAsync), countyId);
                        }
                    }

                    if (!yearBuiltPredictionPipelineOptions.Score)
                    {
                        continue;
                    }

                    if (yearBuiltPredictor is null)
                    {
                        Fail(nameof(IYearBuiltPredictor), countyId);
                        continue;
                    }

                    List<string> references = [];
                    foreach (Building2DYearBuiltPredictions building2DYearBuiltPredictions_Temp in building2DYearBuiltPredictions)
                    {
                        if (!string.IsNullOrWhiteSpace(building2DYearBuiltPredictions_Temp.Reference))
                        {
                            references.Add(building2DYearBuiltPredictions_Temp.Reference!);
                        }
                    }

                    Dictionary<string, short> years_ByReference = [];

                    for (int i = 0; i < references.Count; i += referenceBatchSize)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            cancelled = true;
                            break;
                        }

                        List<string> references_Batch = references.GetRange(i, Math.Min(referenceBatchSize, references.Count - i));

                        Table? table_Features = await Query.BuildingDataTableAsync(gisWebAPIManager, countyId, references_Batch, columnUniqueIds, postOptions_Bulk, cancellationToken);
                        if (table_Features is null)
                        {
                            Fail(nameof(Query.BuildingDataTableAsync), countyId);
                            continue;
                        }

                        featureRowCount += table_Features.RowCount;

                        Table? table_Predictions = yearBuiltPredictor.Predict(table_Features);
                        if (table_Predictions is null)
                        {
                            Fail(nameof(IYearBuiltPredictor), countyId);
                            continue;
                        }

                        int index_Reference = table_Predictions.GetColumnIndex(DiGi.GIS.IO.Constants.Column.Reference.Name);
                        int index_PredictedYearBuilt = table_Predictions.GetColumnIndex(DiGi.GIS.IO.Constants.Column.PredictedYearBuilt.Name);
                        if (index_Reference < 0 || index_PredictedYearBuilt < 0)
                        {
                            Fail(nameof(IYearBuiltPredictor), countyId);
                            continue;
                        }

                        for (int j = 0; j < table_Predictions.RowCount; j++)
                        {
                            if (table_Predictions.GetValue<string>(j, index_Reference) is not string reference || string.IsNullOrWhiteSpace(reference))
                            {
                                continue;
                            }

                            if (!table_Predictions.TryGetValue(j, index_PredictedYearBuilt, out ushort year))
                            {
                                continue;
                            }

                            // The stored year is a short while the column is a ushort, so a year the regressor
                            // put beyond that range is dropped rather than wrapped into a plausible one.
                            if (year > short.MaxValue)
                            {
                                continue;
                            }

                            years_ByReference[reference] = (short)year;
                        }
                    }

                    if (cancelled)
                    {
                        break;
                    }

                    predictionCount += years_ByReference.Count;

                    if (years_ByReference.Count == 0)
                    {
                        continue;
                    }

                    if (!yearBuiltPredictionPipelineOptions.UpdateYearBuiltData && !yearBuiltPredictionPipelineOptions.UpdatePredictedYearBuilt)
                    {
                        continue;
                    }

                    List<YearBuiltData> yearBuiltDatas = await Query.YearBuiltDatasAsync(gisWebAPIManager, countyId, years_ByReference, runTimestamp, yearBuiltPredictionPipelineOptions.UpdateYearBuiltData, maxConcurrentRequests, postOptions_Item, cancellationToken);

                    for (int i = 0; i < yearBuiltDatas.Count; i += batchSize)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            cancelled = true;
                            break;
                        }

                        List<YearBuiltData> yearBuiltDatas_Batch = yearBuiltDatas.GetRange(i, Math.Min(batchSize, yearBuiltDatas.Count - i));

                        if (yearBuiltPredictionPipelineOptions.UpdateYearBuiltData)
                        {
                            bool updated = await WebAPI.Modify.UpdateItemsAsync(gisWebAPIManager, yearBuiltDatas_Batch, countyIds_County, postOptions_Bulk);
                            if (updated)
                            {
                                yearBuiltDataUpdatedCount += yearBuiltDatas_Batch.Count;
                            }
                            else
                            {
                                Fail($"{nameof(WebAPI.Modify.UpdateItemsAsync)} ({PostgreSQL.Constants.TableName.YearBuiltData})", countyId);
                            }
                        }

                        if (yearBuiltPredictionPipelineOptions.UpdatePredictedYearBuilt)
                        {
                            // Built from the same merged year built data the history write uses, so the column
                            // and the history cannot end up saying different things.
                            Table table_PredictedYearBuilt = new();
                            DiGi.GIS.IO.Modify.Update_Building2D_PredictedYearBuilt(table_PredictedYearBuilt, countyId, yearBuiltDatas_Batch);

                            if (table_PredictedYearBuilt.RowCount != 0)
                            {
                                bool updated = await WebAPI.Modify.UpdateItemsAsync(gisWebAPIManager, table_PredictedYearBuilt, countyIds_County, postOptions_Bulk);
                                if (updated)
                                {
                                    buildingDataUpdatedCount += table_PredictedYearBuilt.RowCount;
                                }
                                else
                                {
                                    Fail($"{nameof(WebAPI.Modify.UpdateItemsAsync)} ({PostgreSQL.Constants.TableName.BuildingData})", countyId);
                                }
                            }
                        }

                        progressCount += yearBuiltDatas_Batch.Count;
                        progress?.Report(progressCount);
                    }
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    // Stopping is not a failure of this county, and what it managed to write is committed and
                    // still worth reporting, so the loop is left rather than the exception propagated.
                    cancelled = true;
                }
                catch (Exception exception)
                {
                    Fail(nameof(RunYearBuiltPredictionsAsync), countyId);
                    Serilog.Modify.Log(exception, "Year built prediction county failed - county {CountyId}", countyId);
                }

                if (cancelled)
                {
                    break;
                }
            }

            cancelled = cancelled || cancellationToken.IsCancellationRequested;

            Serilog.Modify.Log(
                cancelled || failedStepNames.Count != 0 ? Serilog.Enums.LogEventLevel.Warning : Serilog.Enums.LogEventLevel.Information,
                "{Method} finished{Cancelled}: {ImageCount} images, {DetectionCount} detections over {BuildingCount} buildings, {FeatureRowCount} feature rows, {PredictionCount} predictions, {YearBuiltDataUpdatedCount} year built data written, {BuildingDataUpdatedCount} building data rows written, {FailedStepCount} steps stepped over",
                nameof(RunYearBuiltPredictionsAsync), cancelled ? " after being cancelled" : string.Empty, imageCount, detectionCount, buildingCount, featureRowCount, predictionCount, yearBuiltDataUpdatedCount, buildingDataUpdatedCount, failedStepNames.Count);

            return Result();
        }
    }
}
