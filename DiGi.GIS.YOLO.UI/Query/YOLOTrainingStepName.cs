using DiGi.GIS.YOLO.UI.Enums;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Gives the name under which a failed step of the <c>--train</c> mode is listed in <see cref="Classes.YOLOTrainingRunResult.FailedStepNames"/>.
        /// <para>The name is prefixed with the enumeration, because the dataset and evaluation steps list their own failures by bare names such as <c>Train</c> and <c>Test</c> (the dataset splits), and a step called <c>Train</c> would be read as one of those.</para>
        /// </summary>
        /// <param name="yOLOTrainingStep">The step.</param>
        /// <returns>The name, such as <c>YOLOTrainingStep.Train</c>.</returns>
        public static string YOLOTrainingStepName(YOLOTrainingStep yOLOTrainingStep)
        {
            return string.Concat(nameof(YOLOTrainingStep), ".", yOLOTrainingStep.ToString());
        }
    }
}
