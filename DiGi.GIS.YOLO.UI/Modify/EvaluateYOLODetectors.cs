using DiGi.GIS.YOLO.UI.Classes;
using DiGi.GIS.YOLO.UI.Enums;
using DiGi.YOLO.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Compares detector weights without the regressor: each weights file in <see cref="YOLOTrainingDatasetOptions.WeightsPaths"/> is run over the same Test images of a dataset, and its first detection years are scored against the labels by <see cref="Query.YOLODetectorEvaluations"/> - one row per weights file for all Test buildings and one for the clean subset (Test and not Legacy).
        /// <para>Read-only: the detector runs over the dataset&apos;s images, its output goes to an <c>evaluation</c> folder beside them, and nothing is written to the Web API or to the dataset itself. The prediction threshold is <see cref="YOLOTrainingDatasetOptions.Confidence"/>, the production one by default, so the rows measure the weights as the pipeline would run them.</para>
        /// <para>A weights file that cannot be found or run is reported and stepped over, so the rows of the others still come back.</para>
        /// </summary>
        /// <param name="yOLOTrainingDatasetOptions">The options naming the dataset, the weights files and the interpreter.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the detector.</param>
        /// <returns>The rows, or null when there are no options or no absolute dataset directory.</returns>
        public static YOLODetectorEvaluationResult? EvaluateYOLODetectors(this YOLOTrainingDatasetOptions? yOLOTrainingDatasetOptions, CancellationToken cancellationToken = default)
        {
            string? outputDirectory = yOLOTrainingDatasetOptions?.OutputDirectory;
            if (yOLOTrainingDatasetOptions is null || string.IsNullOrWhiteSpace(outputDirectory) || !Path.IsPathRooted(outputDirectory))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: no options or no absolute dataset directory", nameof(EvaluateYOLODetectors));
                return null;
            }

            outputDirectory = Path.GetFullPath(outputDirectory);

            DateTimeOffset start = DateTimeOffset.Now;
            List<string> failedStepNames = [];
            List<string> messages = [];
            List<YOLODetectorEvaluation> yOLODetectorEvaluations = [];

            void Fail(string stepName, string message)
            {
                if (!failedStepNames.Contains(stepName))
                {
                    failedStepNames.Add(stepName);
                }

                messages.Add(message);

                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "{Method}: step {Step} did not complete - {Message}", nameof(EvaluateYOLODetectors), stepName, message);
            }

            YOLODetectorEvaluationResult Result()
            {
                return new YOLODetectorEvaluationResult(outputDirectory, yOLODetectorEvaluations, failedStepNames, messages, start, DateTimeOffset.Now);
            }

            string path_DatasetReferences = Path.Combine(outputDirectory, Constants.FileName.DatasetReferences);
            List<DatasetReference>? datasetReferences = Query.DatasetReferences(path_DatasetReferences);
            if (datasetReferences is null)
            {
                Fail(nameof(Query.DatasetReferences), string.Format(CultureInfo.InvariantCulture, "The dataset manifest {0} could not be read.", path_DatasetReferences));
                return Result();
            }

            List<string> weightsPaths = yOLOTrainingDatasetOptions.WeightsPaths is null ? [] : [.. yOLOTrainingDatasetOptions.WeightsPaths.Where(x => !string.IsNullOrWhiteSpace(x))];
            if (weightsPaths.Count == 0)
            {
                Fail(nameof(YOLOTrainingDatasetOptions.WeightsPaths), "No weights file was named - list the production weights and every candidate in WeightsPaths.");
                return Result();
            }

            string? directory_Images = new YOLOModel(outputDirectory).GetDirectory_Images(DiGi.YOLO.Enums.Category.Test);
            if (string.IsNullOrWhiteSpace(directory_Images) || !Directory.Exists(directory_Images))
            {
                Fail(nameof(DiGi.YOLO.Enums.Category.Test), string.Format(CultureInfo.InvariantCulture, "The dataset has no Test images at {0}.", directory_Images ?? outputDirectory));
                return Result();
            }

            HashSet<string> references_Imaged = new(StringComparer.Ordinal);
            foreach (string path_Image in Directory.EnumerateFiles(directory_Images, "*.jpeg"))
            {
                if (Query.TryParseImageFileName(path_Image, out string? reference, out _))
                {
                    references_Imaged.Add(reference!);
                }
            }

            int count_Unknown = datasetReferences.Count(x => x.Category == DiGi.YOLO.Enums.Category.Test && x.LegacySource == LegacySource.Unknown && references_Imaged.Contains(x.Reference!));
            if (count_Unknown != 0)
            {
                messages.Add(string.Format(CultureInfo.InvariantCulture, "{0} Test building(s) have no Legacy decision - their history could not be read - and are left out of the clean subset.", count_Unknown));
            }

            string directory_Evaluation = Path.Combine(outputDirectory, "evaluation");
            Directory.CreateDirectory(directory_Evaluation);

            for (int i = 0; i < weightsPaths.Count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string weightsPath = weightsPaths[i];

                string? path_Weights = Query.ModelPath(weightsPath);
                if (string.IsNullOrWhiteSpace(path_Weights) || !File.Exists(path_Weights))
                {
                    Fail(nameof(Query.ModelPath), string.Format(CultureInfo.InvariantCulture, "The weights file was not found - {0}.", weightsPath));
                    continue;
                }

                string? sHA256 = DiGi.YOLO.Query.FileSHA256(path_Weights);

                string path_Results = Path.Combine(directory_Evaluation, string.Format(CultureInfo.InvariantCulture, "{0:00}_{1}.bbrf", i, Path.GetFileNameWithoutExtension(path_Weights)));

                YOLOPredictionOptions? yOLOPredictionOptions = DiGi.YOLO.Create.YOLOPredictionOptions(yOLOTrainingDatasetOptions.PythonPath, path_Weights, directory_Images, path_Results, yOLOTrainingDatasetOptions.WorkingDirectory, yOLOTrainingDatasetOptions.Confidence);
                if (yOLOPredictionOptions is null)
                {
                    Fail(nameof(DiGi.YOLO.Create.YOLOPredictionOptions), string.Format(CultureInfo.InvariantCulture, "The detector could not be set up for {0} - check PythonPath.", weightsPath));
                    continue;
                }

                Serilog.Modify.Log("{Method}: running {Weights} (SHA-256 {SHA256}) over {ImageDirectory}", nameof(EvaluateYOLODetectors), path_Weights, sHA256 ?? string.Empty, directory_Images);

                YOLOPredictionResult? yOLOPredictionResult = DiGi.YOLO.Modify.Predict(yOLOPredictionOptions, cancellationToken);
                BoundingBoxResultFile? boundingBoxResultFile = DiGi.YOLO.Create.BoundingBoxResultFile(yOLOPredictionResult);
                if (boundingBoxResultFile is null)
                {
                    if (yOLOPredictionResult?.StandardError is List<string> standardError && standardError.Count != 0)
                    {
                        messages.Add(string.Join("; ", standardError));
                    }

                    Fail(nameof(DiGi.YOLO.Modify.Predict), string.Format(CultureInfo.InvariantCulture, "The detector did not run for {0}.", weightsPath));
                    continue;
                }

                List<YOLODetectorEvaluation> yOLODetectorEvaluations_Weights = Query.YOLODetectorEvaluations(datasetReferences, boundingBoxResultFile, references_Imaged, path_Weights, sHA256, yOLOTrainingDatasetOptions.Years);
                yOLODetectorEvaluations.AddRange(yOLODetectorEvaluations_Weights);
            }

            return Result();
        }
    }
}
