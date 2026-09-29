using System.Globalization;
using System.IO;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Splits an orthophoto image file name of the form <c>{reference}_{year}.jpeg</c> into its building reference and year.
        /// <para>The name is split at the <b>last</b> underscore, because a reference may itself contain underscores. The same rule <c>DiGi.GIS.YOLO.Create.Building2DYearBuiltPredictions</c> applies to the detector&apos;s output. A prefix test is not a substitute: <c>ABC_1_2015.jpeg</c> starts with <c>ABC_1</c> as well as with <c>ABC</c>, so matching a reference by <c>StartsWith</c> would claim another building&apos;s image.</para>
        /// <para>The year is parsed with <see cref="CultureInfo.InvariantCulture"/>, matching how the file name is written.</para>
        /// </summary>
        /// <param name="fileName">The file name or path. The directory and extension are ignored.</param>
        /// <param name="reference">When this method returns true, the building reference; otherwise null.</param>
        /// <param name="year">When this method returns true, the year of the orthophoto; otherwise 0.</param>
        /// <returns>True when the name carries a non-empty reference and a year.</returns>
        public static bool TryParseImageFileName(string? fileName, out string? reference, out short year)
        {
            reference = null;
            year = 0;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }

            string name = Path.GetFileNameWithoutExtension(fileName!);

            int index = name.LastIndexOf('_');
            if (index <= 0 || index == name.Length - 1)
            {
                return false;
            }

            if (!short.TryParse(name[(index + 1)..], NumberStyles.None, CultureInfo.InvariantCulture, out short year_Temp))
            {
                return false;
            }

            string reference_Temp = name[..index];
            if (string.IsNullOrWhiteSpace(reference_Temp))
            {
                return false;
            }

            reference = reference_Temp;
            year = year_Temp;
            return true;
        }
    }
}
