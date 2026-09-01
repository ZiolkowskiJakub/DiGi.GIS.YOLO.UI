using DiGi.GIS.Classes;
using DiGi.GIS.WebAPI.Classes;
using DiGi.WebAPI.Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads each building&apos;s stored year built data and adds the run&apos;s predicted construction year to it.
        /// <para>The stored entry is read back rather than a fresh one built, because the year built table addresses a stored object by its own identifier: a datum built fresh carries a new one and is stored <i>alongside</i> whatever the building already holds instead of replacing it. Reading it back is also what preserves the history and any user-supplied year.</para>
        /// <para>A building with nothing stored yet gets a new datum, which is the one case where a fresh identifier is correct.</para>
        /// <para>Every prediction of one run carries the same stamp. The stored entries are keyed by it, so one stamp per run leaves one history entry per run, and re-running with the same stamp replaces that entry rather than adding to it.</para>
        /// <para>There is no bulk read for this table - the endpoint answers one reference at a time - so the reads are issued in bounded batches rather than all at once.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="countyId">The identifier of the county row the references belong to.</param>
        /// <param name="years">The predicted construction year of each building, by reference.</param>
        /// <param name="runTimestamp">The stamp every prediction of this run carries.</param>
        /// <param name="readStored">When true, each building&apos;s stored entry is read back first so the prediction is added to its history. Set it false only when the caller is not storing the year built data at all - a county is tens of thousands of buildings and this is a request each, while the building data column is derived from the latest prediction, which a fresh entry already carries.</param>
        /// <param name="maxConcurrentRequests">The maximum number of concurrent WebAPI requests allowed while reading. Defaults to 8.</param>
        /// <param name="postOptions">Optional configuration options for the requests.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task returning the year built data to store, each carrying its building&apos;s history plus this run&apos;s prediction.</returns>
        public static async Task<List<YearBuiltData>> YearBuiltDatasAsync(
            this GISWebAPIManager? gisWebAPIManager,
            int countyId,
            IDictionary<string, short>? years,
            DateTimeOffset runTimestamp,
            bool readStored = true,
            int maxConcurrentRequests = 8,
            PostOptions? postOptions = null,
            CancellationToken cancellationToken = default)
        {
            List<YearBuiltData> result = [];

            if (gisWebAPIManager is null || years is null || years.Count == 0)
            {
                return result;
            }

            HttpClient? httpClient = null;
            string? path = null;

            if (readStored)
            {
                httpClient = gisWebAPIManager.CreateHttpClient<YearBuiltDataController>(nameof(YearBuiltDataController.GetItemsByReferenceAsync), out path);
                if (httpClient is null || string.IsNullOrWhiteSpace(path))
                {
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "HttpClient or path for {Method} could not be resolved", nameof(YearBuiltDataController.GetItemsByReferenceAsync));
                    return result;
                }
            }

            PostOptions postOptions_Temp = postOptions ?? new PostOptions() { RequestResult = true };

            // The predicted year is stored as a short while the model and the column speak in ushort, so the
            // caller has already narrowed it; nothing here can widen it back.
            DateTime dateTime = runTimestamp.UtcDateTime;

            List<string> references = [.. years.Keys];

            int concurrencyLimit = maxConcurrentRequests < 1 ? 1 : maxConcurrentRequests;

            ConcurrentBag<YearBuiltData> yearBuiltDatas = [];

            for (int i = 0; i < references.Count; i += concurrencyLimit)
            {
                cancellationToken.ThrowIfCancellationRequested();

                int count = Math.Min(concurrencyLimit, references.Count - i);
                List<Task> tasks = [];

                for (int j = 0; j < count; j++)
                {
                    string reference = references[i + j];
                    short year = years[reference];

                    tasks.Add(Task.Run(async () =>
                    {
                        YearBuiltData? yearBuiltData = null;

                        // With no history being stored there is no history to preserve, and a county is tens of
                        // thousands of buildings against one request each.
                        if (readStored)
                        {
                            try
                            {
                                cancellationToken.ThrowIfCancellationRequested();

                                string requestUri = new UrlBuilder(path!).AddParameter("reference", reference).AddParameter("countyid", countyId).ToString();

                                PostResponse<List<YearBuiltData>?> postResponse = await DiGi.WebAPI.Query.GetAsync<List<YearBuiltData>>(httpClient, requestUri, postOptions_Temp);
                                if (postResponse is not null && postResponse.Succeeded && postResponse.Result is List<YearBuiltData> yearBuiltDatas_Stored && yearBuiltDatas_Stored.Count != 0)
                                {
                                    yearBuiltData = yearBuiltDatas_Stored[0];
                                }
                            }
                            catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
                            {
                                // A building with nothing stored is answered with no content rather than an
                                // error, so reaching here means the read itself failed. A fresh datum is still
                                // written - it stores the prediction, and the worst it can cost is a second
                                // stored object for a building whose existing one could not be read.
                                Serilog.Modify.Log(exception, "The stored year built data could not be read for reference {Reference} in county {CountyId}", reference, countyId);
                            }
                        }

                        yearBuiltData ??= new YearBuiltData(reference);

                        if (yearBuiltData.SetPredictedYearBuilt(dateTime, year))
                        {
                            yearBuiltDatas.Add(yearBuiltData);
                        }
                    }, cancellationToken));
                }

                await Task.WhenAll(tasks);
            }

            result.AddRange(yearBuiltDatas);

            return result;
        }
    }
}
