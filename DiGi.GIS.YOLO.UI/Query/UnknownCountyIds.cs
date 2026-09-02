using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;
using System.Globalization;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Picks out the named county identifiers that are not county rows, and works out whether each was meant as a county code.
        /// <para>A county is addressed by <see cref="AdministrativeAreal2DReference.Id"/>, which is a database identifier running into six figures. <see cref="AdministrativeAreal2DReference.Code"/> is the four character territorial code, and the two are easy to confuse because a code reads as a number: asking for county 2212 asks for an identifier that does not exist, while the code 2212 is a real county held as two polygon parts under quite different identifiers.</para>
        /// <para>Nothing downstream can tell the difference on its own. An identifier in no county row simply matches no stored building, so the run exports no imagery, detects nothing, scores nothing and reports every one of those as a legitimate zero. That is why the scope is checked here, before any of it starts.</para>
        /// </summary>
        /// <param name="administrativeAreal2DReferences">The stored county rows, as read by <see cref="CountyReferencesAsync(GIS.WebAPI.Classes.GISWebAPIManager, DiGi.WebAPI.Classes.PostOptions)"/>.</param>
        /// <param name="countyIds">The county identifiers the run was scoped to.</param>
        /// <returns>Each named identifier that is not a county row, mapped to the identifiers of the county whose code it spells, ordered ascending. The mapped list is empty when the value is not a county code either, and the whole dictionary is empty when every named identifier is a county row.</returns>
        public static Dictionary<int, List<int>> UnknownCountyIds(IEnumerable<AdministrativeAreal2DReference>? administrativeAreal2DReferences, IEnumerable<int>? countyIds)
        {
            Dictionary<int, List<int>> result = [];

            if (administrativeAreal2DReferences is null || countyIds is null)
            {
                return result;
            }

            HashSet<int> countyIds_Known = [];
            Dictionary<string, List<int>> countyIds_ByCode = [];

            foreach (AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
            {
                if (administrativeAreal2DReference is null || administrativeAreal2DReference.Id <= 0)
                {
                    continue;
                }

                countyIds_Known.Add(administrativeAreal2DReference.Id);

                if (administrativeAreal2DReference.Code is not string code || string.IsNullOrWhiteSpace(code))
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

            foreach (int countyId in countyIds)
            {
                if (countyIds_Known.Contains(countyId) || result.ContainsKey(countyId))
                {
                    continue;
                }

                List<int> countyIds_Code = [];

                // A stored code is zero padded to four characters, so the plain decimal form of the identifier only
                // matches the counties whose code has no leading zero. Both spellings are tried, so a run scoped to
                // 201 is told about 0201 rather than left to look like an empty county.
                foreach (string code in new string[] { countyId.ToString(CultureInfo.InvariantCulture), countyId.ToString("D4", CultureInfo.InvariantCulture) })
                {
                    if (countyIds_ByCode.TryGetValue(code, out List<int>? countyIds_Temp) && countyIds_Temp is not null)
                    {
                        foreach (int countyId_Temp in countyIds_Temp)
                        {
                            if (!countyIds_Code.Contains(countyId_Temp))
                            {
                                countyIds_Code.Add(countyId_Temp);
                            }
                        }
                    }
                }

                countyIds_Code.Sort();

                result[countyId] = countyIds_Code;
            }

            return result;
        }
    }
}
