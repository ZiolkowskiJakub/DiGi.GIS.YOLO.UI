using DiGi.GIS.YOLO.UI.Classes;
using System;
using System.IO;
using System.Text.Json.Nodes;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads and deserializes the <see cref="YearBuiltPredictionPipelineOptions"/> from the specified path or default locations.
        /// <para>A member the file does not name keeps the class default, and a key the class does not declare is dropped in silence - so a misspelt flag reads as an unchanged one. The committed template beside the deployed application is the authority on the spelling.</para>
        /// </summary>
        /// <param name="path">The optional file path to YearBuiltPredictionPipelineOptions.json. If omitted, <see cref="ConfigurationFilePath(string)"/> resolves it against the deployed output.</param>
        /// <returns>The deserialized options instance, or null if not found or invalid.</returns>
        public static YearBuiltPredictionPipelineOptions? YearBuiltPredictionPipelineOptions(string? path = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = ConfigurationFilePath(Constants.FileName.YearBuiltPredictionPipelineOptions);
            }

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return null;
            }

            try
            {
                string json = File.ReadAllText(path);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return null;
                }

                JsonNode? jsonNode = JsonNode.Parse(json);
                if (jsonNode is JsonObject jsonObject)
                {
                    return new YearBuiltPredictionPipelineOptions(jsonObject);
                }
            }
            catch (Exception exception)
            {
                Serilog.Modify.Log(exception, "Failed to read YearBuiltPredictionPipelineOptions from '{Path}'", path);
            }

            return null;
        }
    }
}
