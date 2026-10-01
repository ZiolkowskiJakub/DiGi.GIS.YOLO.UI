namespace DiGi.GIS.YOLO.UI.Constants
{
    /// <summary>
    /// Provides constant values for configuration file names used within the GIS YOLO UI.
    /// </summary>
    public static class FileName
    {
        /// <summary>
        /// Gets the default filename of the configuration file for the Web API client.
        /// </summary>
        public const string GISWebAPIClientConfigurationFile = "GIS_WebAPI_Client.conf";

        /// <summary>
        /// Gets the default filename of the configuration file for the Year Built prediction pipeline options.
        /// </summary>
        public const string YearBuiltPredictionPipelineOptions = "YearBuiltPredictionPipelineOptions.json";

        /// <summary>
        /// Gets the name of the file a county's year built detections are written to by the prediction script.
        /// </summary>
        /// <remarks>The script opens it for writing rather than appending, so a repeated run over one county replaces the previous answer instead of doubling it.</remarks>
        public const string PredictionResults = "results.bbrf";

        /// <summary>
        /// Gets the default filename of the configuration file for the YOLO training dataset tooling - the dataset builder, the label check and the detector evaluation.
        /// </summary>
        public const string YOLOTrainingDatasetOptions = "YOLOTrainingDatasetOptions.json";

        /// <summary>
        /// Gets the default filename of the configuration file for the <c>--train</c> console mode.
        /// </summary>
        public const string YOLOTrainingRunOptions = "YOLOTrainingRunOptions.json";

        /// <summary>
        /// Gets the name of the manifest written beside a training dataset&apos;s conf.yaml: one row per building with its county, split, label and Legacy decision.
        /// <para>It is also the resume journal - a building is appended once all of its images and label files are written, so a building it names is complete.</para>
        /// </summary>
        public const string DatasetReferences = "dataset_references.tsv";

        /// <summary>
        /// Gets the name of the checkpoint ultralytics writes into a run&apos;s <c>weights</c> folder and keeps up to date after every epoch.
        /// <para>A resume continues this file, so a run whose folder holds it and no completed <c>&lt;RunName&gt;.pt</c> is an interrupted run.</para>
        /// </summary>
        public const string LastWeights = "last.pt";
    }
}
