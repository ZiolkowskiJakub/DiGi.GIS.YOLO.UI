using System.ComponentModel;

namespace DiGi.GIS.YOLO.UI.Enums
{
    /// <summary>
    /// Names what the exit code of the headless Year Built prediction runner means.
    /// <para>The runner is started by other processes - the tray application's background task among them - and an exit code is the whole of what they get back. Naming the codes here rather than writing integers on both sides is what keeps a caller's reading of a run and the runner's own verdict from drifting apart: a caller comparing against a literal cannot fail to compile when a code changes meaning.</para>
    /// <para>Anything other than <see cref="Succeeded"/> means no run finished. <see cref="Cancelled"/> is not a failure of the pipeline, and <see cref="Environment"/> is deliberately separate from <see cref="Failed"/> - a machine that cannot start the detector at all is a different thing to fix than a step that failed while running.</para>
    /// </summary>
    [Description("YearBuiltPredictionExitCode")]
    public enum YearBuiltPredictionExitCode
    {
        /// <summary>
        /// The pipeline ran and every step it was asked for completed.
        /// </summary>
        [Description("Pipeline executed successfully")] Succeeded = 0,

        /// <summary>
        /// The options could not be loaded, or they name no county or no scratch directory. Nothing was attempted.
        /// </summary>
        [Description("Configuration or argument validation error")] Configuration = 1,

        /// <summary>
        /// The preflight found that this machine cannot run the detector - no CPython carrying ultralytics, or no weights. Nothing was exported.
        /// </summary>
        [Description("Preflight environment check failed")] Environment = 2,

        /// <summary>
        /// The Web API authorization key is missing, or the client could not be built from it. Nothing was read or written.
        /// </summary>
        [Description("WebAPI key or client configuration missing")] Authorization = 3,

        /// <summary>
        /// The run started and one or more of its steps did not complete. What it managed to write is written.
        /// </summary>
        [Description("Pipeline execution failure")] Failed = 4,

        /// <summary>
        /// The run was stopped before it finished. What it had already written is committed.
        /// </summary>
        [Description("Execution cancelled by user")] Cancelled = 5
    }
}
