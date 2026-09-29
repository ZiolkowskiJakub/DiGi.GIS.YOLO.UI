using DiGi.GIS.PostgreSQL.Classes;
using DiGi.GIS.WebAPI.Classes;
using DiGi.WebAPI.Classes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads the building references filed under more than one county part, most collisions first.
        /// <para>The endpoint is global - it takes no county - so the caller filters the rows by <see cref="Building2DReferenceDuplicate.CountyIds"/>. A reference is unique only per county part and nothing enforces it, so this is the measurement of how many references a build must de-duplicate across parts.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="limit">The most rows to return, at least 1.</param>
        /// <param name="postOptions">Optional configuration options for the request.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task returning the duplicated references, or null when they could not be read.</returns>
        public static async Task<List<Building2DReferenceDuplicate>?> ReferenceDuplicatesAsync(this GISWebAPIManager? gisWebAPIManager, int limit, PostOptions? postOptions = null, CancellationToken cancellationToken = default)
        {
            if (gisWebAPIManager is null)
            {
                return null;
            }

            HttpClient? httpClient = gisWebAPIManager.CreateHttpClient<Building2DController>(nameof(Building2DController.GetReferenceDuplicatesAsync), out string? path);
            if (httpClient is null || string.IsNullOrWhiteSpace(path))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "HttpClient or path for {Method} could not be resolved", nameof(Building2DController.GetReferenceDuplicatesAsync));
                return null;
            }

            string requestUri = new UrlBuilder(path!).AddParameter("limit", limit < 1 ? 1 : limit).ToString();

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                PostResponse<List<Building2DReferenceDuplicate>?> postResponse = await DiGi.WebAPI.Query.GetAsync<List<Building2DReferenceDuplicate>>(httpClient, requestUri, postOptions ?? new PostOptions() { RequestResult = true, Delay = TimeSpan.FromSeconds(60) });

                if (postResponse is null || !postResponse.Succeeded)
                {
                    return null;
                }

                return postResponse.Result ?? [];
            }
            catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
            {
                Serilog.Modify.Log(exception, "The cross-part reference duplicates could not be read");
                return null;
            }
        }
    }
}
