using DiGi.Core.Classes;
using System.IO;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads the API authorization key from the default or specified configuration file path.
        /// </summary>
        /// <param name="path">The optional file path to GIS_WebAPI_Client.conf. If omitted, <see cref="ConfigurationFilePath(string)"/> resolves it against the deployed output.</param>
        /// <returns>The API key if found; otherwise, null.</returns>
        public static string? Key(string? path = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = ConfigurationFilePath(Constants.FileName.GISWebAPIClientConfigurationFile);
            }

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return null;
            }

            ConfigurationFile? configurationFile = Core.Create.ConfigurationFile(path);
            if (configurationFile is null)
            {
                return null;
            }

            if (configurationFile.Dictionary.TryGetValue("Key", out string? key) && !string.IsNullOrWhiteSpace(key))
            {
                return key.Trim('"', ' ', '\t');
            }

            return null;
        }
    }
}
