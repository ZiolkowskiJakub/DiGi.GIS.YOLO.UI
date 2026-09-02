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
        /// Reads every stored county row.
        /// <para>One row per polygon part rather than one per county, so a county whose territory is in several pieces appears several times under one <see cref="AdministrativeAreal2DReference.Code"/>. That is what makes this the answer to both questions the run asks of it: whether a named identifier is a county row at all, and which sibling parts it has.</para>
        /// <para>One request answers both for the whole run, so it is made once and the answer reused.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="postOptions">Optional configuration options for the request.</param>
        /// <returns>A task returning the county rows, or null when they could not be read. Null and an empty list mean different things: the first is a failed read, the second a stored estate with no counties in it.</returns>
        public static async Task<List<AdministrativeAreal2DReference>?> CountyReferencesAsync(this GISWebAPIManager? gisWebAPIManager, PostOptions? postOptions = null)
        {
            if (gisWebAPIManager is null)
            {
                return null;
            }

            HttpClient? httpClient = gisWebAPIManager.CreateHttpClient<AdministrativeAreal2DController>(nameof(AdministrativeAreal2DController.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync), out string? path);
            if (httpClient is null || string.IsNullOrWhiteSpace(path))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "HttpClient or path for {Method} could not be resolved", nameof(AdministrativeAreal2DController.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync));
                return null;
            }

            // The value goes on the wire as an integer rather than as the member name, which has already been
            // renamed once and would be a hard 400 against a build spelling it the other way.
            string requestUri = new UrlBuilder(path).AddParameter("administrativearealtype", (int)DiGi.GIS.PostgreSQL.Enums.AdministrativeArealType.County).ToString();

            try
            {
                PostResponse<List<AdministrativeAreal2DReference>?> postResponse = await DiGi.WebAPI.Query.GetAsync<List<AdministrativeAreal2DReference>>(httpClient, requestUri, postOptions ?? new PostOptions() { RequestResult = true });

                return postResponse is not null && postResponse.Succeeded ? postResponse.Result : null;
            }
            catch (Exception exception)
            {
                Serilog.Modify.Log(exception, "The county rows could not be read");
                return null;
            }
        }
    }
}
