using DiGi.Core.Classes;
using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Create
    {
        /// <summary>
        /// Builds the building data table a detection write posts: the reference, the county and the per-year detection columns of each building.
        /// <para>With <paramref name="references"/> null the table is exactly what <see cref="GIS.IO.Modify.Update_Building2D_YearBuiltPredictions"/> builds - a row per detected building and a column per year some building of the batch was detected in. The endpoint upserts every column on the table and writes an unset cell as NULL, but a building or a column that is not on the table is left as it stands.</para>
        /// <para>That is not enough once the weights change. A building the new detector does not fire on is not on the table, so the previous detector's values - its false positives included - stay on it, and the stored detections become a mix of two detectors that nothing reports (ZiolkowskiJakub/DiGi.GIS.YOLO.UI#21). Naming the scored <paramref name="references"/> makes the write a replacement instead: every detection column of <paramref name="years"/> is on the table, and every scored reference without a detection gets a row of its own with those cells unset, so each scored building's detection columns are overwritten as a whole.</para>
        /// <para>A detected year outside <paramref name="years"/> is still written where it was detected, as before, and is not cleared elsewhere - the regressor does not read it.</para>
        /// </summary>
        /// <param name="countyId">The county row the rows are stamped with.</param>
        /// <param name="building2DYearBuiltPredictions">The detections, one instance per detected building.</param>
        /// <param name="references">The references the detector scored, detected or not. Null keeps the table to the detected buildings and their detected years.</param>
        /// <param name="years">The year range whose detection columns are replaced. Null applies the default range of <see cref="GIS.IO.Query.YearBuiltPredictionFeatureGroups(Range{int}?, IEnumerable{double}?)"/>, the same one the scoring step projects. Ignored when <paramref name="references"/> is null.</param>
        /// <returns>The table, which has no rows when there is nothing to write; null when the county is not a positive identifier.</returns>
        public static Table? BuildingDataYearBuiltPredictionsTable(int countyId, IEnumerable<Building2DYearBuiltPredictions>? building2DYearBuiltPredictions, IEnumerable<string>? references = null, Range<int>? years = null)
        {
            if (countyId <= 0)
            {
                return null;
            }

            Table result = new();

            List<Building2DYearBuiltPredictions> building2DYearBuiltPredictions_Write = [];
            HashSet<string> references_Detected = new(StringComparer.Ordinal);

            if (building2DYearBuiltPredictions is not null)
            {
                foreach (Building2DYearBuiltPredictions building2DYearBuiltPredictions_Temp in building2DYearBuiltPredictions)
                {
                    if (building2DYearBuiltPredictions_Temp?.Reference is not string reference || string.IsNullOrWhiteSpace(reference))
                    {
                        continue;
                    }

                    building2DYearBuiltPredictions_Write.Add(building2DYearBuiltPredictions_Temp);
                    references_Detected.Add(reference);
                }
            }

            if (references is not null)
            {
                // Added before the rows, so a column of the range that no building of this batch was detected in is
                // still on the table and every row's cell in it is written as NULL.
                Dictionary<string, List<Column>> columns_ByGroup = GIS.IO.Query.YearBuiltPredictionFeatureGroups(years, null);
                if (columns_ByGroup.TryGetValue(GIS.IO.Constants.YearBuiltPredictionFeatureGroup.Detection, out List<Column>? columns_Detection) && columns_Detection is not null)
                {
                    GIS.IO.Modify.UpdateColumn(result, GIS.IO.Constants.Column.Reference);
                    GIS.IO.Modify.UpdateColumn(result, GIS.IO.Constants.Column.CountyId);

                    foreach (Column column in columns_Detection)
                    {
                        GIS.IO.Modify.UpdateColumn(result, column);
                    }
                }

                foreach (string reference in references)
                {
                    if (string.IsNullOrWhiteSpace(reference) || !references_Detected.Add(reference))
                    {
                        continue;
                    }

                    // An empty series still makes a row: the reference and the county, and nothing else.
                    building2DYearBuiltPredictions_Write.Add(new Building2DYearBuiltPredictions(reference, []));
                }
            }

            if (building2DYearBuiltPredictions_Write.Count != 0)
            {
                GIS.IO.Modify.Update_Building2D_YearBuiltPredictions(result, countyId, building2DYearBuiltPredictions_Write);
            }

            return result;
        }
    }
}
