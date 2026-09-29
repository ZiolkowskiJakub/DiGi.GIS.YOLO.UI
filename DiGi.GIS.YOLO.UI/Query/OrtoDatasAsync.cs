using DiGi.GIS.WebAPI.Classes;
using DiGi.WebAPI.Classes;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads the stored orthophotos of one building.
        /// <para>The endpoint has no <c>fallbackbyreference</c> parameter and answers <c>204 No Content</c> for a building it does not hold under the county sent. So the county is sent first, and a building it does not answer is asked for once more without it, which finds imagery filed under a sibling polygon part of the county.</para>
        /// <para>A failure is logged and answered with null, the same as a building with no imagery: the caller counts both as a building without imagery and steps over it.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="countyId">The identifier of the county part the building belongs to.</param>
        /// <param name="reference">The building reference.</param>
        /// <param name="postOptions">Optional configuration options for the requests.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task returning the building&apos;s orthophotos, or null when there are none or they could not be read.</returns>
        public static async Task<GIS.Classes.OrtoDatas?> OrtoDatasAsync(this GISWebAPIManager? gisWebAPIManager, int countyId, string? reference, PostOptions? postOptions = null, CancellationToken cancellationToken = default)
        {
            if (gisWebAPIManager is null || string.IsNullOrWhiteSpace(reference))
            {
                return null;
            }

            HttpClient? httpClient = gisWebAPIManager.CreateHttpClient<OrtoDatasController>(nameof(OrtoDatasController.GetItemByReferenceAsync), out string? path);
            if (httpClient is null || string.IsNullOrWhiteSpace(path))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "HttpClient or path for {Method} could not be resolved", nameof(OrtoDatasController.GetItemByReferenceAsync));
                return null;
            }

            PostOptions postOptions_Temp = postOptions ?? new PostOptions() { RequestResult = true };

            async Task<GIS.Classes.OrtoDatas?> Read(int? countyId_Read)
            {
                UrlBuilder urlBuilder = new UrlBuilder(path!).AddParameter("reference", reference);
                if (countyId_Read is int countyId_Value)
                {
                    urlBuilder.AddParameter("countyid", countyId_Value);
                }

                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    PostResponse<GIS.Classes.OrtoDatas?> postResponse = await DiGi.WebAPI.Query.GetAsync<GIS.Classes.OrtoDatas>(httpClient, urlBuilder.ToString(), postOptions_Temp);

                    return postResponse is not null && postResponse.Succeeded ? postResponse.Result : null;
                }
                catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
                {
                    Serilog.Modify.Log(exception, "The orthophotos of reference {Reference} could not be read for county {CountyId}", reference!, countyId);
                    return null;
                }
            }

            return await Read(countyId) ?? await Read(null);
        }
    }
}
