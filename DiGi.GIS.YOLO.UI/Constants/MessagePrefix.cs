namespace DiGi.GIS.YOLO.UI.Constants
{
    /// <summary>
    /// Provides the prefixes the headless runner marks its console output with.
    /// <para>The runner's standard output is a contract when it is driven by another process rather than read by a person, and a prefix written as a literal on both sides of the pipe is a contract nothing checks. Naming them here is what lets a change to one of them fail to compile instead of quietly costing a caller its progress reporting.</para>
    /// </summary>
    public static class MessagePrefix
    {
        /// <summary>
        /// Gets the prefix of the line reporting how many items a run has carried through a step.
        /// </summary>
        public const string Progress = "[PROGRESS]";
    }
}
