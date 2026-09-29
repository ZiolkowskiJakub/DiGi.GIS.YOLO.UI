namespace DiGi.GIS.YOLO.UI.Constants
{
    /// <summary>
    /// Provides constant counts and limits observed by the GIS YOLO UI.
    /// </summary>
    public static class Count
    {
        /// <summary>
        /// Gets the largest number of references the building data table endpoint accepts in one request.
        /// <para>Mirrors the cap the endpoint enforces. A county is thirty to a hundred and fifty thousand buildings, so a feature read is always paged; asking for more than this fails the whole request rather than merely being slower.</para>
        /// </summary>
        public const int BuildingDataReference_Maximum = 10000;

        /// <summary>
        /// Gets the largest number of references the year built data endpoint accepts in one request.
        /// <para>Mirrors the cap the endpoint enforces. A county is thirty to a hundred and fifty thousand buildings, so the read is always paged; asking for more than this fails the whole request rather than merely being slower.</para>
        /// </summary>
        public const int YearBuiltDataReference_Maximum = 10000;

        /// <summary>
        /// Gets the number of orthophoto years a building is assumed to carry when a training dataset build is estimated before any imagery is read.
        /// <para>A rough figure from a sample building with eight years of coverage (2008 to 2023). The real count is only known once the imagery is read, so the estimate is an order of magnitude, not a budget.</para>
        /// </summary>
        public const int ImagePerBuilding_Estimate = 8;

        /// <summary>
        /// Gets the size, in bytes, one saved training image is assumed to take when a training dataset build is estimated before any imagery is read.
        /// <para>A rough figure from the same sample building, whose orthophoto crops are ten to eighteen kilobytes each, plus a label file.</para>
        /// </summary>
        public const long ImageByte_Estimate = 16000;
    }
}
