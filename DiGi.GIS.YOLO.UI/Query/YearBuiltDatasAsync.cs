using DiGi.GIS.Classes;
using DiGi.GIS.WebAPI.Classes;
using DiGi.WebAPI.Classes;
using System;
using System.Collections.Generic;
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
        /// <para>The read is bulk: the endpoint answers up to <see cref="Constants.Count.YearBuiltDataReference_Maximum"/> references in one request, so the references are paged at that size and a page is the unit that succeeds or fails. A page that cannot be read is skipped rather than answered with a fresh datum for every building of it, because that would store a second row alongside the one that could not be read.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="countyId">The identifier of the county row the references belong to.</param>
        /// <param name="years">The predicted construction year of each building, by reference.</param>
        /// <param name="runTimestamp">The stamp every prediction of this run carries.</param>
        /// <param name="modelId">The identity of the predictor that produced the years - normally the SHA-256 of its model file, or a rule name for a heuristic - stamped on every prediction as <see cref="PredictedYearBuilt.ModelId"/>, or null when it is not known (ZiolkowskiJakub/DiGi.GIS.YOLO.UI#27).</param>
        /// <param name="readStored">When true, each building&apos;s stored entry is read back first so the prediction is added to its history. Set it false only when the caller is not storing the year built data at all - a county is tens of thousands of buildings, and the building data column is derived from the latest prediction, which a fresh entry already carries.</param>
        /// <param name="referenceBatchSize">The number of references read in one request, at most <see cref="Constants.Count.YearBuiltDataReference_Maximum"/> - the endpoint&apos;s cap.</param>
        /// <param name="postOptions">Optional configuration options for the requests.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task returning the year built data to store, each carrying its building&apos;s history plus this run&apos;s prediction.</returns>
        public static async Task<List<YearBuiltData>> YearBuiltDatasAsync(
            this GISWebAPIManager? gisWebAPIManager,
            int countyId,
            IDictionary<string, short>? years,
            DateTimeOffset runTimestamp,
            string? modelId,
            bool readStored = true,
            int referenceBatchSize = Constants.Count.YearBuiltDataReference_Maximum,
            PostOptions? postOptions = null,
            CancellationToken cancellationToken = default)
        {
            List<YearBuiltData> result = [];

            if (gisWebAPIManager is null || years is null || years.Count == 0)
            {
                return result;
            }

            // The predicted year is stored as a short while the model and the column speak in ushort, so the
            // caller has already narrowed it; nothing here can widen it back.
            DateTime dateTime = runTimestamp.UtcDateTime;

            List<string> references = [.. years.Keys];

            if (readStored)
            {
                // A page is the unit that succeeds or fails. Its buildings are skipped rather than answered with a
                // fresh datum - that would store a second row alongside the one that could not be read - so they
                // carry no prediction this run, and a re-run merges them.
                HashSet<string> references_Failed = new(StringComparer.Ordinal);

                Dictionary<string, List<YearBuiltData>>? yearBuiltDatas_ByReference = await StoredYearBuiltDatasAsync(gisWebAPIManager, countyId, references, referenceBatchSize, references_Failed, postOptions, cancellationToken);
                if (yearBuiltDatas_ByReference is null)
                {
                    return result;
                }

                foreach (string reference in references)
                {
                    if (references_Failed.Contains(reference))
                    {
                        continue;
                    }

                    // A reference can carry several stored rows; the first is the one the read returned first,
                    // as the singular read took it.
                    YearBuiltData yearBuiltData = yearBuiltDatas_ByReference.TryGetValue(reference, out List<YearBuiltData>? yearBuiltDatas_Stored) && yearBuiltDatas_Stored is not null && yearBuiltDatas_Stored.Count != 0
                        ? yearBuiltDatas_Stored[0]
                        : new YearBuiltData(reference);

                    if (yearBuiltData.SetPredictedYearBuilt(dateTime, years[reference], modelId))
                    {
                        result.Add(yearBuiltData);
                    }
                }
            }
            else
            {
                // With no history being stored there is no history to preserve, and a county is tens of
                // thousands of buildings - nothing here is a request each, but nothing is read at all either.
                foreach (string reference in references)
                {
                    YearBuiltData yearBuiltData = new(reference);
                    if (yearBuiltData.SetPredictedYearBuilt(dateTime, years[reference], modelId))
                    {
                        result.Add(yearBuiltData);
                    }
                }
            }

            return result;
        }
    }
}
