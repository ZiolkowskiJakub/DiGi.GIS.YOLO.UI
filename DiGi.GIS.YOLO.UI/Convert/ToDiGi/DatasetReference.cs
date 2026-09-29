using DiGi.GIS.YOLO.UI.Classes;
using DiGi.GIS.YOLO.UI.Enums;
using DiGi.YOLO.Enums;
using System;
using System.Globalization;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Convert
    {
        /// <summary>
        /// Parses one row of the <c>dataset_references.tsv</c> manifest, written by <see cref="ToTSV(DatasetReference?)"/>, back into a dataset building.
        /// <para><c>Legacy</c> is not read: it is derived from <c>LegacySource</c>, so the two cannot disagree.</para>
        /// </summary>
        /// <param name="line">The tab-separated row, in the column order of <see cref="Constants.Header.DatasetReferences"/>.</param>
        /// <returns>The dataset building, or null when the row is blank, is the header, or does not parse.</returns>
        public static DatasetReference? ToDiGi_DatasetReference(this string? line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }

            string[] values = line!.Split('\t');
            if (values.Length < 6)
            {
                return null;
            }

            string reference = values[0].Trim();
            if (string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            if (!int.TryParse(values[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int countyId))
            {
                return null;
            }

            if (!Enum.TryParse(values[2], false, out Category category) || !Enum.IsDefined(typeof(Category), category))
            {
                return null;
            }

            if (!short.TryParse(values[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out short label))
            {
                return null;
            }

            string description = values[5].Trim();

            LegacySource? legacySource = null;
            foreach (LegacySource legacySource_Temp in Enum.GetValues(typeof(LegacySource)))
            {
                if (string.Equals(Core.Query.Description(legacySource_Temp), description, StringComparison.Ordinal))
                {
                    legacySource = legacySource_Temp;
                    break;
                }
            }

            if (legacySource is null)
            {
                return null;
            }

            return new DatasetReference(reference, countyId, category, label, legacySource.Value);
        }
    }
}
