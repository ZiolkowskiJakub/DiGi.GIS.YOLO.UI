using System.ComponentModel;

namespace DiGi.GIS.YOLO.UI.Enums
{
    /// <summary>
    /// Names the subset of the Test buildings a detector evaluation row covers.
    /// </summary>
    [Description("YOLODetectorEvaluationSubset")]
    public enum YOLODetectorEvaluationSubset
    {
        /// <summary>
        /// Every Test building with at least one image.
        /// </summary>
        [Description("All")] All = 0,

        /// <summary>
        /// The Test buildings the <c>train8</c> detector cannot have seen - Test and not Legacy. A building whose Legacy decision could not be taken is not in it.
        /// </summary>
        [Description("Clean")] Clean = 1
    }
}
