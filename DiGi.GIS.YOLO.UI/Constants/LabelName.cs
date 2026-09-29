namespace DiGi.GIS.YOLO.UI.Constants
{
    /// <summary>
    /// Provides the class names of the YOLO training dataset.
    /// </summary>
    public static class LabelName
    {
        /// <summary>
        /// Gets the name of the single class the year built detector is trained on. It is added first, so it is class index 0 - the index the production weights report.
        /// </summary>
        public const string Building = "Building";
    }
}
