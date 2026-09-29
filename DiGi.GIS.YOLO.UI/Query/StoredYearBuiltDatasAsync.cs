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
        /// Reads every stored year built datum of the named references, grouped by reference.
        /// <para><b>All</b> rows of a reference are kept, in the order the read returned them. One reference can carry several stored rows - one per user among them - and a rule that looks at the history, such as the <c>train8</c> Legacy rule, has to see every one; a caller that wants a single datum takes the first, as <see cref="YearBuiltDatasAsync"/> does.</para>
        /// <para>The read is bulk and paged at <paramref name="referenceBatchSize"/>, at most <see cref="Constants.Count.YearBuiltDataReference_Maximum"/> - the endpoint&apos;s cap. <c>fallbackbyreference=true</c> is sent explicitly, because the endpoint defaults it off and without it a row filed under a sibling polygon part of the county is not returned. The request body is passed as a factory, so a retry of a page rebuilds it rather than resending a drained stream.</para>
        /// <para>A page is the unit that succeeds or fails. The references of a page that could not be read are added to <paramref name="references_Failed"/> and are absent from the result - a caller must not read their absence as "nothing stored".</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="countyId">The identifier of the county row the references belong to.</param>
        /// <param name="references">The building references to read.</param>
        /// <param name="referenceBatchSize">The number of references read in one request, clamped to [1, <see cref="Constants.Count.YearBuiltDataReference_Maximum"/>].</param>
        /// <param name="references_Failed">An optional collection the references of every page that could not be read are added to.</param>
        /// <param name="postOptions">Optional configuration options for the requests.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task returning the stored rows by reference - a reference with nothing stored is absent - or null when the client could not be built.</returns>
        public static async Task<Dictionary<string, List<YearBuiltData>>?> StoredYearBuiltDatasAsync(this GISWebAPIManager? gisWebAPIManager, int countyId, IEnumerable<string>? references, int referenceBatchSize = Constants.Count.YearBuiltDataReference_Maximum, ICollection<string>? references_Failed = null, PostOptions? postOptions = null, CancellationToken cancellationToken = default)
        {
            if (gisWebAPIManager is null)
            {
                return null;
            }

            HttpClient? httpClient = gisWebAPIManager.CreateHttpClient<YearBuiltDataController>(nameof(YearBuiltDataController.GetItemsByReferencesAsync), out string? path);
            if (httpClient is null || string.IsNullOrWhiteSpace(path))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "HttpClient or path for {Method} could not be resolved", nameof(YearBuiltDataController.GetItemsByReferencesAsync));
                return null;
            }

            Dictionary<string, List<YearBuiltData>> result = new(StringComparer.Ordinal);

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

            // The endpoint refuses more than its cap in one request, so a larger page would fail the whole
            // county rather than just being slower.
            referenceBatchSize = referenceBatchSize < 1 ? 1 : Math.Min(referenceBatchSize, Constants.Count.YearBuiltDataReference_Maximum);

            PostOptions postOptions_Temp = postOptions ?? new PostOptions() { RequestResult = true };

            // Sent explicitly, not left to the server default: the bulk endpoint defaults it off. An omitted
            // parameter is not a binding failure - it keeps the default - and without the flag a stored row filed
            // under a sibling polygon part is no longer read back, which is what strands a duplicate.
            string requestUri = new UrlBuilder(path!).AddParameter("countyid", countyId).AddParameter("fallbackbyreference", true).ToString();

            for (int i = 0; i < references_Temp.Count; i += referenceBatchSize)
            {
                cancellationToken.ThrowIfCancellationRequested();

                List<string> references_Page = references_Temp.GetRange(i, Math.Min(referenceBatchSize, references_Temp.Count - i));

                List<YearBuiltData>? yearBuiltDatas_Stored;
                try
                {
                    // Passed as a factory, not an instance: sending consumes and disposes the content, so a retry of
                    // the page has to rebuild the body rather than resend an already-drained stream.
                    PostResponse<List<YearBuiltData>?> postResponse = await DiGi.WebAPI.Modify.PostAsync<List<YearBuiltData>>(httpClient, requestUri, () => GIS.WebAPI.Create.HttpContent(references_Page, cancellationToken), postOptions_Temp);

                    if (postResponse is null || !postResponse.Succeeded)
                    {
                        throw new Exception("The bulk read did not succeed");
                    }

                    yearBuiltDatas_Stored = postResponse.Result;
                }
                catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
                {
                    Serilog.Modify.Log(exception, "The stored year built data could not be read for county {CountyId} over {Count} references - the page is skipped", countyId, references_Page.Count);

                    if (references_Failed is not null)
                    {
                        foreach (string reference_Page in references_Page)
                        {
                            references_Failed.Add(reference_Page);
                        }
                    }

                    continue;
                }

                if (yearBuiltDatas_Stored is null)
                {
                    continue;
                }

                foreach (YearBuiltData yearBuiltData_Stored in yearBuiltDatas_Stored)
                {
                    string? reference_Stored = yearBuiltData_Stored?.Reference;
                    if (string.IsNullOrWhiteSpace(reference_Stored))
                    {
                        continue;
                    }

                    if (!result.TryGetValue(reference_Stored!, out List<YearBuiltData>? yearBuiltDatas) || yearBuiltDatas is null)
                    {
                        yearBuiltDatas = [];
                        result[reference_Stored!] = yearBuiltDatas;
                    }

                    yearBuiltDatas.Add(yearBuiltData_Stored!);
                }
            }

            return result;
        }
    }
}
