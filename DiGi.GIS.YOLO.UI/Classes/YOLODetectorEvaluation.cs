using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Enums;
using DiGi.GIS.YOLO.UI.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// How well one detector&apos;s first detection year matches the label year over one subset of the Test buildings.
    /// <para>The first detection year is the first orthophoto year the detector finds the building in - the feature the Year Built regressor leans on most - so comparing it with the label measures the detector without the regressor, which was fitted to the old detector and would penalise any change.</para>
    /// </summary>
    public class YOLODetectorEvaluation : SerializableObject, IGISYOLOUISerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Count))]
        private readonly int count;

        [JsonInclude, JsonPropertyName(nameof(ExactShare))]
        private readonly double exactShare;

        [JsonInclude, JsonPropertyName(nameof(MeanAbsoluteError))]
        private readonly double meanAbsoluteError;

        [JsonInclude, JsonPropertyName(nameof(RootMeanSquareError))]
        private readonly double rootMeanSquareError;

        [JsonInclude, JsonPropertyName(nameof(SHA256))]
        private readonly string? sHA256;

        [JsonInclude, JsonPropertyName(nameof(Subset))]
        private readonly YOLODetectorEvaluationSubset subset;

        [JsonInclude, JsonPropertyName(nameof(WeightsPath))]
        private readonly string? weightsPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLODetectorEvaluation"/> class.
        /// </summary>
        /// <param name="weightsPath">The weights file evaluated.</param>
        /// <param name="sHA256">The SHA-256 of the weights file, in lower-case hexadecimal.</param>
        /// <param name="subset">The subset of the Test buildings the row covers.</param>
        /// <param name="count">The number of buildings in the subset.</param>
        /// <param name="meanAbsoluteError">The mean absolute difference between the first detection year and the label, in years.</param>
        /// <param name="rootMeanSquareError">The root mean square difference between the first detection year and the label, in years.</param>
        /// <param name="exactShare">The share of buildings whose first detection year equals the label.</param>
        public YOLODetectorEvaluation(string? weightsPath, string? sHA256, YOLODetectorEvaluationSubset subset, int count, double meanAbsoluteError, double rootMeanSquareError, double exactShare)
        {
            this.weightsPath = weightsPath;
            this.sHA256 = sHA256;
            this.subset = subset;
            this.count = count;
            this.meanAbsoluteError = meanAbsoluteError;
            this.rootMeanSquareError = rootMeanSquareError;
            this.exactShare = exactShare;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLODetectorEvaluation"/> class by copying an existing one.
        /// </summary>
        /// <param name="yOLODetectorEvaluation">The <see cref="YOLODetectorEvaluation"/> to copy from.</param>
        public YOLODetectorEvaluation(YOLODetectorEvaluation? yOLODetectorEvaluation)
            : base(yOLODetectorEvaluation)
        {
            if (yOLODetectorEvaluation is not null)
            {
                count = yOLODetectorEvaluation.count;
                exactShare = yOLODetectorEvaluation.exactShare;
                meanAbsoluteError = yOLODetectorEvaluation.meanAbsoluteError;
                rootMeanSquareError = yOLODetectorEvaluation.rootMeanSquareError;
                sHA256 = yOLODetectorEvaluation.sHA256;
                subset = yOLODetectorEvaluation.subset;
                weightsPath = yOLODetectorEvaluation.weightsPath;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLODetectorEvaluation"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public YOLODetectorEvaluation(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the number of buildings in the subset. State it beside every metric: the clean subset is much smaller than the full Test set.
        /// </summary>
        [JsonIgnore]
        public int Count
        {
            get
            {
                return count;
            }
        }

        /// <summary>
        /// Gets the share of buildings whose first detection year equals the label, in [0, 1], or 0 for an empty subset - read it with <see cref="Count"/>.
        /// </summary>
        [JsonIgnore]
        public double ExactShare
        {
            get
            {
                return exactShare;
            }
        }

        /// <summary>
        /// Gets the mean absolute difference between the first detection year and the label, in years, or 0 for an empty subset - read it with <see cref="Count"/>.
        /// </summary>
        [JsonIgnore]
        public double MeanAbsoluteError
        {
            get
            {
                return meanAbsoluteError;
            }
        }

        /// <summary>
        /// Gets the root mean square difference between the first detection year and the label, in years, or 0 for an empty subset - read it with <see cref="Count"/>.
        /// </summary>
        [JsonIgnore]
        public double RootMeanSquareError
        {
            get
            {
                return rootMeanSquareError;
            }
        }

        /// <summary>
        /// Gets the SHA-256 of the weights file, in lower-case hexadecimal - the identity of the weights, whatever the file is called.
        /// </summary>
        [JsonIgnore]
        public string? SHA256
        {
            get
            {
                return sHA256;
            }
        }

        /// <summary>
        /// Gets the subset of the Test buildings the row covers.
        /// </summary>
        [JsonIgnore]
        public YOLODetectorEvaluationSubset Subset
        {
            get
            {
                return subset;
            }
        }

        /// <summary>
        /// Gets the weights file evaluated.
        /// </summary>
        [JsonIgnore]
        public string? WeightsPath
        {
            get
            {
                return weightsPath;
            }
        }
    }
}
