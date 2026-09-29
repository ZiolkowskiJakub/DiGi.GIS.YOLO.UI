using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// What one YOLO training dataset build - or <c>CountOnly</c> run - did: the tallies per county part and in total, and what it could not finish.
    /// <para><see cref="FailedStepNames"/> is what says whether the run did everything it set out to do. A building that fails is logged and stepped over, and a county part whose labels cannot be read is refused while the others are built, so a result that came back at all is not by itself evidence of a complete dataset.</para>
    /// </summary>
    public class YOLOTrainingDatasetResult : SerializableResult, IGISYOLOUISerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Cancelled))]
        private readonly bool cancelled;

        [JsonInclude, JsonPropertyName(nameof(CountOnly))]
        private readonly bool countOnly;

        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        private readonly List<int> countyIds = [];

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

        [JsonInclude, JsonPropertyName(nameof(Total))]
        private readonly YOLOTrainingDatasetCount? total;

        [JsonInclude, JsonPropertyName(nameof(YOLOTrainingDatasetCounts))]
        private readonly List<YOLOTrainingDatasetCount> yOLOTrainingDatasetCounts = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingDatasetResult"/> class.
        /// </summary>
        /// <param name="countyIds">The county parts the run covered, or null for none.</param>
        /// <param name="outputDirectory">The root directory of the dataset.</param>
        /// <param name="countOnly">Whether the run only counted.</param>
        /// <param name="total">The tallies of the whole run.</param>
        /// <param name="yOLOTrainingDatasetCounts">The tallies per county part, or null for none.</param>
        /// <param name="failedStepNames">The steps that reported a failure, or null for none.</param>
        /// <param name="messages">What the run has to say beyond its tallies, or null for nothing.</param>
        /// <param name="start">When the run started.</param>
        /// <param name="end">When the run ended.</param>
        /// <param name="cancelled">Whether the run was stopped before it covered everything it was given.</param>
        public YOLOTrainingDatasetResult(
            IEnumerable<int>? countyIds,
            string? outputDirectory,
            bool countOnly,
            YOLOTrainingDatasetCount? total,
            IEnumerable<YOLOTrainingDatasetCount>? yOLOTrainingDatasetCounts,
            IEnumerable<string>? failedStepNames,
            IEnumerable<string>? messages,
            DateTimeOffset? start,
            DateTimeOffset? end,
            bool cancelled)
        {
            if (countyIds is not null)
            {
                this.countyIds = [.. countyIds];
            }

            this.outputDirectory = outputDirectory;
            this.countOnly = countOnly;
            this.total = Core.Query.Clone(total);

            if (yOLOTrainingDatasetCounts is not null)
            {
                foreach (YOLOTrainingDatasetCount yOLOTrainingDatasetCount in yOLOTrainingDatasetCounts)
                {
                    if (Core.Query.Clone(yOLOTrainingDatasetCount) is YOLOTrainingDatasetCount yOLOTrainingDatasetCount_Clone)
                    {
                        this.yOLOTrainingDatasetCounts.Add(yOLOTrainingDatasetCount_Clone);
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
            this.cancelled = cancelled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingDatasetResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="yOLOTrainingDatasetResult">The <see cref="YOLOTrainingDatasetResult"/> to copy from.</param>
        public YOLOTrainingDatasetResult(YOLOTrainingDatasetResult? yOLOTrainingDatasetResult)
            : base(yOLOTrainingDatasetResult)
        {
            if (yOLOTrainingDatasetResult is not null)
            {
                cancelled = yOLOTrainingDatasetResult.cancelled;
                countOnly = yOLOTrainingDatasetResult.countOnly;
                countyIds = [.. yOLOTrainingDatasetResult.countyIds];
                end = yOLOTrainingDatasetResult.end;
                failedStepNames = [.. yOLOTrainingDatasetResult.failedStepNames];
                messages = [.. yOLOTrainingDatasetResult.messages];
                outputDirectory = yOLOTrainingDatasetResult.outputDirectory;
                start = yOLOTrainingDatasetResult.start;
                total = Core.Query.Clone(yOLOTrainingDatasetResult.total);
                yOLOTrainingDatasetCounts = yOLOTrainingDatasetResult.YOLOTrainingDatasetCounts;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingDatasetResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public YOLOTrainingDatasetResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets whether the run was stopped before it covered everything it was given. What it wrote is written, and the manifest lets a re-run continue.
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
        /// Gets whether the run only counted. A counting run requests no orthophoto and writes nothing.
        /// </summary>
        [JsonIgnore]
        public bool CountOnly
        {
            get
            {
                return countOnly;
            }
        }

        /// <summary>
        /// Gets the county parts the run covered, by identifier.
        /// </summary>
        [JsonIgnore]
        public List<int> CountyIds
        {
            get
            {
                return [.. countyIds];
            }
        }

        /// <summary>
        /// Gets how long the run took, or null when either end of it is unknown.
        /// </summary>
        [JsonIgnore]
        public TimeSpan? Duration
        {
            get
            {
                return start is null || end is null ? null : end.Value - start.Value;
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
        /// Gets the steps that reported a failure. Empty means the run did everything it set out to do.
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
        /// Gets what the run has to say beyond its tallies.
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
        /// Gets the root directory of the dataset.
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
        /// Gets the tallies of the whole run - the sum of <see cref="YOLOTrainingDatasetCounts"/>.
        /// </summary>
        [JsonIgnore]
        public YOLOTrainingDatasetCount? Total
        {
            get
            {
                return Core.Query.Clone(total);
            }
        }

        /// <summary>
        /// Gets the tallies per county part, in ascending identifier order.
        /// </summary>
        [JsonIgnore]
        public List<YOLOTrainingDatasetCount> YOLOTrainingDatasetCounts
        {
            get
            {
                List<YOLOTrainingDatasetCount> result = [];
                foreach (YOLOTrainingDatasetCount yOLOTrainingDatasetCount in yOLOTrainingDatasetCounts)
                {
                    if (Core.Query.Clone(yOLOTrainingDatasetCount) is YOLOTrainingDatasetCount yOLOTrainingDatasetCount_Clone)
                    {
                        result.Add(yOLOTrainingDatasetCount_Clone);
                    }
                }

                return result;
            }
        }
    }
}
