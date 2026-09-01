using DiGi.GIS.PostgreSQL.Classes;
using DiGi.GIS.WebAPI.Classes;
using DiGi.WebAPI.Classes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Resolves, for each county row named, every polygon part of the county that row belongs to.
        /// <para>A county whose territory is in several pieces is held as one row per piece, so a county identifier names a part rather than a county. The write endpoints file each item under the part its reference belongs to, and can only do that when they are told which parts are in play - naming one part of a multi-part county files the whole batch there whether or not the buildings belong to it.</para>
        /// <para>One request answers this for the whole run, so it is made once and the answer reused. A county the list does not cover is left out rather than guessed at, and the caller then has nothing better to do than write it as a single part.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="countyIds">The county rows to resolve.</param>
        /// <param name="postOptions">Optional configuration options for the request.</param>
        /// <returns>A task returning each named county row mapped to the polygon parts of its county, ordered ascending. Empty when the county rows could not be read at all.</returns>
        public static async Task<Dictionary<int, List<int>>> SiblingCountyIdsAsync(this GISWebAPIManager? gisWebAPIManager, IEnumerable<int>? countyIds, PostOptions? postOptions = null)
        {
            Dictionary<int, List<int>> result = [];

            if (gisWebAPIManager is null || countyIds is null)
            {
                return result;
            }

            HashSet<int> countyIds_Requested = [.. countyIds];
            if (countyIds_Requested.Count == 0)
            {
                return result;
            }

            HttpClient? httpClient = gisWebAPIManager.CreateHttpClient<AdministrativeAreal2DController>(nameof(AdministrativeAreal2DController.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync), out string? path);
            if (httpClient is null || string.IsNullOrWhiteSpace(path))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "HttpClient or path for {Method} could not be resolved", nameof(AdministrativeAreal2DController.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync));
                return result;
            }

            // The value goes on the wire as an integer rather than as the member name, which has already been
            // renamed once and would be a hard 400 against a build spelling it the other way.
            string requestUri = new UrlBuilder(path).AddParameter("administrativearealtype", (int)DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType.County).ToString();

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences;
            try
            {
                PostResponse<List<AdministrativeAreal2DReference>?> postResponse = await DiGi.WebAPI.Query.GetAsync<List<AdministrativeAreal2DReference>>(httpClient, requestUri, postOptions ?? new PostOptions() { RequestResult = true });

                administrativeAreal2DReferences = postResponse is not null && postResponse.Succeeded ? postResponse.Result : null;
            }
            catch (Exception exception)
            {
                Serilog.Modify.Log(exception, "The county rows could not be read");
                return result;
            }

            if (administrativeAreal2DReferences is null)
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
