using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// Provides the settings of the YOLO training dataset tooling: which counties the dataset is built from and where it goes, how buildings are labelled and split, and what the label check and the detector evaluation run with.
    /// <para>One options type serves the three console modes - <c>--dataset</c>, <c>--check-labels</c> and <c>--evaluate-detector</c> - because the two checks read the dataset the first one built, and one file then names it once.</para>
    /// <para>Every default is a working value except the two that state scope: <see cref="CountyIds"/> and <see cref="OutputDirectory"/> have none, so a dataset is never built from counties nobody named into a folder nobody chose. There is deliberately no member for the Web API key; it travels on <see cref="GIS.WebAPI.Classes.GISWebAPIManager.Key"/>.</para>
    /// </summary>
    public class YOLOTrainingDatasetOptions : SerializableOptions, IGISYOLOUISerializableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingDatasetOptions"/> class with default values.
        /// </summary>
        public YOLOTrainingDatasetOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingDatasetOptions"/> class by copying an existing options instance.
        /// </summary>
        /// <param name="yOLOTrainingDatasetOptions">The source options instance to copy from.</param>
        public YOLOTrainingDatasetOptions(YOLOTrainingDatasetOptions? yOLOTrainingDatasetOptions)
            : base(yOLOTrainingDatasetOptions)
        {
            if (yOLOTrainingDatasetOptions is not null)
            {
                Confidence = yOLOTrainingDatasetOptions.Confidence;
                CountOnly = yOLOTrainingDatasetOptions.CountOnly;
                CountyIds = yOLOTrainingDatasetOptions.CountyIds is null ? null : [.. yOLOTrainingDatasetOptions.CountyIds];
                HoldoutDenominator = yOLOTrainingDatasetOptions.HoldoutDenominator;
                LabelCheckOverlayCount = yOLOTrainingDatasetOptions.LabelCheckOverlayCount;
                LabelCheckSampleSize = yOLOTrainingDatasetOptions.LabelCheckSampleSize;
                LegacyCutoff = yOLOTrainingDatasetOptions.LegacyCutoff;
                LegacyReferencesFilePath = yOLOTrainingDatasetOptions.LegacyReferencesFilePath;
                MaxConcurrentRequests = yOLOTrainingDatasetOptions.MaxConcurrentRequests;
                ModelPath = yOLOTrainingDatasetOptions.ModelPath;
                Offset = yOLOTrainingDatasetOptions.Offset;
                OutputDirectory = yOLOTrainingDatasetOptions.OutputDirectory;
                PythonPath = yOLOTrainingDatasetOptions.PythonPath;
                ReferenceBatchSize = yOLOTrainingDatasetOptions.ReferenceBatchSize;
                ReferenceDuplicateLimit = yOLOTrainingDatasetOptions.ReferenceDuplicateLimit;
                ReportsDirectory = yOLOTrainingDatasetOptions.ReportsDirectory;
                Resume = yOLOTrainingDatasetOptions.Resume;
                Seed = yOLOTrainingDatasetOptions.Seed;
                ValidateWeight = yOLOTrainingDatasetOptions.ValidateWeight;
                WeightsPaths = yOLOTrainingDatasetOptions.WeightsPaths is null ? null : [.. yOLOTrainingDatasetOptions.WeightsPaths];
                WorkingDirectory = yOLOTrainingDatasetOptions.WorkingDirectory;
                Years = Core.Query.Clone(yOLOTrainingDatasetOptions.Years);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingDatasetOptions"/> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the configuration settings.</param>
        public YOLOTrainingDatasetOptions(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets or sets the confidence threshold a detection has to reach to be reported by the label check and the detector evaluation, passed to the prediction script as --conf.
        /// <para>The default is the production threshold, so the evaluation measures the detector as the pipeline runs it.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Confidence))]
        public double Confidence { get; set; } = 0.1;

        /// <summary>
        /// Gets or sets whether <c>--dataset</c> only counts: per county, the labelled buildings, the split, the Legacy agreement table, bounded entries, cross-part duplicates and an estimate of the requests, images and disk a build would need.
        /// <para>No orthophoto is requested and nothing is written. Run it first and post the output on the tracking issue before a build.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CountOnly))]
        public bool CountOnly { get; set; } = false;

        /// <summary>
        /// Gets or sets the county rows the dataset is built from, by identifier.
        /// <para>Identifiers rather than codes, and each identifier is a polygon part: name every part of a county. A reference found under several named parts is built once, under the lowest identifier.</para>
        /// <para>There is no default. The scope is always stated.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        public HashSet<int>? CountyIds { get; set; } = null;

        /// <summary>
        /// Gets or sets the holdout rate as one in how many references: a reference is held out when <see cref="GIS.IO.Query.Holdout(string?, int)"/> says so, and a held-out reference goes to the Test split only.
        /// <para>The default, 5, is the holdout the Year Built regressor uses, so both models are measured on the same buildings. Change it only together with the regressor.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(HoldoutDenominator))]
        public int HoldoutDenominator { get; set; } = 5;

        /// <summary>
        /// Gets or sets how many label check samples are written as overlay images - the label box and the best detection drawn on the image - to the reports folder.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(LabelCheckOverlayCount))]
        public int LabelCheckOverlayCount { get; set; } = 20;

        /// <summary>
        /// Gets or sets how many positive images the label check runs the detector over, drawn with <see cref="Seed"/> from the Train and Validate splits.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(LabelCheckSampleSize))]
        public int LabelCheckSampleSize { get; set; } = 500;

        /// <summary>
        /// Gets or sets the moment the <c>train8</c> detector was saved. A held-out building with a user entry that is undated or dated before it may have been seen by <c>train8</c>, and is Legacy.
        /// <para>The default, 2025-05-23T00:00:00Z, is <c>train8</c>&apos;s save date from the DiGi.YOLO provenance table.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(LegacyCutoff))]
        public DateTimeOffset LegacyCutoff { get; set; } = new(2025, 5, 23, 0, 0, 0, TimeSpan.Zero);

        /// <summary>
        /// Gets or sets the path of the legacy reference list - the regressor training table <c>Data_2025.05.27.tsv</c> kept in DiGi.GIS.ML under <c>Data/</c> - resolved like <see cref="ModelPath"/>.
        /// <para>A reference it names is Legacy: the table was built from the same sources days after <c>train8</c>. Copy it into the git-ignored <c>user files</c> folder; the default names it there. The build is refused when it cannot be read, because a missing list would report every held-out building as clean.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(LegacyReferencesFilePath))]
        public string? LegacyReferencesFilePath { get; set; } = "user files/Data_2025.05.27.tsv";

        /// <summary>
        /// Gets or sets how many orthophoto requests may be in flight at once.
        /// <para>The deployed API shares one connection pool across every caller, so keep this to a handful.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(MaxConcurrentRequests))]
        public int MaxConcurrentRequests { get; set; } = 8;

        /// <summary>
        /// Gets or sets the path of the weights the label check runs, resolved against the runner by <see cref="Query.ModelPath(string?)"/>.
        /// <para>The default is the production detector, <c>train8</c>: the check compares the new labels with what the current detector finds.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ModelPath))]
        public string? ModelPath { get; set; } = "user files/YOLO/models/model.pt";

        /// <summary>
        /// Gets or sets the distance, in metres, a building&apos;s bounding box is grown by on every side before it is projected onto an orthophoto.
        /// <para>The default, 1, is the offset the legacy builder used for <c>train8</c>.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Offset))]
        public double Offset { get; set; } = 1;

        /// <summary>
        /// Gets or sets the root directory of the dataset: <c>conf.yaml</c>, the <c>images</c> and <c>labels</c> folders, and the <c>dataset_references.tsv</c> manifest.
        /// <para>Must be absolute - it is written into <c>conf.yaml</c> as the dataset path. There is no default.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(OutputDirectory))]
        public string? OutputDirectory { get; set; } = null;

        /// <summary>
        /// Gets or sets the path of the CPython interpreter that runs the prediction script, or the name of one on PATH. Used by the label check and the detector evaluation.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(PythonPath))]
        public string? PythonPath { get; set; } = null;

        /// <summary>
        /// Gets or sets how many references a bulk read is asked for in one request. A larger value is clamped to the endpoint cap of ten thousand.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ReferenceBatchSize))]
        public int ReferenceBatchSize { get; set; } = 10000;

        /// <summary>
        /// Gets or sets the most cross-part reference duplicates <c>CountOnly</c> asks the API for. The endpoint is global - it cannot be filtered by county - so it is read once and filtered here.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ReferenceDuplicateLimit))]
        public int ReferenceDuplicateLimit { get; set; } = 100000;

        /// <summary>
        /// Gets or sets the directory the label check writes its overlay images to. A relative path is resolved against the current directory.
        /// <para>Generated output, so the default is the git-ignored <c>user files/reports</c> folder, never <c>files</c>.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ReportsDirectory))]
        public string? ReportsDirectory { get; set; } = "user files/reports";

        /// <summary>
        /// Gets or sets whether a build into an existing dataset continues it rather than refusing.
        /// <para>The manifest is the journal: a building is appended to it once all of its images and label files are written, a building already in it is skipped, and the files of a building that is not in it - one a stopped run left half-written - are removed and rebuilt. With it off, an existing dataset is refused.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Resume))]
        public bool Resume { get; set; } = true;

        /// <summary>
        /// Gets or sets the seed of the Train / Validate split and of the label check sample.
        /// <para>The split is drawn over the non-holdout references sorted ordinally, so the same labels and seed always give the same split.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Seed))]
        public int Seed { get; set; } = 0;

        /// <summary>
        /// Gets or sets the share of the non-holdout references that go to the Validate split; the rest go to Train.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ValidateWeight))]
        public double ValidateWeight { get; set; } = 0.1;

        /// <summary>
        /// Gets or sets the weights files the detector evaluation compares, each resolved by <see cref="Query.ModelPath(string?)"/>.
        /// <para>Every file is run over the same Test images and reported as one row per subset, so list the production weights alongside the candidates.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(WeightsPaths))]
        public List<string>? WeightsPaths { get; set; } = null;

        /// <summary>
        /// Gets or sets the directory the prediction process runs in, which is also where the runner keeps the Python scripts. Null uses the folder of the prediction output.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(WorkingDirectory))]
        public string? WorkingDirectory { get; set; } = null;

        /// <summary>
        /// Gets or sets the range of years the detector evaluation reads first detection years over. Null uses the default of <see cref="GIS.IO.Query.FirstDetectionYears"/>, 2008 to 2025.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Years))]
        public Range<int>? Years { get; set; } = null;
    }
}
