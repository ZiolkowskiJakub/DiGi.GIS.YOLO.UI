using System;
using System.Collections.Generic;
using System.IO;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Lists the building references whose orthophoto images are in a prediction image folder - the buildings a detector run over that folder has scored, whether it fired on them or not.
        /// <para>The detector's results name only the buildings it fired on, so they cannot say which buildings were looked at and found empty. The image folder can: every <c>{reference}_{year}.jpeg</c> in it was handed to the detector. The detection write needs exactly that set to clear what an earlier detector left on a building the current one does not fire on (ZiolkowskiJakub/DiGi.GIS.YOLO.UI#21).</para>
        /// <para>Names are split by <see cref="TryParseImageFileName(string?, out string?, out short)"/>; a file that does not parse is not a scored building and is skipped.</para>
        /// </summary>
        /// <param name="directory">The folder the detector ran over.</param>
        /// <returns>The references, compared ordinally; empty when the folder is missing or holds no image.</returns>
        public static HashSet<string> PredictionImageReferences(string? directory)
        {
            HashSet<string> result = new(StringComparer.Ordinal);

            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return result;
            }

            foreach (string path in Directory.EnumerateFiles(directory, "*.jpeg"))
            {
                if (TryParseImageFileName(path, out string? reference, out _) && reference is not null)
                {
                    result.Add(reference);
                }
            }

            return result;
        }
    }
}
