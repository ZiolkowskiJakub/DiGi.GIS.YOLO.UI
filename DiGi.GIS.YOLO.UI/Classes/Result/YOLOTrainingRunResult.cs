using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// What one <c>--train</c> run did: the identity of the weights it started from and of the weights it produced, and how they scored on the Test split.
    /// <para>Both identities are the SHA-256 of the file on disk, so a row of a comparison table names exactly which file it measures. <see cref="FailedStepNames"/> says whether every step that was asked for completed; the first failure stops the run.</para>
    /// <para>A resumed run says so through <see cref="Resumed"/> and <see cref="ResumedFromEpoch"/> - an interrupted run is continued rather than restarted, and a resumed run is not bit-identical to an uninterrupted one. <see cref="AutoResumes"/> names each resume the runner made on its own after the training stalled or crashed.</para>
    /// </summary>
    public class YOLOTrainingRunResult : SerializableResult, IGISYOLOUISerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(AutoResumes))]
        private readonly List<YOLOTrainingAutoResume> autoResumes = [];

        [JsonInclude, JsonPropertyName(nameof(Cancelled))]
        private readonly bool cancelled;

        [JsonInclude, JsonPropertyName(nameof(End))]
        private readonly DateTimeOffset? end;

        [JsonInclude, JsonPropertyName(nameof(FailedStepNames))]
        private readonly List<string> failedStepNames = [];

        [JsonInclude, JsonPropertyName(nameof(MAP50))]
        private readonly double? mAP50;

        [JsonInclude, JsonPropertyName(nameof(MAP50_95))]
        private readonly double? mAP50_95;

        [JsonInclude, JsonPropertyName(nameof(Messages))]
        private readonly List<string> messages = [];

        [JsonInclude, JsonPropertyName(nameof(RunName))]
        private readonly string? runName;

        [JsonInclude, JsonPropertyName(nameof(Resumed))]
        private readonly bool resumed;

        [JsonInclude, JsonPropertyName(nameof(ResumedFromEpoch))]
        private readonly int? resumedFromEpoch;

        [JsonInclude, JsonPropertyName(nameof(Start))]
        private readonly DateTimeOffset? start;

        [JsonInclude, JsonPropertyName(nameof(StartWeightsPath))]
        private readonly string? startWeightsPath;

        [JsonInclude, JsonPropertyName(nameof(StartWeightsSHA256))]
        private readonly string? startWeightsSHA256;

        [JsonInclude, JsonPropertyName(nameof(WeightsPath))]
        private readonly string? weightsPath;

        [JsonInclude, JsonPropertyName(nameof(WeightsSHA256))]
        private readonly string? weightsSHA256;

        [JsonInclude, JsonPropertyName(nameof(YOLODetectorEvaluations))]
        private readonly List<YOLODetectorEvaluation> yOLODetectorEvaluations = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingRunResult"/> class.
        /// </summary>
        /// <param name="runName">The name of the run.</param>
        /// <param name="startWeightsPath">The weights the training started from.</param>
        /// <param name="startWeightsSHA256">The SHA-256 of the start weights, or null when they were not read.</param>
        /// <param name="weightsPath">The new weights file the trained weights were copied to, or null when there are none.</param>
        /// <param name="weightsSHA256">The SHA-256 of that file, or null when there is none.</param>
        /// <param name="mAP50">The box mAP at IoU 0.5 on the Test split, or null when it was not validated.</param>
        /// <param name="mAP50_95">The box mAP over IoU 0.5 to 0.95 on the Test split, or null when it was not validated.</param>
        /// <param name="yOLODetectorEvaluations">The rows of the detector evaluation step, or null when it did not run.</param>
        /// <param name="cancelled">Whether the run was stopped before it finished.</param>
        /// <param name="failedStepNames">The steps that reported a failure, or null for none.</param>
        /// <param name="messages">What the run has to say beyond its identities, or null for nothing.</param>
        /// <param name="start">When the run started.</param>
        /// <param name="end">When the run ended.</param>
        /// <param name="resumed">Whether the run continued an interrupted training instead of starting from the beginning.</param>
        /// <param name="resumedFromEpoch">The 1-based epoch the resumed run entered, or null for a fresh run, a run that was not resumed or one whose resume epoch was not read.</param>
        /// <param name="autoResumes">The automatic resumes the run made after its training stalled or crashed, or null for none.</param>
        public YOLOTrainingRunResult(
            string? runName,
            string? startWeightsPath,
            string? startWeightsSHA256,
            string? weightsPath,
            string? weightsSHA256,
            double? mAP50,
            double? mAP50_95,
            IEnumerable<YOLODetectorEvaluation>? yOLODetectorEvaluations,
            bool cancelled,
            IEnumerable<string>? failedStepNames,
            IEnumerable<string>? messages,
            DateTimeOffset? start,
            DateTimeOffset? end,
            bool resumed = false,
            int? resumedFromEpoch = null,
            IEnumerable<YOLOTrainingAutoResume>? autoResumes = null)
        {
            this.runName = runName;
            this.startWeightsPath = startWeightsPath;
            this.startWeightsSHA256 = startWeightsSHA256;
            this.weightsPath = weightsPath;
            this.weightsSHA256 = weightsSHA256;
            this.mAP50 = mAP50;
            this.mAP50_95 = mAP50_95;
            this.cancelled = cancelled;

            if (autoResumes is not null)
            {
                foreach (YOLOTrainingAutoResume yOLOTrainingAutoResume in autoResumes)
                {
                    if (Core.Query.Clone(yOLOTrainingAutoResume) is YOLOTrainingAutoResume yOLOTrainingAutoResume_Clone)
                    {
                        this.autoResumes.Add(yOLOTrainingAutoResume_Clone);
                    }
                }
            }

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
            this.resumed = resumed;
            this.resumedFromEpoch = resumedFromEpoch;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingRunResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="yOLOTrainingRunResult">The <see cref="YOLOTrainingRunResult"/> to copy from.</param>
        public YOLOTrainingRunResult(YOLOTrainingRunResult? yOLOTrainingRunResult)
            : base(yOLOTrainingRunResult)
        {
            if (yOLOTrainingRunResult is not null)
            {
                autoResumes = yOLOTrainingRunResult.AutoResumes;
                cancelled = yOLOTrainingRunResult.cancelled;
                end = yOLOTrainingRunResult.end;
                failedStepNames = [.. yOLOTrainingRunResult.failedStepNames];
                mAP50 = yOLOTrainingRunResult.mAP50;
                mAP50_95 = yOLOTrainingRunResult.mAP50_95;
                messages = [.. yOLOTrainingRunResult.messages];
                resumed = yOLOTrainingRunResult.resumed;
                resumedFromEpoch = yOLOTrainingRunResult.resumedFromEpoch;
                runName = yOLOTrainingRunResult.runName;
                start = yOLOTrainingRunResult.start;
                startWeightsPath = yOLOTrainingRunResult.startWeightsPath;
                startWeightsSHA256 = yOLOTrainingRunResult.startWeightsSHA256;
                weightsPath = yOLOTrainingRunResult.weightsPath;
                weightsSHA256 = yOLOTrainingRunResult.weightsSHA256;
                yOLODetectorEvaluations = yOLOTrainingRunResult.YOLODetectorEvaluations;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingRunResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public YOLOTrainingRunResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the automatic resumes the run made after its training stalled or crashed, one entry per resume, in the order they happened. Empty when none was made.
        /// <para>This is distinct from <see cref="Resumed"/>: that one describes a resume an operator asked for, this one the retries the runner made by itself.</para>
        /// </summary>
        [JsonIgnore]
        public List<YOLOTrainingAutoResume> AutoResumes
        {
            get
            {
                List<YOLOTrainingAutoResume> result = [];
                foreach (YOLOTrainingAutoResume yOLOTrainingAutoResume in autoResumes)
                {
                    if (Core.Query.Clone(yOLOTrainingAutoResume) is YOLOTrainingAutoResume yOLOTrainingAutoResume_Clone)
                    {
                        result.Add(yOLOTrainingAutoResume_Clone);
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Gets whether the run was stopped before it finished.
        /// </summary>
        [JsonIgnore]
        public bool Cancelled
        {
            get
            {
                return cancelled;
            }
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
        /// Gets the steps that reported a failure. Empty means every step that was asked for completed.
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
        /// Gets the box mAP at IoU 0.5 of the trained weights on the Test split, or null when they were not validated.
        /// </summary>
        [JsonIgnore]
        public double? MAP50
        {
            get
            {
                return mAP50;
            }
        }

        /// <summary>
        /// Gets the box mAP over IoU 0.5 to 0.95 of the trained weights on the Test split, or null when they were not validated.
        /// </summary>
        [JsonIgnore]
        public double? MAP50_95
        {
            get
            {
                return mAP50_95;
            }
        }

        /// <summary>
        /// Gets what the run has to say beyond its identities.
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
        /// Gets whether the run continued an interrupted training instead of starting from the beginning.
        /// </summary>
        [JsonIgnore]
        public bool Resumed
        {
            get
            {
                return resumed;
            }
        }

        /// <summary>
        /// Gets the 1-based epoch the resumed run entered, or null for a fresh run, a run that was not resumed or one whose resume epoch was not read.
        /// </summary>
        [JsonIgnore]
        public int? ResumedFromEpoch
        {
            get
            {
                return resumedFromEpoch;
            }
        }

        /// <summary>
        /// Gets the name of the run.
        /// </summary>
        [JsonIgnore]
        public string? RunName
        {
            get
            {
                return runName;
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
        /// Gets the weights the training started from.
        /// </summary>
        [JsonIgnore]
        public string? StartWeightsPath
        {
            get
            {
                return startWeightsPath;
            }
        }

        /// <summary>
        /// Gets the SHA-256 of the start weights, or null when they were not read.
        /// </summary>
        [JsonIgnore]
        public string? StartWeightsSHA256
        {
            get
            {
                return startWeightsSHA256;
            }
        }

        /// <summary>
        /// Gets the new weights file the trained weights were copied to, or null when there are none.
        /// </summary>
        [JsonIgnore]
        public string? WeightsPath
        {
            get
            {
                return weightsPath;
            }
        }

        /// <summary>
        /// Gets the SHA-256 of <see cref="WeightsPath"/>, or null when there is none.
        /// </summary>
        [JsonIgnore]
        public string? WeightsSHA256
        {
            get
            {
                return weightsSHA256;
            }
        }

        /// <summary>
        /// Gets the rows of the detector evaluation step, one per weights file per subset. Empty when the step did not run.
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
