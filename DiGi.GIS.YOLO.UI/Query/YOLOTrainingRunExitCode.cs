using DiGi.GIS.YOLO.UI.Classes;
using DiGi.GIS.YOLO.UI.Enums;
using System.Collections.Generic;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Maps the outcome of a <c>--train</c> run to the exit code of the console application.
        /// <para>A cancellation is <see cref="YearBuiltPredictionExitCode.Cancelled"/> whatever else was listed. Otherwise an option that cannot be used is a <see cref="YearBuiltPredictionExitCode.Configuration"/>, a machine that cannot run the detector an <see cref="YearBuiltPredictionExitCode.Environment"/>, the training and the validation have codes of their own, and any other failed step is <see cref="YearBuiltPredictionExitCode.Failed"/>. A dataset split named in a failure - <c>Train</c>, <c>Validate</c> or <c>Test</c> - is a dataset problem, not a failed step, and keeps the <see cref="YearBuiltPredictionExitCode.Configuration"/> code. The earlier reading of the same step wins, so a run refused before its first step keeps the code of what was wrong with the options.</para>
        /// </summary>
        /// <param name="yOLOTrainingRunResult">The result of the run.</param>
        /// <returns>The exit code. <see cref="YearBuiltPredictionExitCode.Failed"/> when there is no result.</returns>
        public static YearBuiltPredictionExitCode YOLOTrainingRunExitCode(YOLOTrainingRunResult? yOLOTrainingRunResult)
        {
            if (yOLOTrainingRunResult is null)
            {
                return YearBuiltPredictionExitCode.Failed;
            }

            if (yOLOTrainingRunResult.Cancelled)
            {
                return YearBuiltPredictionExitCode.Cancelled;
            }

            List<string> failedStepNames = yOLOTrainingRunResult.FailedStepNames;
            if (failedStepNames.Count == 0)
            {
                return YearBuiltPredictionExitCode.Succeeded;
            }

            List<string> stepNames_Configuration =
            [
                nameof(Classes.YOLOTrainingRunOptions.DatasetOptions),
                nameof(Classes.YOLOTrainingRunOptions.StartWeightsPath),
                nameof(Classes.YOLOTrainingRunOptions.RunName),
                nameof(Classes.YOLOTrainingRunOptions.ProjectDirectory),
                nameof(Classes.YOLOTrainingRunOptions.ResumeTraining),
                nameof(DiGi.YOLO.Query.YOLOCheckpointInformation),
                nameof(UnknownCountyIds),
                nameof(LegacyReferences),
                nameof(Classes.YOLOTrainingDatasetOptions.OutputDirectory),
                nameof(Classes.YOLOTrainingDatasetOptions.Resume),
                nameof(Constants.LabelName),
                nameof(DatasetReferences),
                nameof(DiGi.YOLO.Modify.Read),
                nameof(Classes.YOLOTrainingDatasetOptions.WeightsPaths),
                nameof(DiGi.YOLO.Enums.Category.Train),
                nameof(DiGi.YOLO.Enums.Category.Validate),
                nameof(DiGi.YOLO.Enums.Category.Test)
            ];

            List<string> stepNames_Environment =
            [
                nameof(Classes.YOLOTrainingRunOptions.PythonPath),
                nameof(DiGi.YOLO.Query.YOLOEnvironmentResult),
                nameof(ModelPath),
                nameof(DiGi.YOLO.Create.YOLOPredictionOptions)
            ];

            if (failedStepNames.Exists(stepNames_Configuration.Contains))
            {
                return YearBuiltPredictionExitCode.Configuration;
            }

            if (failedStepNames.Exists(stepNames_Environment.Contains))
            {
                return YearBuiltPredictionExitCode.Environment;
            }

            if (failedStepNames.Contains(YOLOTrainingStepName(YOLOTrainingStep.Train)))
            {
                return YearBuiltPredictionExitCode.Training;
            }

            if (failedStepNames.Contains(YOLOTrainingStepName(YOLOTrainingStep.Validate)))
            {
                return YearBuiltPredictionExitCode.Validation;
            }

            return YearBuiltPredictionExitCode.Failed;
        }
    }
}
