using System.ComponentModel;

namespace DiGi.GIS.YOLO.UI.Enums
{
    /// <summary>
    /// Names which record says a building may have been seen by the <c>train8</c> detector, whose dataset is lost.
    /// <para>Two independent records are consulted and a building is Legacy when either says so - the union is conservative, so doubt removes a building from the clean evaluation subset rather than adding one. Anything other than <see cref="None"/> is Legacy, and <see cref="Unknown"/> is Legacy too: a building whose history could not be read is never reported as clean by default.</para>
    /// </summary>
    [Description("LegacySource")]
    public enum LegacySource
    {
        /// <summary>
        /// The history could not be read, so the decision was not taken. Treated as Legacy.
        /// </summary>
        [Description("unknown")] Unknown = -1,

        /// <summary>
        /// Neither record places the building in the <c>train8</c> dataset.
        /// </summary>
        [Description("")] None = 0,

        /// <summary>
        /// Only the legacy reference list - the regressor training table built days after <c>train8</c> - names the building.
        /// </summary>
        [Description("tsv")] Tsv = 1,

        /// <summary>
        /// Only the stored history names the building: it carries a user entry that is undated or dated before the cut-off.
        /// </summary>
        [Description("timestamp")] Timestamp = 2,

        /// <summary>
        /// Both records name the building.
        /// </summary>
        [Description("both")] Both = 3
    }
}
