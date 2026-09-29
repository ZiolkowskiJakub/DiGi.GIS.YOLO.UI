using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// What one detector evaluation found: one row per weights file per subset of the Test buildings, all run over the same images.
    /// <para><see cref="FailedStepNames"/> says whether every weights file was scored. A weights file that cannot be run is reported and stepped over, so the rows of the others still come back.</para>
    /// </summary>
    public class YOLODetectorEvaluationResult : SerializableResult, IGISYOLOUISerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(End))]
        private readonly DateTimeOffset? end;

        [JsonInclude, JsonPropertyName(nameof(FailedStepNames))]
        private readonly List<string> failedStepNames = [];

        [JsonInclude, JsonPropertyName(nameof(Messages))]
        private readonly List<string> messages = [];

        [JsonInclude, JsonPropertyName(nameof(OutputDirectory))]
        private readonly string? outputDirectory;

        [JsonInclude, JsonPropertyName(nameof(Start))]
        private readonly DateTimeOffset? start;

        [JsonInclude, JsonPropertyName(nameof(YOLODetectorEvaluations))]
        private readonly List<YOLODetectorEvaluation> yOLODetectorEvaluations = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLODetectorEvaluationResult"/> class.
        /// </summary>
        /// <param name="outputDirectory">The root directory of the dataset evaluated.</param>
        /// <param name="yOLODetectorEvaluations">The rows, one per weights file per subset, or null for none.</param>
        /// <param name="failedStepNames">The steps that reported a failure, or null for none.</param>
        /// <param name="messages">What the run has to say beyond its rows, or null for nothing.</param>
        /// <param name="start">When the run started.</param>
        /// <param name="end">When the run ended.</param>
        public YOLODetectorEvaluationResult(string? outputDirectory, IEnumerable<YOLODetectorEvaluation>? yOLODetectorEvaluations, IEnumerable<string>? failedStepNames, IEnumerable<string>? messages, DateTimeOffset? start, DateTimeOffset? end)
        {
            this.outputDirectory = outputDirectory;

            if (yOLODetectorEvaluations is not null)
            {
                foreach (YOLODetectorEvaluation yOLODetectorEvaluation in yOLODetectorEvaluations)
                {
                    if (Core.Query.Clone(yOLODetectorEvaluation) is YOLODetectorEvaluation yOLODetectorEvaluation_Clone)
                    {
                        this.yOLODetectorEvaluations.Add(yOLODetectorEvaluation_Clone);
                    }
                }
            }

            if (failedStepNames is not null)
            {
                this.failedStepNames = [.. failedStepNames];
            }

            if (messages is not null)
            {
                this.messages = [.. messages];
            }

            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLODetectorEvaluationResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="yOLODetectorEvaluationResult">The <see cref="YOLODetectorEvaluationResult"/> to copy from.</param>
        public YOLODetectorEvaluationResult(YOLODetectorEvaluationResult? yOLODetectorEvaluationResult)
            : base(yOLODetectorEvaluationResult)
        {
            if (yOLODetectorEvaluationResult is not null)
            {
                end = yOLODetectorEvaluationResult.end;
                failedStepNames = [.. yOLODetectorEvaluationResult.failedStepNames];
                messages = [.. yOLODetectorEvaluationResult.messages];
                outputDirectory = yOLODetectorEvaluationResult.outputDirectory;
                start = yOLODetectorEvaluationResult.start;
                yOLODetectorEvaluations = yOLODetectorEvaluationResult.YOLODetectorEvaluations;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLODetectorEvaluationResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public YOLODetectorEvaluationResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets when the run ended.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? End
        {
            get
            {
                return end;
            }
        }

        /// <summary>
        /// Gets the steps that reported a failure. Empty means every weights file was scored.
        /// </summary>
        [JsonIgnore]
        public List<string> FailedStepNames
        {
            get
            {
                return [.. failedStepNames];
            }
        }

        /// <summary>
        /// Gets what the run has to say beyond its rows.
        /// </summary>
        [JsonIgnore]
        public List<string> Messages
        {
            get
            {
                return [.. messages];
            }
        }

        /// <summary>
        /// Gets the root directory of the dataset evaluated.
        /// </summary>
        [JsonIgnore]
        public string? OutputDirectory
        {
            get
            {
                return outputDirectory;
            }
        }

        /// <summary>
        /// Gets when the run started.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Start
        {
            get
            {
                return start;
            }
        }

        /// <summary>
        /// Gets the rows, one per weights file per subset, in the order the weights were named.
        /// </summary>
        [JsonIgnore]
        public List<YOLODetectorEvaluation> YOLODetectorEvaluations
        {
            get
            {
                List<YOLODetectorEvaluation> result = [];
                foreach (YOLODetectorEvaluation yOLODetectorEvaluation in yOLODetectorEvaluations)
                {
                    if (Core.Query.Clone(yOLODetectorEvaluation) is YOLODetectorEvaluation yOLODetectorEvaluation_Clone)
                    {
                        result.Add(yOLODetectorEvaluation_Clone);
                    }
                }

                return result;
            }
        }
    }
}
