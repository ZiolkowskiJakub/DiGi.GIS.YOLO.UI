using DiGi.GIS.YOLO.UI.Classes;
using System;
using System.IO;
using System.Text.Json.Nodes;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads and deserializes the <see cref="YOLOTrainingRunOptions"/> from the specified path or default locations.
        /// <para>The nested <see cref="YOLOTrainingRunOptions.DatasetOptions"/> is written as a plain object, exactly as in a <see cref="YOLOTrainingDatasetOptions"/> file. A member the file does not name keeps the class default, and a key the class does not declare is dropped in silence - so a misspelt flag reads as an unchanged one. The committed template beside the deployed application is the authority on the spelling.</para>
        /// </summary>
        /// <param name="path">The optional file path to YOLOTrainingRunOptions.json. If omitted, <see cref="ConfigurationFilePath(string)"/> resolves it against the deployed output.</param>
        /// <returns>The deserialized options instance, or null if not found or invalid.</returns>
        public static YOLOTrainingRunOptions? YOLOTrainingRunOptions(string? path = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = ConfigurationFilePath(Constants.FileName.YOLOTrainingRunOptions);
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
                    // A hand-written file carries no _type discriminator, which is what the deserializer needs to build a nested
                    // serializable object, so the dataset half is read the way --dataset reads its own file. It is copied out
                    // before the options are built and removed from what they are built from, because the deserializer cannot turn a plain nested object into one.
                    JsonObject? jsonObject_DatasetOptions = jsonObject[nameof(Classes.YOLOTrainingRunOptions.DatasetOptions)] is JsonObject jsonObject_Nested ? JsonNode.Parse(jsonObject_Nested.ToJsonString()) as JsonObject : null;

                    jsonObject.Remove(nameof(Classes.YOLOTrainingRunOptions.DatasetOptions));

                    YOLOTrainingRunOptions yOLOTrainingRunOptions = new(jsonObject);
                    if (jsonObject_DatasetOptions is not null)
                    {
                        yOLOTrainingRunOptions.DatasetOptions = new YOLOTrainingDatasetOptions(jsonObject_DatasetOptions);
                    }

                    return yOLOTrainingRunOptions;
                }
            }
            catch (Exception exception)
            {
                Serilog.Modify.Log(exception, "Failed to read YOLOTrainingRunOptions from '{Path}'", path);
            }

            return null;
        }
    }
}
