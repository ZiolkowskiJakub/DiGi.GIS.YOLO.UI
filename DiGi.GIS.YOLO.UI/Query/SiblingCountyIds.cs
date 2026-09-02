using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Resolves, for each county row named, every polygon part of the county that row belongs to.
        /// <para>A county whose territory is in several pieces is held as one row per piece, so a county identifier names a part rather than a county. The write endpoints file each item under the part its reference belongs to, and can only do that when they are told which parts are in play - naming one part of a multi-part county files the whole batch there whether or not the buildings belong to it.</para>
        /// <para>A county row the list does not cover is left out rather than guessed at. Ask <see cref="UnknownCountyIds(IEnumerable{AdministrativeAreal2DReference}, IEnumerable{int})"/> about those before running anything: an identifier that is in no county row is a mis-scoped run, not a county with one part.</para>
        /// </summary>
        /// <param name="administrativeAreal2DReferences">The stored county rows, as read by <see cref="CountyReferencesAsync(GIS.WebAPI.Classes.GISWebAPIManager, DiGi.WebAPI.Classes.PostOptions)"/>.</param>
        /// <param name="countyIds">The county rows to resolve.</param>
        /// <returns>Each named county row mapped to the polygon parts of its county, ordered ascending.</returns>
        public static Dictionary<int, List<int>> SiblingCountyIds(IEnumerable<AdministrativeAreal2DReference>? administrativeAreal2DReferences, IEnumerable<int>? countyIds)
        {
            Dictionary<int, List<int>> result = [];

            if (administrativeAreal2DReferences is null || countyIds is null)
            {
                return result;
            }

            HashSet<int> countyIds_Requested = [.. countyIds];
            if (countyIds_Requested.Count == 0)
            {
                return result;
            }

            Dictionary<string, List<int>> countyIds_ByCode = [];
            foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
            {
                if (administrativeAreal2DReference?.Code is not string code || string.IsNullOrWhiteSpace(code) || administrativeAreal2DReference.Id <= 0)
                {
                    continue;
                }

                if (!countyIds_ByCode.TryGetValue(code, out List<int>? countyIds_Code))
                {
                    countyIds_Code = [];
                    countyIds_ByCode[code] = countyIds_Code;
                }

                countyIds_Code.Add(administrativeAreal2DReference.Id);
            }

            foreach (KeyValuePair<string, List<int>> keyValuePair in countyIds_ByCode)
            {
                List<int> countyIds_Code = keyValuePair.Value;
                countyIds_Code.Sort();

                foreach (int countyId in countyIds_Code)
                {
                    if (countyIds_Requested.Contains(countyId))
                    {
                        result[countyId] = countyIds_Code;
                    }
                }
            }

            return result;
        }
    }
}
