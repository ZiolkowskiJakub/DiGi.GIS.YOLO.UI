using System;
using System.IO;
using System.Reflection;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Resolves where a deployed configuration file of the given name sits.
        /// <para>Both copy targets flatten into the output root - <c>CopyUserFiles</c> runs after <c>CopyFiles</c>, so a secret in the git-ignored <c>user files</c> folder overwrites the committed default of the same name - which is why only the output root is probed. A <c>bin\user files</c> folder is never produced, so looking for one would read as a working fallback while finding nothing.</para>
        /// </summary>
        /// <param name="fileName">The name of the configuration file, without a directory.</param>
        /// <returns>The full path the file would have, whether or not it exists, or null when neither directory can be resolved or no name was given.</returns>
        public static string? ConfigurationFilePath(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            string? directory = null;
            try
            {
                string? location = Assembly.GetExecutingAssembly().Location;
                if (!string.IsNullOrWhiteSpace(location))
                {
                    directory = Path.GetDirectoryName(location);
                }
            }
            catch
            {
                // An assembly bundled into a single file application reports no location. The application base
                // directory below answers for it.
            }

            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory) || !File.Exists(Path.Combine(directory, fileName!)))
            {
                directory = AppDomain.CurrentDomain.BaseDirectory;
            }

            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return null;
            }

            return Path.Combine(directory, fileName!);
        }
    }
}
