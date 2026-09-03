using System;
using System.Globalization;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads the running total out of one line of the headless runner's standard output.
        /// <para>The counterpart of <see cref="Create.ProgressMessage(long)"/>. A caller pumping the runner's output passes every line through this and reports the ones that carry a count; a line that is not a progress line - a banner, a note, a tally - answers null rather than zero, so a caller cannot mistake other output for a run that has done nothing.</para>
        /// </summary>
        /// <param name="line">One line of the runner's standard output. Null, empty and unrecognised lines all answer null.</param>
        /// <returns>The running total the line reports, or null when the line is not a progress line.</returns>
        public static long? ProgressCount(string? line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }

            string text = line!.Trim();
            if (!text.StartsWith(Constants.MessagePrefix.Progress, StringComparison.Ordinal))
            {
                return null;
            }

            text = text.Substring(Constants.MessagePrefix.Progress.Length).Trim();

            int start = -1;
            int end = -1;
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsDigit(text[i]))
                {
                    if (start != -1)
                    {
                        break;
                    }

                    continue;
                }

                if (start == -1)
                {
                    start = i;
                }

                end = i;
            }

            if (start == -1)
            {
                return null;
            }

            return long.TryParse(text.Substring(start, end - start + 1), NumberStyles.None, CultureInfo.InvariantCulture, out long count) ? count : null;
        }
    }
}
