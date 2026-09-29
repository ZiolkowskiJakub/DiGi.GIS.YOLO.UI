using DiGi.GIS.YOLO.UI.Classes;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Adds every tally of one YOLO training dataset count to another - a county part&apos;s into the run total, or one building&apos;s into its county part&apos;s.
        /// <para>The county identifier of the target is left as it is.</para>
        /// </summary>
        /// <param name="yOLOTrainingDatasetCount">The count added to.</param>
        /// <param name="yOLOTrainingDatasetCount_Add">The count added.</param>
        /// <returns>True when both counts were given and the tallies were added.</returns>
        public static bool Add(this YOLOTrainingDatasetCount? yOLOTrainingDatasetCount, YOLOTrainingDatasetCount? yOLOTrainingDatasetCount_Add)
        {
            if (yOLOTrainingDatasetCount is null || yOLOTrainingDatasetCount_Add is null)
            {
                return false;
            }

            yOLOTrainingDatasetCount.BoundedEntryCount += yOLOTrainingDatasetCount_Add.BoundedEntryCount;
            yOLOTrainingDatasetCount.BuildingCount += yOLOTrainingDatasetCount_Add.BuildingCount;
            yOLOTrainingDatasetCount.ClampedBoxCount += yOLOTrainingDatasetCount_Add.ClampedBoxCount;
            yOLOTrainingDatasetCount.DroppedBoxCount += yOLOTrainingDatasetCount_Add.DroppedBoxCount;
            yOLOTrainingDatasetCount.DuplicateReferenceCount += yOLOTrainingDatasetCount_Add.DuplicateReferenceCount;
            yOLOTrainingDatasetCount.EstimatedByteCount += yOLOTrainingDatasetCount_Add.EstimatedByteCount;
            yOLOTrainingDatasetCount.EstimatedImageCount += yOLOTrainingDatasetCount_Add.EstimatedImageCount;
            yOLOTrainingDatasetCount.EstimatedRequestCount += yOLOTrainingDatasetCount_Add.EstimatedRequestCount;
            yOLOTrainingDatasetCount.FailedBuildingCount += yOLOTrainingDatasetCount_Add.FailedBuildingCount;
            yOLOTrainingDatasetCount.IdenticalImageDroppedCount += yOLOTrainingDatasetCount_Add.IdenticalImageDroppedCount;
            yOLOTrainingDatasetCount.IdenticalImageMergedCount += yOLOTrainingDatasetCount_Add.IdenticalImageMergedCount;
            yOLOTrainingDatasetCount.ImageCount += yOLOTrainingDatasetCount_Add.ImageCount;
            yOLOTrainingDatasetCount.LabelConflictCount += yOLOTrainingDatasetCount_Add.LabelConflictCount;
            yOLOTrainingDatasetCount.LabelledCount += yOLOTrainingDatasetCount_Add.LabelledCount;
            yOLOTrainingDatasetCount.LegacyBothCount += yOLOTrainingDatasetCount_Add.LegacyBothCount;
            yOLOTrainingDatasetCount.LegacyNoneCount += yOLOTrainingDatasetCount_Add.LegacyNoneCount;
            yOLOTrainingDatasetCount.LegacyTimestampCount += yOLOTrainingDatasetCount_Add.LegacyTimestampCount;
            yOLOTrainingDatasetCount.LegacyTsvCount += yOLOTrainingDatasetCount_Add.LegacyTsvCount;
            yOLOTrainingDatasetCount.LegacyUnknownCount += yOLOTrainingDatasetCount_Add.LegacyUnknownCount;
            yOLOTrainingDatasetCount.NegativeImageCount += yOLOTrainingDatasetCount_Add.NegativeImageCount;
            yOLOTrainingDatasetCount.PositiveImageCount += yOLOTrainingDatasetCount_Add.PositiveImageCount;
            yOLOTrainingDatasetCount.ReferenceDuplicateCount += yOLOTrainingDatasetCount_Add.ReferenceDuplicateCount;
            yOLOTrainingDatasetCount.ResumedCount += yOLOTrainingDatasetCount_Add.ResumedCount;
            yOLOTrainingDatasetCount.SameYearImageCount += yOLOTrainingDatasetCount_Add.SameYearImageCount;
            yOLOTrainingDatasetCount.TestCount += yOLOTrainingDatasetCount_Add.TestCount;
            yOLOTrainingDatasetCount.TrainCount += yOLOTrainingDatasetCount_Add.TrainCount;
            yOLOTrainingDatasetCount.ValidateCount += yOLOTrainingDatasetCount_Add.ValidateCount;
            yOLOTrainingDatasetCount.WithoutFootprintCount += yOLOTrainingDatasetCount_Add.WithoutFootprintCount;
            yOLOTrainingDatasetCount.WithoutImageryCount += yOLOTrainingDatasetCount_Add.WithoutImageryCount;

            return true;
        }
    }
}
