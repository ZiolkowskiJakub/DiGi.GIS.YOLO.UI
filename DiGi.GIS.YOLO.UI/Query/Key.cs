using DiGi.Core.Classes;
using System;
using System.IO;
using System.Reflection;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads the API authorization key from the default or specified configuration file path.
        /// </summary>
        /// <param name="path">The optional file path to GIS_WebAPI_Client.conf. If omitted, searches next to the executing assembly or application base directory.</param>
        /// <returns>The API key if found; otherwise, null.</returns>
        public static string? Key(string? path = null)
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

                if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory) || !File.Exists(System.IO.Path.Combine(directory, Constants.FileName.GISWebAPIClientConfigurationFile)))
                {
                    directory = AppDomain.CurrentDomain.BaseDirectory;
                }

                if (!string.IsNullOrWhiteSpace(directory) && Directory.Exists(directory))
                {
                    path = System.IO.Path.Combine(directory, Constants.FileName.GISWebAPIClientConfigurationFile);
                }
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
