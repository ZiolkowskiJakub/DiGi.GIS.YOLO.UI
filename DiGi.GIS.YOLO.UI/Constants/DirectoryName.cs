namespace DiGi.GIS.YOLO.UI.Constants
{
    /// <summary>
    /// Provides constant directory names used within the GIS YOLO UI.
    /// </summary>
    public static class DirectoryName
    {
        /// <summary>
        /// Gets the name of the folder a county's exported orthophoto prediction images are written to.
        /// </summary>
        public const string PredictionImages = "images";

        /// <summary>
        /// Gets the name of the folder inside a training run that ultralytics writes the checkpoints it can resume from into.
        /// <para>Holds <c>last.pt</c>, the checkpoint a resume continues, and the per-epoch checkpoints a run with <c>save_period</c> leaves behind.</para>
        /// </summary>
        public const string Weights = "weights";
    }
}
