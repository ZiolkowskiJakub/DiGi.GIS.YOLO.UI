using System;
using System.IO;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Resolves the absolute path to the YOLO model file from the specified path or standard deployment locations.
        /// </summary>
        /// <param name="modelPath">The configured model path, which may be relative to the application directory or user files directory.</param>
        /// <returns>The resolved absolute path if the model file exists; otherwise, the normalized path or null.</returns>
        public static string? ModelPath(string? modelPath)
        {
            if (string.IsNullOrWhiteSpace(modelPath))
            {
                return null;
            }

            if (File.Exists(modelPath))
            {
                return Path.GetFullPath(modelPath);
            }

            string path_BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, modelPath);
            if (File.Exists(path_BaseDirectory))
            {
                return Path.GetFullPath(path_BaseDirectory);
            }

            if (modelPath.StartsWith("user files/", StringComparison.OrdinalIgnoreCase) || modelPath.StartsWith("user files\\", StringComparison.OrdinalIgnoreCase))
            {
                string stripped = modelPath.Substring("user files/".Length);

                string path_BaseStripped = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, stripped);
                if (File.Exists(path_BaseStripped))
                {
                    return Path.GetFullPath(path_BaseStripped);
                }

                string path_CwdStripped = Path.Combine(Environment.CurrentDirectory, stripped);
                if (File.Exists(path_CwdStripped))
                {
                    return Path.GetFullPath(path_CwdStripped);
                }
            }
            else
            {
                string path_UserFiles = Path.Combine(Environment.CurrentDirectory, "user files", modelPath);
                if (File.Exists(path_UserFiles))
                {
                    return Path.GetFullPath(path_UserFiles);
                }
            }

            try
            {
                return Path.GetFullPath(modelPath);
            }
            catch
            {
                return modelPath;
            }
        }
    }
}
