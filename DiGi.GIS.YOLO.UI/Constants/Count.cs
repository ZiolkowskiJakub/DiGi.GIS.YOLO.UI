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
    }
}
