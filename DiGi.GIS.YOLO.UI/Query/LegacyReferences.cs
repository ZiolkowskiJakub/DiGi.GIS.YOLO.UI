using System;
using System.Collections.Generic;
using System.IO;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads the building references named by a tab-separated legacy reference list - the regressor training table <c>Data_2025.05.27.tsv</c> from DiGi.GIS.ML.
        /// <para>The column is found by its header, <c>Reference</c>, not by position, so a re-ordered table still reads. References are compared ordinally.</para>
        /// <para>Null - never an empty set - when the file is missing, unreadable or has no <c>Reference</c> column: an empty set would report every held-out building as clean, which is the wrong direction to fail in.</para>
        /// </summary>
        /// <param name="path">The path of the tab-separated file.</param>
        /// <returns>The references the file names, or null when it could not be read.</returns>
        public static HashSet<string>? LegacyReferences(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return null;
            }

            try
            {
                using StreamReader streamReader = new(path!);

                string? header = streamReader.ReadLine();
                if (string.IsNullOrWhiteSpace(header))
                {
                    return null;
                }

                int index = Array.IndexOf(header!.Split('\t'), "Reference");
                if (index < 0)
                {
                    return null;
                }

                HashSet<string> result = new(StringComparer.Ordinal);

                string? line;
                while ((line = streamReader.ReadLine()) is not null)
                {
                    string[] values = line.Split('\t');
                    if (values.Length <= index)
                    {
                        continue;
                    }

                    string reference = values[index].Trim();
                    if (!string.IsNullOrWhiteSpace(reference))
                    {
                        result.Add(reference);
                    }
                }

                return result;
            }
            catch (Exception exception)
            {
                Serilog.Modify.Log(exception, "The legacy reference list could not be read - {Path}", path!);
                return null;
            }
        }
    }
}
