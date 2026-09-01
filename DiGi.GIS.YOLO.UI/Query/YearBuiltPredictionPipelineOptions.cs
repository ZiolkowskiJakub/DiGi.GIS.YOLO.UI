using DiGi.GIS.YOLO.UI.Classes;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Nodes;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads and deserializes the <see cref="YearBuiltPredictionPipelineOptions"/> from the specified path or default locations.
        /// </summary>
        /// <param name="path">The optional file path to YearBuiltPredictionPipelineOptions.json. If omitted, searches next to the executing assembly or application base directory.</param>
        /// <returns>The deserialized options instance, or null if not found or invalid.</returns>
        public static YearBuiltPredictionPipelineOptions? YearBuiltPredictionPipelineOptions(string? path = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                string? directory = null;
                try
                {
                    string? location = Assembly.GetExecutingAssembly().Location;
                    if (!string.IsNullOrWhiteSpace(location))
                    {
                        directory = System.IO.Path.GetDirectoryName(location);
                    }
                }
                catch
                {
                }

                if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory) || !File.Exists(System.IO.Path.Combine(directory, Constants.FileName.YearBuiltPredictionPipelineOptions)))
                {
                    directory = AppDomain.CurrentDomain.BaseDirectory;
                }

                if (!string.IsNullOrWhiteSpace(directory) && Directory.Exists(directory))
                {
                    path = System.IO.Path.Combine(directory, Constants.FileName.YearBuiltPredictionPipelineOptions);
                }
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
