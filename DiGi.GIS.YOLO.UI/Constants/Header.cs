namespace DiGi.GIS.YOLO.UI.Constants
{
    /// <summary>
    /// Provides the header lines of the tab-separated files the GIS YOLO UI writes.
    /// </summary>
    public static class Header
    {
        /// <summary>
        /// Gets the header of the <c>dataset_references.tsv</c> manifest. The columns are read by name, so this is the contract between the dataset builder and the two checks that read the dataset.
        /// </summary>
        public const string DatasetReferences = "Reference\tCountyId\tCategory\tLabel\tLegacy\tLegacySource";
    }
}
