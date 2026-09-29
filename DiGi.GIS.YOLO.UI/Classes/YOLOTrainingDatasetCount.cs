using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// The tallies of a YOLO training dataset build, for one county part or for the whole run.
    /// <para>The counting half - labelled buildings, the split, the Legacy agreement table, bounded entries, cross-part duplicates and the estimates - is filled by a <c>CountOnly</c> run as well as by a build; the building half - images, boxes, skipped and failed buildings - only by a build.</para>
    /// <para>The Legacy counts cover the Test buildings only: the clean subset of the detector evaluation is Test and not Legacy, and a Train or Validate building&apos;s Legacy decision feeds no metric.</para>
    /// </summary>
    public class YOLOTrainingDatasetCount : SerializableObject, IGISYOLOUISerializableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingDatasetCount"/> class with every tally at zero.
        /// </summary>
        /// <param name="countyId">The identifier of the county part the tallies cover, or null for the whole run.</param>
        public YOLOTrainingDatasetCount(int? countyId)
        {
            CountyId = countyId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingDatasetCount"/> class by copying an existing one.
        /// </summary>
        /// <param name="yOLOTrainingDatasetCount">The <see cref="YOLOTrainingDatasetCount"/> to copy from.</param>
        public YOLOTrainingDatasetCount(YOLOTrainingDatasetCount? yOLOTrainingDatasetCount)
            : base(yOLOTrainingDatasetCount)
        {
            if (yOLOTrainingDatasetCount is not null)
            {
                BoundedEntryCount = yOLOTrainingDatasetCount.BoundedEntryCount;
                BuildingCount = yOLOTrainingDatasetCount.BuildingCount;
                ClampedBoxCount = yOLOTrainingDatasetCount.ClampedBoxCount;
                CountyId = yOLOTrainingDatasetCount.CountyId;
                DroppedBoxCount = yOLOTrainingDatasetCount.DroppedBoxCount;
                DuplicateReferenceCount = yOLOTrainingDatasetCount.DuplicateReferenceCount;
                EstimatedByteCount = yOLOTrainingDatasetCount.EstimatedByteCount;
                EstimatedImageCount = yOLOTrainingDatasetCount.EstimatedImageCount;
                EstimatedRequestCount = yOLOTrainingDatasetCount.EstimatedRequestCount;
                FailedBuildingCount = yOLOTrainingDatasetCount.FailedBuildingCount;
                IdenticalImageDroppedCount = yOLOTrainingDatasetCount.IdenticalImageDroppedCount;
                IdenticalImageMergedCount = yOLOTrainingDatasetCount.IdenticalImageMergedCount;
                ImageCount = yOLOTrainingDatasetCount.ImageCount;
                LabelConflictCount = yOLOTrainingDatasetCount.LabelConflictCount;
                LabelledCount = yOLOTrainingDatasetCount.LabelledCount;
                LegacyBothCount = yOLOTrainingDatasetCount.LegacyBothCount;
                LegacyNoneCount = yOLOTrainingDatasetCount.LegacyNoneCount;
                LegacyTimestampCount = yOLOTrainingDatasetCount.LegacyTimestampCount;
                LegacyTsvCount = yOLOTrainingDatasetCount.LegacyTsvCount;
                LegacyUnknownCount = yOLOTrainingDatasetCount.LegacyUnknownCount;
                NegativeImageCount = yOLOTrainingDatasetCount.NegativeImageCount;
                PositiveImageCount = yOLOTrainingDatasetCount.PositiveImageCount;
                ReferenceDuplicateCount = yOLOTrainingDatasetCount.ReferenceDuplicateCount;
                ResumedCount = yOLOTrainingDatasetCount.ResumedCount;
                SameYearImageCount = yOLOTrainingDatasetCount.SameYearImageCount;
                TestCount = yOLOTrainingDatasetCount.TestCount;
                TrainCount = yOLOTrainingDatasetCount.TrainCount;
                ValidateCount = yOLOTrainingDatasetCount.ValidateCount;
                WithoutFootprintCount = yOLOTrainingDatasetCount.WithoutFootprintCount;
                WithoutImageryCount = yOLOTrainingDatasetCount.WithoutImageryCount;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingDatasetCount"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public YOLOTrainingDatasetCount(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets or sets the number of Test buildings carrying a bounded user entry (at or before, or after a year).
        /// <para>Bounded entries are not trained on; the count lets a later cycle decide whether to use them.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(BoundedEntryCount))]
        public long BoundedEntryCount { get; set; }

        /// <summary>
        /// Gets or sets the number of buildings the dataset takes from the county part, after de-duplication.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(BuildingCount))]
        public long BuildingCount { get; set; }

        /// <summary>
        /// Gets or sets the number of boxes that crossed an image edge and were clamped to it.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ClampedBoxCount))]
        public long ClampedBoxCount { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the county part the tallies cover, or null for the whole run.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        public int? CountyId { get; set; }

        /// <summary>
        /// Gets or sets the number of boxes with no area inside their image.
        /// <para>The image is then not written at all: a positive year with no box would teach the detector the building is absent.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(DroppedBoxCount))]
        public long DroppedBoxCount { get; set; }

        /// <summary>
        /// Gets or sets the number of labelled references dropped because a lower-numbered county part of the run already holds them.
        /// <para>A reference is unique only per county part, so one filed under two named parts would otherwise be built twice - and could land in two splits.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(DuplicateReferenceCount))]
        public long DuplicateReferenceCount { get; set; }

        /// <summary>
        /// Gets or sets the estimated disk space, in bytes, a build would take, from an assumed size per image.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(EstimatedByteCount))]
        public long EstimatedByteCount { get; set; }

        /// <summary>
        /// Gets or sets the estimated number of images a build would write, from an assumed number of orthophoto years per building.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(EstimatedImageCount))]
        public long EstimatedImageCount { get; set; }

        /// <summary>
        /// Gets or sets the estimated number of Web API requests a build would make: one orthophoto read per building, plus the paged footprint reads.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(EstimatedRequestCount))]
        public long EstimatedRequestCount { get; set; }

        /// <summary>
        /// Gets or sets the number of buildings that failed while their images were saved.
        /// <para>Each is logged and stepped over.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(FailedBuildingCount))]
        public long FailedBuildingCount { get; set; }

        /// <summary>
        /// Gets or sets the number of images dropped because the same photo bytes appear under another year of the building with a conflicting label - one year positive, the other negative.
        /// <para>Identical pixels with contradictory labels teach nothing but noise.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(IdenticalImageDroppedCount))]
        public long IdenticalImageDroppedCount { get; set; }

        /// <summary>
        /// Gets or sets the number of images dropped because the same photo bytes appear under an earlier year of the building with an agreeing label.
        /// <para>The earliest is kept.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(IdenticalImageMergedCount))]
        public long IdenticalImageMergedCount { get; set; }

        /// <summary>
        /// Gets or sets the number of images written.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ImageCount))]
        public long ImageCount { get; set; }

        /// <summary>
        /// Gets or sets the number of dropped duplicate references whose label differs between the parts.
        /// <para>The label of the lowest-numbered part is kept.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(LabelConflictCount))]
        public long LabelConflictCount { get; set; }

        /// <summary>
        /// Gets or sets the number of references the county part holds a label for - an exact user year built in the building data.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(LabelledCount))]
        public long LabelledCount { get; set; }

        /// <summary>
        /// Gets or sets the number of Test buildings both records name.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(LegacyBothCount))]
        public long LegacyBothCount { get; set; }

        /// <summary>
        /// Gets or sets the number of Test buildings neither record names - the clean subset.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(LegacyNoneCount))]
        public long LegacyNoneCount { get; set; }

        /// <summary>
        /// Gets or sets the number of Test buildings only the stored history names - a user entry undated or dated before the cut-off.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(LegacyTimestampCount))]
        public long LegacyTimestampCount { get; set; }

        /// <summary>
        /// Gets or sets the number of Test buildings only the legacy reference list names.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(LegacyTsvCount))]
        public long LegacyTsvCount { get; set; }

        /// <summary>
        /// Gets or sets the number of Test buildings whose history could not be read.
        /// <para>They are Legacy, never clean.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(LegacyUnknownCount))]
        public long LegacyUnknownCount { get; set; }

        /// <summary>
        /// Gets or sets the number of images of a year before the label - registered with an empty label file, as background.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(NegativeImageCount))]
        public long NegativeImageCount { get; set; }

        /// <summary>
        /// Gets or sets the number of images of a year at or after the label - each carries the building&apos;s box.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(PositiveImageCount))]
        public long PositiveImageCount { get; set; }

        /// <summary>
        /// Gets or sets the number of references the API reports as filed under several county parts, among them this one.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ReferenceDuplicateCount))]
        public long ReferenceDuplicateCount { get; set; }

        /// <summary>
        /// Gets or sets the number of buildings skipped because the manifest already records them as complete.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ResumedCount))]
        public long ResumedCount { get; set; }

        /// <summary>
        /// Gets or sets the number of orthophotos skipped because an earlier one of the same year was already taken.
        /// <para>One photo per year.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(SameYearImageCount))]
        public long SameYearImageCount { get; set; }

        /// <summary>
        /// Gets or sets the number of buildings in the Test split - the held-out ones.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(TestCount))]
        public long TestCount { get; set; }

        /// <summary>
        /// Gets or sets the number of buildings in the Train split.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(TrainCount))]
        public long TrainCount { get; set; }

        /// <summary>
        /// Gets or sets the number of buildings in the Validate split.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ValidateCount))]
        public long ValidateCount { get; set; }

        /// <summary>
        /// Gets or sets the number of buildings skipped because no footprint could be read.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(WithoutFootprintCount))]
        public long WithoutFootprintCount { get; set; }

        /// <summary>
        /// Gets or sets the number of buildings skipped because no orthophoto could be read.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(WithoutImageryCount))]
        public long WithoutImageryCount { get; set; }
    }
}
