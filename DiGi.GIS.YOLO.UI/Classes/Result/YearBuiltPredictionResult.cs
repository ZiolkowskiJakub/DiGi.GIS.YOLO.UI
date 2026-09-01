using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// What one run of the Year Built prediction pipeline did: how much it read, how much it scored, how much it stored, and what it could not finish.
    /// <para><see cref="FailedStepNames"/> is what says whether a run did everything it set out to do. A step that fails is logged and stepped over so the steps behind it still run, so a result that came back at all is not by itself evidence of a complete run.</para>
    /// <para><see cref="RunTimestamp"/> is the stamp every prediction of the run carries into the year built data. One stamp for the whole run is deliberate: the stored entries are keyed by it, so a stamp taken per building would write one history entry per building instead of one per run.</para>
    /// </summary>
    public class YearBuiltPredictionResult : SerializableResult, IGISYOLOUISerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(BuildingCount))]
        private readonly long buildingCount;

        [JsonInclude, JsonPropertyName(nameof(BuildingDataUpdatedCount))]
        private readonly long buildingDataUpdatedCount;

        [JsonInclude, JsonPropertyName(nameof(Cancelled))]
        private readonly bool cancelled;

        [JsonInclude, JsonPropertyName(nameof(CountyIds))]
        private readonly List<int> countyIds = [];

        [JsonInclude, JsonPropertyName(nameof(DetectionCount))]
        private readonly long detectionCount;

        [JsonInclude, JsonPropertyName(nameof(End))]
        private readonly DateTimeOffset? end;

        [JsonInclude, JsonPropertyName(nameof(FailedStepNames))]
        private readonly List<string> failedStepNames = [];

        [JsonInclude, JsonPropertyName(nameof(FeatureRowCount))]
        private readonly long featureRowCount;

        [JsonInclude, JsonPropertyName(nameof(ImageCount))]
        private readonly long imageCount;

        [JsonInclude, JsonPropertyName(nameof(Messages))]
        private readonly List<string> messages = [];

        [JsonInclude, JsonPropertyName(nameof(PredictionCount))]
        private readonly long predictionCount;

        [JsonInclude, JsonPropertyName(nameof(RunTimestamp))]
        private readonly DateTimeOffset? runTimestamp;

        [JsonInclude, JsonPropertyName(nameof(Start))]
        private readonly DateTimeOffset? start;

        [JsonInclude, JsonPropertyName(nameof(YearBuiltDataUpdatedCount))]
        private readonly long yearBuiltDataUpdatedCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltPredictionResult"/> class.
        /// </summary>
        /// <param name="countyIds">The county rows the run covered, or null for none.</param>
        /// <param name="runTimestamp">The stamp every prediction of the run carries, or null when nothing was scored.</param>
        /// <param name="start">When the run started.</param>
        /// <param name="end">When the run ended.</param>
        /// <param name="imageCount">The number of orthophoto images the detector was given.</param>
        /// <param name="detectionCount">The number of detections the detector reported.</param>
        /// <param name="buildingCount">The number of buildings carrying at least one detection.</param>
        /// <param name="featureRowCount">The number of building data rows read for scoring.</param>
        /// <param name="predictionCount">The number of construction years the regressor returned.</param>
        /// <param name="yearBuiltDataUpdatedCount">The number of year built data entries written.</param>
        /// <param name="buildingDataUpdatedCount">The number of building data rows written.</param>
        /// <param name="failedStepNames">The steps that reported a failure, or null for none.</param>
        /// <param name="messages">What the run has to say beyond its tallies, or null for nothing.</param>
        /// <param name="cancelled">Whether the run was stopped before it covered everything it was given.</param>
        public YearBuiltPredictionResult(
            IEnumerable<int>? countyIds,
            DateTimeOffset? runTimestamp,
            DateTimeOffset? start,
            DateTimeOffset? end,
            long imageCount,
            long detectionCount,
            long buildingCount,
            long featureRowCount,
            long predictionCount,
            long yearBuiltDataUpdatedCount,
            long buildingDataUpdatedCount,
            IEnumerable<string>? failedStepNames,
            IEnumerable<string>? messages,
            bool cancelled)
        {
            if (countyIds is not null)
            {
                this.countyIds = [.. countyIds];
            }

            this.runTimestamp = runTimestamp;
            this.start = start;
            this.end = end;
            this.imageCount = imageCount;
            this.detectionCount = detectionCount;
            this.buildingCount = buildingCount;
            this.featureRowCount = featureRowCount;
            this.predictionCount = predictionCount;
            this.yearBuiltDataUpdatedCount = yearBuiltDataUpdatedCount;
            this.buildingDataUpdatedCount = buildingDataUpdatedCount;

            if (failedStepNames is not null)
            {
                this.failedStepNames = [.. failedStepNames];
            }

            if (messages is not null)
            {
                this.messages = [.. messages];
            }

            this.cancelled = cancelled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltPredictionResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="yearBuiltPredictionResult">The <see cref="YearBuiltPredictionResult"/> to copy from.</param>
        public YearBuiltPredictionResult(YearBuiltPredictionResult? yearBuiltPredictionResult)
            : base(yearBuiltPredictionResult)
        {
            if (yearBuiltPredictionResult is not null)
            {
                buildingCount = yearBuiltPredictionResult.buildingCount;
                buildingDataUpdatedCount = yearBuiltPredictionResult.buildingDataUpdatedCount;
                cancelled = yearBuiltPredictionResult.cancelled;
                countyIds = [.. yearBuiltPredictionResult.countyIds];
                detectionCount = yearBuiltPredictionResult.detectionCount;
                end = yearBuiltPredictionResult.end;
                failedStepNames = [.. yearBuiltPredictionResult.failedStepNames];
                featureRowCount = yearBuiltPredictionResult.featureRowCount;
                imageCount = yearBuiltPredictionResult.imageCount;
                messages = [.. yearBuiltPredictionResult.messages];
                predictionCount = yearBuiltPredictionResult.predictionCount;
                runTimestamp = yearBuiltPredictionResult.runTimestamp;
                start = yearBuiltPredictionResult.start;
                yearBuiltDataUpdatedCount = yearBuiltPredictionResult.yearBuiltDataUpdatedCount;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltPredictionResult"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public YearBuiltPredictionResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the number of buildings carrying at least one detection.
        /// <para>Lower than the number of images, because one building is imaged once per year of orthophoto coverage, and lower than the number of buildings in the county, because a building the detector found nothing on in any year is not counted.</para>
        /// </summary>
        [JsonIgnore]
        public long BuildingCount => buildingCount;

        /// <summary>
        /// Gets the number of building data rows written, counting the detection write and the predicted year column separately.
        /// </summary>
        [JsonIgnore]
        public long BuildingDataUpdatedCount => buildingDataUpdatedCount;

        /// <summary>
        /// Gets whether the run was stopped before it covered everything it was given.
        /// </summary>
        [JsonIgnore]
        public bool Cancelled => cancelled;

        /// <summary>
        /// Gets the county rows the run covered.
        /// <para>Each identifier is a polygon part rather than a county, so a multi-part county appears here once per part.</para>
        /// </summary>
        [JsonIgnore]
        public List<int> CountyIds => countyIds;

        /// <summary>
        /// Gets the number of detections the detector reported, across every building and every year.
        /// </summary>
        [JsonIgnore]
        public long DetectionCount => detectionCount;

        /// <summary>
        /// Gets the duration of the run, or null when it did not record both ends.
        /// </summary>
        [JsonIgnore]
        public TimeSpan? Duration => start.HasValue && end.HasValue ? end.Value - start.Value : null;

        /// <summary>
        /// Gets when the run ended.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? End => end;

        /// <summary>
        /// Gets the steps that reported a failure and were stepped over.
        /// <para>Empty is the only evidence that a run did everything it set out to do - the result comes back either way.</para>
        /// </summary>
        [JsonIgnore]
        public List<string> FailedStepNames => failedStepNames;

        /// <summary>
        /// Gets the number of building data rows read for scoring.
        /// </summary>
        [JsonIgnore]
        public long FeatureRowCount => featureRowCount;

        /// <summary>
        /// Gets the number of orthophoto images the detector was given.
        /// </summary>
        [JsonIgnore]
        public long ImageCount => imageCount;

        /// <summary>
        /// Gets what the run has to say beyond its tallies, such as why the machine could not run the detector at all.
        /// </summary>
        [JsonIgnore]
        public List<string> Messages => messages;

        /// <summary>
        /// Gets the number of construction years the regressor returned.
        /// </summary>
        [JsonIgnore]
        public long PredictionCount => predictionCount;

        /// <summary>
        /// Gets the stamp every prediction of the run carries into the year built data, or null when nothing was scored.
        /// <para>One stamp for the whole run. The stored entries are keyed by it, so re-running with the same stamp replaces the run rather than adding to the history, and a stamp taken per building would write one entry per building.</para>
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? RunTimestamp => runTimestamp;

        /// <summary>
        /// Gets when the run started.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Start => start;

        /// <summary>
        /// Gets the number of year built data entries written, preserving each building's history.
        /// </summary>
        [JsonIgnore]
        public long YearBuiltDataUpdatedCount => yearBuiltDataUpdatedCount;
    }
}
