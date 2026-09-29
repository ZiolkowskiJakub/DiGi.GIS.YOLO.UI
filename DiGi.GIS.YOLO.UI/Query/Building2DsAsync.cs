using DiGi.GIS.Classes;
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
        /// Reads the stored footprints of the named references, by reference.
        /// <para>The read is bulk and paged at <paramref name="referenceBatchSize"/>. The endpoint has no <c>fallbackbyreference</c> parameter: it falls back to a lookup by reference alone only when no county is sent. So the county is sent first - it is the cheap, partition-pruned read - and the references it did not answer are asked for once more without it, which finds a footprint filed under a sibling polygon part of the county.</para>
        /// <para>A page that cannot be read is logged and skipped; its references are simply absent from the result, and the caller counts them as buildings without a footprint.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="countyId">The identifier of the county part the references belong to.</param>
        /// <param name="references">The building references to read.</param>
        /// <param name="referenceBatchSize">The number of references read in one request, at least 1.</param>
        /// <param name="postOptions">Optional configuration options for the requests.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task returning the footprints by reference, or null when the client could not be built.</returns>
        public static async Task<Dictionary<string, Building2D>?> Building2DsAsync(this GISWebAPIManager? gisWebAPIManager, int countyId, IEnumerable<string>? references, int referenceBatchSize = Constants.Count.BuildingDataReference_Maximum, PostOptions? postOptions = null, CancellationToken cancellationToken = default)
        {
            if (gisWebAPIManager is null)
            {
                return null;
            }

            HttpClient? httpClient = gisWebAPIManager.CreateHttpClient<Building2DController>(nameof(Building2DController.GetItemsByReferencesAsync), out string? path);
            if (httpClient is null || string.IsNullOrWhiteSpace(path))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "HttpClient or path for {Method} could not be resolved", nameof(Building2DController.GetItemsByReferencesAsync));
                return null;
            }

            Dictionary<string, Building2D> result = new(StringComparer.Ordinal);

            List<string> references_Temp = [];
            if (references is not null)
            {
                HashSet<string> references_Unique = new(StringComparer.Ordinal);
                foreach (string reference in references)
                {
                    if (!string.IsNullOrWhiteSpace(reference) && references_Unique.Add(reference))
                    {
                        references_Temp.Add(reference);
                    }
                }
            }

            if (references_Temp.Count == 0)
            {
                return result;
            }

            referenceBatchSize = referenceBatchSize < 1 ? 1 : referenceBatchSize;

            PostOptions postOptions_Temp = postOptions ?? new PostOptions() { RequestResult = true, Delay = TimeSpan.FromSeconds(60) };

            async Task Read(List<string> references_Read, int? countyId_Read)
            {
                UrlBuilder urlBuilder = new(path!);
                if (countyId_Read is int countyId_Value)
                {
                    urlBuilder.AddParameter("countyid", countyId_Value);
                }

                string requestUri = urlBuilder.ToString();

                for (int i = 0; i < references_Read.Count; i += referenceBatchSize)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    List<string> references_Page = references_Read.GetRange(i, Math.Min(referenceBatchSize, references_Read.Count - i));

                    List<Building2D>? building2Ds;
                    try
                    {
                        // Passed as a factory, so a retry rebuilds the body rather than resending a drained stream.
                        PostResponse<List<Building2D>?> postResponse = await DiGi.WebAPI.Modify.PostAsync<List<Building2D>>(httpClient, requestUri, () => GIS.WebAPI.Create.HttpContent(references_Page, cancellationToken), postOptions_Temp);

                        building2Ds = postResponse is not null && postResponse.Succeeded ? postResponse.Result : null;
                    }
                    catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
                    {
                        Serilog.Modify.Log(exception, "The footprints could not be read for county {CountyId} over {Count} references - the page is skipped", countyId, references_Page.Count);
                        continue;
                    }

                    if (building2Ds is null)
                    {
                        continue;
                    }

                    foreach (Building2D building2D in building2Ds)
                    {
                        string? reference = building2D?.Reference;
                        if (string.IsNullOrWhiteSpace(reference) || result.ContainsKey(reference!))
                        {
                            continue;
                        }

                        result[reference!] = building2D!;
                    }
                }
            }

            await Read(references_Temp, countyId);

            List<string> references_Missing = references_Temp.FindAll(x => !result.ContainsKey(x));
            if (references_Missing.Count != 0)
            {
                await Read(references_Missing, null);
            }

            return result;
        }
    }
}
