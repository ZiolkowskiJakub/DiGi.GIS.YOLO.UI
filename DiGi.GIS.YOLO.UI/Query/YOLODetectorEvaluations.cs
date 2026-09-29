using DiGi.Core.Classes;
using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.YOLO.UI.Classes;
using DiGi.YOLO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Scores one detector&apos;s detections over the Test buildings of a dataset: the first detection year of each building against its label, as mean absolute error, root mean square error and exact-match share, for all Test buildings and for the clean subset.
        /// <para>The detections go through the <b>same in-memory columns</b> the pipeline writes into the building data - <see cref="GIS.YOLO.Create.Building2DYearBuiltPredictions(BoundingBoxResultFile?)"/> then <see cref="GIS.IO.Modify.Update_Building2D_YearBuiltPredictions"/> on an empty table - and the first detection year is read back by <see cref="GIS.IO.Query.FirstDetectionYears"/>, so the number measured here is the feature the regressor is given, not a re-implementation of it. Nothing is written anywhere.</para>
        /// <para>A Test building with images the detector fired on in no year is still scored - its first detection year is the start of the range, as the regressor sees it. A building with no Test image is not in either subset. The clean subset is Test and not Legacy; a building whose Legacy decision could not be taken is Legacy.</para>
        /// </summary>
        /// <param name="datasetReferences">The dataset manifest. Only its Test buildings are scored.</param>
        /// <param name="boundingBoxResultFile">The detector&apos;s output over the Test images.</param>
        /// <param name="references_Imaged">The references with at least one Test image - the population scored.</param>
        /// <param name="weightsPath">The weights file the detections came from, carried into the rows.</param>
        /// <param name="sHA256">The SHA-256 of the weights file, carried into the rows.</param>
        /// <param name="years">The range of years first detections are read over. Null uses 2008 to 2025.</param>
        /// <returns>Two rows - <see cref="Enums.YOLODetectorEvaluationSubset.All"/> then <see cref="Enums.YOLODetectorEvaluationSubset.Clean"/> - or an empty list when there is no manifest.</returns>
        public static List<YOLODetectorEvaluation> YOLODetectorEvaluations(IEnumerable<DatasetReference>? datasetReferences, BoundingBoxResultFile? boundingBoxResultFile, IEnumerable<string>? references_Imaged, string? weightsPath, string? sHA256, Range<int>? years = null)
        {
            List<YOLODetectorEvaluation> result = [];
            if (datasetReferences is null)
            {
                return result;
            }

            Range<int> range_Years = years ?? new Range<int>(2008, 2025);

            HashSet<string> references_Imaged_Set = references_Imaged is null ? new(StringComparer.Ordinal) : new(references_Imaged, StringComparer.Ordinal);

            Dictionary<string, DatasetReference> datasetReferences_Test = new(StringComparer.Ordinal);
            foreach (DatasetReference datasetReference in datasetReferences)
            {
                if (datasetReference?.Reference is not string reference || datasetReference.Category != DiGi.YOLO.Enums.Category.Test || !references_Imaged_Set.Contains(reference))
                {
                    continue;
                }

                datasetReferences_Test[reference] = datasetReference;
            }

            Dictionary<string, Building2DYearBuiltPredictions> building2DYearBuiltPredictions_ByReference = new(StringComparer.Ordinal);
            List<Building2DYearBuiltPredictions>? building2DYearBuiltPredictions = GIS.YOLO.Create.Building2DYearBuiltPredictions(boundingBoxResultFile);
            if (building2DYearBuiltPredictions is not null)
            {
                foreach (Building2DYearBuiltPredictions building2DYearBuiltPredictions_Temp in building2DYearBuiltPredictions)
                {
                    if (building2DYearBuiltPredictions_Temp?.Reference is string reference)
                    {
                        building2DYearBuiltPredictions_ByReference[reference] = building2DYearBuiltPredictions_Temp;
                    }
                }
            }

            // One row per Test building, each filed under its own county, with an empty prediction set for a building
            // the detector found in no year - so it reads back as never detected rather than as absent.
            Table table = new();
            foreach (IGrouping<int, DatasetReference> grouping in datasetReferences_Test.Values.GroupBy(x => x.CountyId))
            {
                List<Building2DYearBuiltPredictions> building2DYearBuiltPredictions_County = [];
                foreach (DatasetReference datasetReference in grouping)
                {
                    building2DYearBuiltPredictions_County.Add(building2DYearBuiltPredictions_ByReference.TryGetValue(datasetReference.Reference!, out Building2DYearBuiltPredictions? building2DYearBuiltPredictions_Temp) && building2DYearBuiltPredictions_Temp is not null ? building2DYearBuiltPredictions_Temp : new Building2DYearBuiltPredictions(datasetReference.Reference, []));
                }

                GIS.IO.Modify.Update_Building2D_YearBuiltPredictions(table, grouping.Key, building2DYearBuiltPredictions_County);
            }

            List<short> firstDetectionYears = GIS.IO.Query.FirstDetectionYears(table, range_Years, (short)range_Years.Min);

            int index_Reference = table.GetColumnIndex(GIS.IO.Constants.Column.Reference.Name);

            List<(short Error, bool Clean)> errors = [];
            for (int i = 0; i < firstDetectionYears.Count; i++)
            {
                string? reference = index_Reference < 0 ? null : table.GetValue<string>(i, index_Reference);
                if (string.IsNullOrWhiteSpace(reference) || !datasetReferences_Test.TryGetValue(reference!, out DatasetReference? datasetReference) || datasetReference is null)
                {
                    continue;
                }

                errors.Add(((short)(firstDetectionYears[i] - datasetReference.Label), datasetReference.LegacySource == Enums.LegacySource.None));
            }

            YOLODetectorEvaluation Evaluation(Enums.YOLODetectorEvaluationSubset subset, List<short> errors_Subset)
            {
                if (errors_Subset.Count == 0)
                {
                    return new YOLODetectorEvaluation(weightsPath, sHA256, subset, 0, 0, 0, 0);
                }

                double meanAbsoluteError = errors_Subset.Average(x => (double)Math.Abs((int)x));
                double rootMeanSquareError = Math.Sqrt(errors_Subset.Average(x => (double)x * x));
                double exactShare = errors_Subset.Count(x => x == 0) / (double)errors_Subset.Count;

                return new YOLODetectorEvaluation(weightsPath, sHA256, subset, errors_Subset.Count, meanAbsoluteError, rootMeanSquareError, exactShare);
            }

            result.Add(Evaluation(Enums.YOLODetectorEvaluationSubset.All, [.. errors.Select(x => x.Error)]));
            result.Add(Evaluation(Enums.YOLODetectorEvaluationSubset.Clean, [.. errors.Where(x => x.Clean).Select(x => x.Error)]));

            return result;
        }
    }
}
