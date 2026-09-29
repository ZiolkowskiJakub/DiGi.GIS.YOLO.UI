using System.ComponentModel;

namespace DiGi.GIS.YOLO.UI.Enums
{
    /// <summary>
    /// Names one step of the <c>--train</c> console mode. The steps always run in the order they are declared here, whatever order <see cref="Classes.YOLOTrainingRunOptions.Steps"/> lists them in.
    /// </summary>
    [Description("YOLOTrainingStep")]
    public enum YOLOTrainingStep
    {
        /// <summary>
        /// Builds the training dataset, or appends to the folder that already exists.
        /// </summary>
        [Description("Dataset")] Dataset = 0,

        /// <summary>
        /// Checks the labels of the dataset against the detector named by the dataset options.
        /// </summary>
        [Description("LabelCheck")] LabelCheck = 1,

        /// <summary>
        /// Trains a detector from the start weights.
        /// </summary>
        [Description("Train")] Train = 2,

        /// <summary>
        /// Validates the trained weights on the Test split of the dataset.
        /// </summary>
        [Description("Validate")] Validate = 3,

        /// <summary>
        /// Scores every weights file of the dataset options, and the trained weights, on the Test buildings.
        /// </summary>
        [Description("Evaluate")] Evaluate = 4
    }
}
