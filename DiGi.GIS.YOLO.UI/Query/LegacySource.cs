using DiGi.GIS.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Decides whether a building may have been seen by the <c>train8</c> detector, and which record says so.
        /// <para>The <c>train8</c> dataset is lost, so which buildings it learned from is reconstructed from two independent records, and the building is Legacy when <b>either</b> places it there:</para>
        /// <para>- the legacy reference list (<c>Data_2025.05.27.tsv</c>, built from the same sources days after <c>train8</c>) names it;</para>
        /// <para>- its stored history carries a user entry - of <b>any</b> relation, exact or bounded - that is undated or dated before <paramref name="cutoff"/>. Legacy entries were stored without a timestamp, and <c>train8</c> saw a building&apos;s images whatever year it was labelled with, so any user entry counts, not only the one that decides the label. Every row of every stored datum is looked at, because one reference can carry several.</para>
        /// <para>A pure function, so the rule is testable without the Web API.</para>
        /// </summary>
        /// <param name="legacyReferences">The references the legacy reference list names, compared ordinally. Null means no list, not an empty one - the caller refuses to run without it.</param>
        /// <param name="reference">The building reference.</param>
        /// <param name="yearBuiltDatas">Every stored year built datum of the building, or null when its history was not read. Null answers only from the list.</param>
        /// <param name="cutoff">The moment <c>train8</c> was saved. An entry dated on or after it cannot have been seen.</param>
        /// <returns>The record(s) placing the building in the <c>train8</c> dataset, or <see cref="Enums.LegacySource.None"/> when neither does.</returns>
        public static Enums.LegacySource LegacySource(ISet<string>? legacyReferences, string? reference, IEnumerable<YearBuiltData>? yearBuiltDatas, DateTimeOffset cutoff)
        {
            bool tsv = !string.IsNullOrWhiteSpace(reference) && legacyReferences is not null && legacyReferences.Contains(reference!);

            bool timestamp = false;
            if (yearBuiltDatas is not null)
            {
                foreach (YearBuiltData yearBuiltData in yearBuiltDatas)
                {
                    UserYearBuilt? userYearBuilt = yearBuiltData?.GetUserYearBuilt();
                    if (userYearBuilt is null)
                    {
                        continue;
                    }

                    if (userYearBuilt.DateTime is not DateTimeOffset dateTimeOffset || dateTimeOffset < cutoff)
                    {
                        timestamp = true;
                        break;
                    }
                }
            }

            if (tsv && timestamp)
            {
                return Enums.LegacySource.Both;
            }

            if (tsv)
            {
                return Enums.LegacySource.Tsv;
            }

            if (timestamp)
            {
                return Enums.LegacySource.Timestamp;
            }

            return Enums.LegacySource.None;
        }
    }
}
