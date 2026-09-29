using DiGi.GIS.YOLO.UI.Classes;
using System;
using System.Collections.Generic;
using System.IO;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads the <c>dataset_references.tsv</c> manifest of a YOLO training dataset.
        /// <para>A row that does not parse is skipped rather than failing the file: the manifest is appended to as the build goes, so a stopped run can leave a torn last line, and the building on it is then simply rebuilt. A reference named twice keeps its first row.</para>
        /// </summary>
        /// <param name="path">The path of the manifest.</param>
        /// <returns>The dataset buildings in file order, or null when the file is missing, unreadable, or does not start with <see cref="Constants.Header.DatasetReferences"/>.</returns>
        public static List<DatasetReference>? DatasetReferences(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return null;
            }

            try
            {
                using StreamReader streamReader = new(path!);

                string? header = streamReader.ReadLine();
                if (!string.Equals(header?.TrimEnd(), Constants.Header.DatasetReferences, StringComparison.Ordinal))
                {
                    return null;
                }

                List<DatasetReference> result = [];
                HashSet<string> references = new(StringComparer.Ordinal);

                string? line;
                while ((line = streamReader.ReadLine()) is not null)
                {
                    DatasetReference? datasetReference = Convert.ToDiGi_DatasetReference(line);
                    if (datasetReference?.Reference is not string reference || !references.Add(reference))
                    {
                        continue;
                    }

                    result.Add(datasetReference);
                }

                return result;
            }
            catch (Exception exception)
            {
                Serilog.Modify.Log(exception, "The dataset manifest could not be read - {Path}", path!);
                return null;
            }
        }
    }
}
