using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// Provides the settings one unattended run of the Year Built prediction pipeline needs: which counties it covers, where it keeps its scratch files, which weights and interpreter score the imagery, and which of its steps actually run.
    /// <para>Every step carries its own flag so a run can be resumed without repeating the expensive ones. The three write steps are off by default, so a first pass over a county is harmless - the run reads everything, scores everything and stores nothing.</para>
    /// <para>There is deliberately no member for the Web API key. These options are written to disk as JSON and the key is a secret, so it travels on <see cref="GIS.WebAPI.Classes.GISWebAPIManager.Key"/>, which the host reads from a git-ignored configuration file.</para>
    /// </summary>
    public class YearBuiltPredictionPipelineOptions : SerializableOptions, IGISYOLOUISerializableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltPredictionPipelineOptions"/> class with default values.
        /// </summary>
        public YearBuiltPredictionPipelineOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltPredictionPipelineOptions"/> class by copying an existing options instance.
        /// </summary>
        /// <param name="yearBuiltPredictionPipelineOptions">The source options instance to copy from.</param>
        public YearBuiltPredictionPipelineOptions(YearBuiltPredictionPipelineOptions? yearBuiltPredictionPipelineOptions)
            : base(yearBuiltPredictionPipelineOptions)
        {
            if (yearBuiltPredictionPipelineOptions is not null)
            {
                BatchSize = yearBuiltPredictionPipelineOptions.BatchSize;
                Confidence = yearBuiltPredictionPipelineOptions.Confidence;
                CountyIds = yearBuiltPredictionPipelineOptions.CountyIds is null ? null : [.. yearBuiltPredictionPipelineOptions.CountyIds];
                ExportImages = yearBuiltPredictionPipelineOptions.ExportImages;
                MaxConcurrentRequests = yearBuiltPredictionPipelineOptions.MaxConcurrentRequests;
                ModelPath = yearBuiltPredictionPipelineOptions.ModelPath;
                PythonPath = yearBuiltPredictionPipelineOptions.PythonPath;
                Radiuses = yearBuiltPredictionPipelineOptions.Radiuses is null ? null : [.. yearBuiltPredictionPipelineOptions.Radiuses];
                ReferenceBatchSize = yearBuiltPredictionPipelineOptions.ReferenceBatchSize;
                Resume = yearBuiltPredictionPipelineOptions.Resume;
                RunPrediction = yearBuiltPredictionPipelineOptions.RunPrediction;
                ScratchDirectory = yearBuiltPredictionPipelineOptions.ScratchDirectory;
                Score = yearBuiltPredictionPipelineOptions.Score;
                UpdateDetections = yearBuiltPredictionPipelineOptions.UpdateDetections;
                UpdatePredictedYearBuilt = yearBuiltPredictionPipelineOptions.UpdatePredictedYearBuilt;
                UpdateYearBuiltData = yearBuiltPredictionPipelineOptions.UpdateYearBuiltData;
                WorkingDirectory = yearBuiltPredictionPipelineOptions.WorkingDirectory;
                Years = Core.Query.Clone(yearBuiltPredictionPipelineOptions.Years);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltPredictionPipelineOptions"/> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the configuration settings.</param>
        public YearBuiltPredictionPipelineOptions(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets or sets the number of buildings whose detections or predictions are sent in one request.
        /// <para>A county carries ninety-odd detection columns over tens of thousands of buildings, so the writes are batched rather than sent as one body.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(BatchSize))]
        public int BatchSize { get; set; } = 5000;

        /// <summary>
        /// Gets or sets the confidence threshold a detection has to reach to be reported, passed to the prediction script as --conf.
        /// <para>The default matches the script's own default. The weights are frozen, so this is the only knob over how much the detector reports.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Confidence))]
        public double Confidence { get; set; } = 0.1;

        /// <summary>
        /// Gets or sets the county rows the run covers, by identifier.
        /// <para>Identifiers rather than codes, and each identifier is a polygon part: a county whose territory is in several pieces is held as one row per piece. Name every part of a county, so the parts are recognised as siblings and each written row is filed under the part its reference belongs to.</para>
        /// <para>There is no run-everything default. The pipeline writes deployed data, so the scope is always stated.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        public HashSet<int>? CountyIds { get; set; } = null;

        /// <summary>
        /// Gets or sets whether the orthophoto imagery is exported to the scratch directory before the detector runs.
        /// <para>Turn it off to score imagery a previous run already wrote. With <see cref="Resume"/> set the export skips what is on disk anyway, so leaving it on costs one listing request per county.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ExportImages))]
        public bool ExportImages { get; set; } = true;

        /// <summary>
        /// Gets or sets how many Web API requests may be in flight at once.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(MaxConcurrentRequests))]
        public int MaxConcurrentRequests { get; set; } = 8;

        /// <summary>
        /// Gets or sets the path of the trained weights the detector scores with.
        /// <para>Left null the script falls back to its own search, which picks whichever training run is newest on disk. Name the file, so a run is reproducible.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ModelPath))]
        public string? ModelPath { get; set; } = null;

        /// <summary>
        /// Gets or sets the path of the CPython interpreter that runs the prediction script, or the name of one on PATH.
        /// <para>This has to be CPython with ultralytics and torch installed. The IronPython engine in DiGi.Scripting.Python can host neither.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(PythonPath))]
        public string? PythonPath { get; set; } = null;

        /// <summary>
        /// Gets or sets how many references a bulk read is asked for in one request.
        /// <para>The feature table and the year built data share the cap - each endpoint refuses more than ten thousand references at a time - and a county is thirty to a hundred and fifty thousand buildings, so both reads are paged. A larger value is clamped down to the cap while the run works.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ReferenceBatchSize))]
        public int ReferenceBatchSize { get; set; } = 10000;

        /// <summary>
        /// Gets or sets whether work a previous run already did is skipped rather than repeated.
        /// <para>Governs the image export, which is the expensive step: an image already on disk is neither fetched nor re-encoded.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Resume))]
        public bool Resume { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the detector is run over the exported imagery.
        /// <para>Turn it off to re-use the detections a previous run wrote to the scratch directory. The results file is opened for writing rather than appending, so a repeated run replaces the previous answer instead of doubling it.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(RunPrediction))]
        public bool RunPrediction { get; set; } = true;

        /// <summary>
        /// Gets or sets the directory the run keeps its imagery and its detection results in.
        /// <para>Each county gets its own folder underneath, named after the county identifier, so two counties cannot score each other's imagery and a resumed run finds what it left behind.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ScratchDirectory))]
        public string? ScratchDirectory { get; set; } = null;

        /// <summary>
        /// Gets or sets whether the building features are read and scored into predicted construction years.
        /// <para>Requires an implementation of <see cref="GIS.IO.Interfaces.IYearBuiltPredictor"/>. With it off the run stops after the detections, which is the shape of a detection-only pass.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Score))]
        public bool Score { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the detection features are written into the stored building data.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(UpdateDetections))]
        public bool UpdateDetections { get; set; } = false;

        /// <summary>
        /// Gets or sets whether the latest predicted construction year is written into the building data column.
        /// <para>Written from the same merged year built data the history step builds, so the column and the history cannot disagree.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(UpdatePredictedYearBuilt))]
        public bool UpdatePredictedYearBuilt { get; set; } = false;

        /// <summary>
        /// Gets or sets whether the dated prediction is written into the year built data, preserving the history.
        /// <para>The stored entry is read back and added to rather than replaced, because a year built datum built fresh carries a new identifier and would be stored alongside the building's existing one rather than in place of it.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(UpdateYearBuiltData))]
        public bool UpdateYearBuiltData { get; set; } = false;

        /// <summary>
        /// Gets or sets the directory the prediction process runs in, which is also where the runner keeps the Python scripts.
        /// <para>The prediction script imports its helper module from the directory it sits in, so the two files have to stay together. Ultralytics also writes its own caches here.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(WorkingDirectory))]
        public string? WorkingDirectory { get; set; } = null;

        /// <summary>
        /// Gets or sets the range of years the detection and temporal features cover.
        /// <para>Has to match the range the regressor was trained on, because it decides which columns the feature projection asks for. Null means the same default the column list itself applies.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Years))]
        public Range<int>? Years { get; set; } = null;

        /// <summary>
        /// Gets or sets the radiuses the radial ratio features cover, in metres.
        /// <para>Carried for the same reason as <see cref="Years"/>: it decides which columns the feature projection asks for, and a projection that disagrees with the range the regressor was trained on hands the model defaults rather than features - which scores without failing. Null means the same default the column list itself applies.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Radiuses))]
        public List<double>? Radiuses { get; set; } = null;
    }
}
