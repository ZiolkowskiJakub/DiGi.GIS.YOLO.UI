using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// How well the label boxes of a training dataset agree with what the current detector finds on the same images: the intersection over union of each label box with the best-overlapping detection.
    /// <para>The <c>train8</c> label files are lost, so the new labels cannot be compared with the old ones directly. The detector trained on them is the next best record: a low mean intersection over union means the new boxes are shifted or scaled against the ones it learned from - the device-independent-pixel normalisation of the legacy builder is the known suspect - and training should stop until it is explained.</para>
    /// </summary>
    public class YOLOLabelCheckResult : SerializableResult, IGISYOLOUISerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(CountyIntersectionOverUnions))]
        private readonly Dictionary<string, double> countyIntersectionOverUnions = [];

        [JsonInclude, JsonPropertyName(nameof(DetectedCount))]
        private readonly int detectedCount;

        [JsonInclude, JsonPropertyName(nameof(End))]
        private readonly DateTimeOffset? end;

        [JsonInclude, JsonPropertyName(nameof(FailedStepNames))]
        private readonly List<string> failedStepNames = [];

        [JsonInclude, JsonPropertyName(nameof(MeanIntersectionOverUnion))]
        private readonly double meanIntersectionOverUnion;

        [JsonInclude, JsonPropertyName(nameof(MedianIntersectionOverUnion))]
        private readonly double medianIntersectionOverUnion;

        [JsonInclude, JsonPropertyName(nameof(Messages))]
        private readonly List<string> messages = [];

        [JsonInclude, JsonPropertyName(nameof(OverlayDirectory))]
        private readonly string? overlayDirectory;

        [JsonInclude, JsonPropertyName(nameof(SampleCount))]
        private readonly int sampleCount;

        [JsonInclude, JsonPropertyName(nameof(ShareAboveHalf))]
        private readonly double shareAboveHalf;

        [JsonInclude, JsonPropertyName(nameof(Start))]
        private readonly DateTimeOffset? start;

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOLabelCheckResult"/> class.
        /// </summary>
        /// <param name="sampleCount">The number of positive images checked.</param>
        /// <param name="detectedCount">The number of checked images the detector found anything on.</param>
        /// <param name="meanIntersectionOverUnion">The mean intersection over union across the sample; an image with no detection counts as 0.</param>
        /// <param name="medianIntersectionOverUnion">The median intersection over union across the sample.</param>
        /// <param name="shareAboveHalf">The share of the sample with an intersection over union of 0.5 or more.</param>
        /// <param name="countyIntersectionOverUnions">The mean intersection over union per county part, keyed by the county identifier in invariant culture, or null for none.</param>
        /// <param name="overlayDirectory">The directory the overlay images were written to.</param>
        /// <param name="failedStepNames">The steps that reported a failure, or null for none.</param>
        /// <param name="messages">What the run has to say beyond its numbers, or null for nothing.</param>
        /// <param name="start">When the run started.</param>
        /// <param name="end">When the run ended.</param>
        public YOLOLabelCheckResult(
            int sampleCount,
            int detectedCount,
            double meanIntersectionOverUnion,
            double medianIntersectionOverUnion,
            double shareAboveHalf,
            IDictionary<string, double>? countyIntersectionOverUnions,
            string? overlayDirectory,
            IEnumerable<string>? failedStepNames,
            IEnumerable<string>? messages,
            DateTimeOffset? start,
            DateTimeOffset? end)
        {
            this.sampleCount = sampleCount;
            this.detectedCount = detectedCount;
            this.meanIntersectionOverUnion = meanIntersectionOverUnion;
            this.medianIntersectionOverUnion = medianIntersectionOverUnion;
            this.shareAboveHalf = shareAboveHalf;

            if (countyIntersectionOverUnions is not null)
            {
                this.countyIntersectionOverUnions = new Dictionary<string, double>(countyIntersectionOverUnions);
            }

            this.overlayDirectory = overlayDirectory;

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
        /// Initializes a new instance of the <see cref="YOLOLabelCheckResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="yOLOLabelCheckResult">The <see cref="YOLOLabelCheckResult"/> to copy from.</param>
        public YOLOLabelCheckResult(YOLOLabelCheckResult? yOLOLabelCheckResult)
            : base(yOLOLabelCheckResult)
        {
            if (yOLOLabelCheckResult is not null)
            {
                countyIntersectionOverUnions = new Dictionary<string, double>(yOLOLabelCheckResult.countyIntersectionOverUnions);
                detectedCount = yOLOLabelCheckResult.detectedCount;
                end = yOLOLabelCheckResult.end;
                failedStepNames = [.. yOLOLabelCheckResult.failedStepNames];
                meanIntersectionOverUnion = yOLOLabelCheckResult.meanIntersectionOverUnion;
                medianIntersectionOverUnion = yOLOLabelCheckResult.medianIntersectionOverUnion;
                messages = [.. yOLOLabelCheckResult.messages];
                overlayDirectory = yOLOLabelCheckResult.overlayDirectory;
                sampleCount = yOLOLabelCheckResult.sampleCount;
                shareAboveHalf = yOLOLabelCheckResult.shareAboveHalf;
                start = yOLOLabelCheckResult.start;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOLabelCheckResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public YOLOLabelCheckResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the mean intersection over union per county part, keyed by the county identifier in invariant culture. A county that stands out from the others points at data rather than at the rule.
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, double> CountyIntersectionOverUnions
        {
            get
            {
                return new Dictionary<string, double>(countyIntersectionOverUnions);
            }
        }

        /// <summary>
        /// Gets the number of checked images the detector found anything on.
        /// </summary>
        [JsonIgnore]
        public int DetectedCount
        {
            get
            {
                return detectedCount;
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
        /// Gets the steps that reported a failure. Empty means the check ran over the whole sample.
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
        /// Gets the mean intersection over union across the sample. An image the detector found nothing on counts as 0.
        /// </summary>
        [JsonIgnore]
        public double MeanIntersectionOverUnion
        {
            get
            {
                return meanIntersectionOverUnion;
            }
        }

        /// <summary>
        /// Gets the median intersection over union across the sample.
        /// </summary>
        [JsonIgnore]
        public double MedianIntersectionOverUnion
        {
            get
            {
                return medianIntersectionOverUnion;
            }
        }

        /// <summary>
        /// Gets what the run has to say beyond its numbers.
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
        /// Gets the directory the overlay images - the label box and the best detection drawn on the image - were written to.
        /// </summary>
        [JsonIgnore]
        public string? OverlayDirectory
        {
            get
            {
                return overlayDirectory;
            }
        }

        /// <summary>
        /// Gets the number of positive images checked.
        /// </summary>
        [JsonIgnore]
        public int SampleCount
        {
            get
            {
                return sampleCount;
            }
        }

        /// <summary>
        /// Gets the share of the sample with an intersection over union of 0.5 or more - the usual threshold for a box counting as found.
        /// </summary>
        [JsonIgnore]
        public double ShareAboveHalf
        {
            get
            {
                return shareAboveHalf;
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
    }
}
