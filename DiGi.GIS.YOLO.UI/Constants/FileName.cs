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
    }
}
