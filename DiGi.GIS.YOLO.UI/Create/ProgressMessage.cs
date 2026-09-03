using System.Globalization;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Create
    {
        /// <summary>
        /// Builds the line the headless runner writes to report how far a run has got.
        /// <para>The line is read back by <see cref="Query.ProgressCount(string)"/>, so both sides of the pipe are built from this one method rather than from a format literal written twice. A caller watching the runner's standard output has no other way to learn what a long run is doing.</para>
        /// <para>Invariant culture, because the reader is a machine: a thousands separator taken from the machine's own settings would make the count unparseable on exactly the machines that use one.</para>
        /// </summary>
        /// <param name="count">The running total of items the run has carried through a step.</param>
        /// <returns>The progress line, without a trailing line break.</returns>
        public static string ProgressMessage(long count)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} Processed {1} items...", Constants.MessagePrefix.Progress, count);
        }
    }
}
