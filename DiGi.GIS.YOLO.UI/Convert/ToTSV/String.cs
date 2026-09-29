using DiGi.GIS.YOLO.UI.Classes;
using System.Globalization;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Convert
    {
        /// <summary>
        /// Formats a dataset building as one row of the <c>dataset_references.tsv</c> manifest, in the column order of <see cref="Constants.Header.DatasetReferences"/>.
        /// <para>Numbers are written with <see cref="CultureInfo.InvariantCulture"/>, the split by its enum name, <c>Legacy</c> as <c>true</c> / <c>false</c>, and <c>LegacySource</c> by its description - empty for a clean building.</para>
        /// </summary>
        /// <param name="datasetReference">The dataset building.</param>
        /// <returns>The tab-separated row, without a line break, or null when there is no building or no reference.</returns>
        public static string? ToTSV(this DatasetReference? datasetReference)
        {
            if (datasetReference is null || string.IsNullOrWhiteSpace(datasetReference.Reference))
            {
                return null;
            }

            return string.Join("\t",
                datasetReference.Reference,
                datasetReference.CountyId.ToString(CultureInfo.InvariantCulture),
                datasetReference.Category.ToString(),
                datasetReference.Label.ToString(CultureInfo.InvariantCulture),
                datasetReference.Legacy ? "true" : "false",
                Core.Query.Description(datasetReference.LegacySource));
        }
    }
}
