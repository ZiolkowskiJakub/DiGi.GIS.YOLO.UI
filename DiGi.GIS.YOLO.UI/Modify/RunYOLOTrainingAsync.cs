using DiGi.GIS.WebAPI.Classes;
using DiGi.GIS.YOLO.UI.Classes;
using DiGi.GIS.YOLO.UI.Enums;
using DiGi.YOLO.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Runs the detector retraining as one run: builds or appends to the training dataset, checks its labels, trains from the start weights, validates the result on the Test split and scores it against the other detectors.
        /// <para>The steps run in the order of <see cref="YOLOTrainingStep"/> and the run stops at the first one that fails; <see cref="YOLOTrainingRunOptions.Steps"/> narrows them. Every path is made absolute and every refusal that can be known up front - a missing start file, an unusable interpreter, a run name that is taken, a project folder inside a <c>YOLO\models</c> folder - is reported before the first step starts, with the option it concerns as the step name.</para>
        /// <para>The trained weights are copied to <c>&lt;ProjectDirectory&gt;\&lt;RunName&gt;\&lt;RunName&gt;.pt</c>, a new file that is never overwritten and never named <c>model</c>; the validation and the evaluation measure that copy, and its SHA-256 is compared with the one the training reported. The identity of the start weights and of the copy is written to the log and to <paramref name="information"/> as soon as it is known. Without the training step the validation measures the start weights, which gives the baseline a candidate is compared with.</para>
        /// <para>A cancellation is a result with <see cref="YOLOTrainingRunResult.Cancelled"/> set rather than an exception, and what earlier steps wrote is left as it is; the dataset manifest lets a re-run continue.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The client of the Web API, needed only by the <see cref="YOLOTrainingStep.Dataset"/> step.</param>
        /// <param name="yOLOTrainingRunOptions">The options naming the dataset, the start weights, the run and the steps.</param>
        /// <param name="progress">Receives the number of buildings the dataset step has completed.</param>
        /// <param name="information">Receives one line for each thing worth reporting, without a prefix.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>The result, or null when there are no options.</returns>
        public static async Task<YOLOTrainingRunResult?> RunYOLOTrainingAsync(this GISWebAPIManager? gisWebAPIManager, YOLOTrainingRunOptions? yOLOTrainingRunOptions, IProgress<long>? progress = null, IProgress<string>? information = null, CancellationToken cancellationToken = default)
        {
            if (yOLOTrainingRunOptions is null)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: no options", nameof(RunYOLOTrainingAsync));
                return null;
            }

            DateTimeOffset start = DateTimeOffset.Now;
            List<string> failedStepNames = [];
            List<string> messages = [];
            List<YOLODetectorEvaluation> yOLODetectorEvaluations = [];
            string? startWeightsSHA256 = null;
            string? weightsPath = null;
            string? weightsSHA256 = null;
            double? mAP50 = null;
            double? mAP50_95 = null;
            bool cancelled = false;

            string? runName = yOLOTrainingRunOptions.RunName;
            string? startWeightsPath = string.IsNullOrWhiteSpace(yOLOTrainingRunOptions.StartWeightsPath) ? null : Path.GetFullPath(yOLOTrainingRunOptions.StartWeightsPath);

            void Report(string message)
            {
                information?.Report(message);
                Serilog.Modify.Log("{Method}: {Message}", nameof(RunYOLOTrainingAsync), message);
            }

            void Fail(string stepName, string message)
            {
                if (!failedStepNames.Contains(stepName))
                {
                    failedStepNames.Add(stepName);
                }

                messages.Add(message);

                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "{Method}: step {Step} did not complete - {Message}", nameof(RunYOLOTrainingAsync), stepName, message);
            }

            YOLOTrainingRunResult Result()
            {
                return new YOLOTrainingRunResult(runName, startWeightsPath, startWeightsSHA256, weightsPath, weightsSHA256, mAP50, mAP50_95, yOLODetectorEvaluations, cancelled, failedStepNames, messages, start, DateTimeOffset.Now);
            }

            List<YOLOTrainingStep> yOLOTrainingSteps = yOLOTrainingRunOptions.Steps is null || yOLOTrainingRunOptions.Steps.Count == 0 ? [.. Enum.GetValues<YOLOTrainingStep>()] : [.. yOLOTrainingRunOptions.Steps.Distinct().OrderBy(x => x)];

            YOLOTrainingDatasetOptions? yOLOTrainingDatasetOptions = yOLOTrainingRunOptions.DatasetOptions;
            string? outputDirectory = yOLOTrainingDatasetOptions?.OutputDirectory;
            if (yOLOTrainingDatasetOptions is null || string.IsNullOrWhiteSpace(outputDirectory) || !Path.IsPathRooted(outputDirectory))
            {
                Fail(nameof(YOLOTrainingRunOptions.DatasetOptions), "The options must name DatasetOptions with an absolute OutputDirectory - the dataset root.");
                return Result();
            }

            outputDirectory = Path.GetFullPath(outputDirectory);

            string? pythonPath = string.IsNullOrWhiteSpace(yOLOTrainingRunOptions.PythonPath) ? yOLOTrainingDatasetOptions.PythonPath : yOLOTrainingRunOptions.PythonPath;
            string? workingDirectory = string.IsNullOrWhiteSpace(yOLOTrainingRunOptions.WorkingDirectory) ? yOLOTrainingDatasetOptions.WorkingDirectory : yOLOTrainingRunOptions.WorkingDirectory;
            if (!string.IsNullOrWhiteSpace(workingDirectory))
            {
                workingDirectory = Path.GetFullPath(workingDirectory);
            }

            string path_Configuration = Path.Combine(outputDirectory, DiGi.YOLO.Constants.FileName.Conf);

            bool train = yOLOTrainingSteps.Contains(YOLOTrainingStep.Train);
            bool validate = yOLOTrainingSteps.Contains(YOLOTrainingStep.Validate);

            // The preflight: everything that can be known without starting a process is checked before the first step,
            // because a dataset build can take hours and a mistyped run name should not be found after it.
            if (yOLOTrainingSteps.Contains(YOLOTrainingStep.Dataset) && gisWebAPIManager is null)
            {
                Fail(nameof(GISWebAPIManager), "The Dataset step needs a Web API client.");
            }

            string? runDirectory = null;
            string? path_Weights = null;
            string? projectDirectory = null;

            if (train || validate)
            {
                if (startWeightsPath is null || !string.Equals(Path.GetExtension(startWeightsPath), ".pt", StringComparison.OrdinalIgnoreCase) || !File.Exists(startWeightsPath))
                {
                    Fail(nameof(YOLOTrainingRunOptions.StartWeightsPath), string.Format(CultureInfo.InvariantCulture, "The start weights must be an existing .pt file: {0}", yOLOTrainingRunOptions.StartWeightsPath ?? "(none)"));
                }
                else
                {
                    startWeightsSHA256 = DiGi.YOLO.Query.FileSHA256(startWeightsPath);
                    Report(string.Format(CultureInfo.InvariantCulture, "Start weights {0} SHA256 {1}", startWeightsPath, startWeightsSHA256 ?? "(unreadable)"));
                }

                if (string.IsNullOrWhiteSpace(pythonPath) || (Path.IsPathRooted(pythonPath) && !File.Exists(pythonPath)))
                {
                    Fail(nameof(YOLOTrainingRunOptions.PythonPath), string.Format(CultureInfo.InvariantCulture, "The Python interpreter is missing or does not exist: {0}", pythonPath ?? "(none)"));
                }
            }

            if (train)
            {
                if (string.IsNullOrWhiteSpace(runName) || runName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || string.Equals(runName, "model", StringComparison.OrdinalIgnoreCase))
                {
                    Fail(nameof(YOLOTrainingRunOptions.RunName), "RunName must be a plain file name and may not be 'model' - the production weights are never a target.");
                }

                if (string.IsNullOrWhiteSpace(yOLOTrainingRunOptions.ProjectDirectory) || !Path.IsPathRooted(yOLOTrainingRunOptions.ProjectDirectory))
                {
                    Fail(nameof(YOLOTrainingRunOptions.ProjectDirectory), "ProjectDirectory must be an absolute path.");
                }
                else
                {
                    projectDirectory = Path.GetFullPath(yOLOTrainingRunOptions.ProjectDirectory);

                    if (DiGi.YOLO.Query.IsInsideModelsDirectory(projectDirectory))
                    {
                        Fail(nameof(YOLOTrainingRunOptions.ProjectDirectory), string.Format(CultureInfo.InvariantCulture, "Refusing to write a run under '{0}': it is inside a YOLO\\models folder, where the frozen weights live.", projectDirectory));
                    }
                    else if (!failedStepNames.Contains(nameof(YOLOTrainingRunOptions.RunName)) && runName is not null)
                    {
                        runDirectory = Path.Combine(projectDirectory, runName);
                        path_Weights = Path.Combine(runDirectory, string.Concat(runName, ".pt"));
                        if (Directory.Exists(runDirectory) || File.Exists(path_Weights))
                        {
                            Fail(nameof(YOLOTrainingRunOptions.RunName), string.Format(CultureInfo.InvariantCulture, "The run '{0}' already exists under {1}; choose a new RunName.", runName, projectDirectory));
                        }
                    }
                }
            }

            if (failedStepNames.Count != 0)
            {
                return Result();
            }

            if (cancellationToken.IsCancellationRequested)
            {
                cancelled = true;
                return Result();
            }

            if (train)
            {
                YOLOEnvironmentResult yOLOEnvironmentResult = DiGi.YOLO.Query.YOLOEnvironmentResult(pythonPath!, startWeightsPath!, workingDirectory ?? outputDirectory, cancellationToken);
                if (!yOLOEnvironmentResult.Runnable)
                {
                    Fail(nameof(DiGi.YOLO.Query.YOLOEnvironmentResult), string.Format(CultureInfo.InvariantCulture, "The interpreter cannot run the training: {0}", string.Join(" ", yOLOEnvironmentResult.Messages ?? [])));
                    return Result();
                }

                pythonPath = yOLOEnvironmentResult.PythonPath ?? pythonPath;
            }

            bool Stopped()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    cancelled = true;
                    Report("Cancelled - what earlier steps wrote is left as it is.");
                }

                return cancelled;
            }

            try
            {
                if (yOLOTrainingSteps.Contains(YOLOTrainingStep.Dataset))
                {
                    YOLOTrainingDatasetResult? yOLOTrainingDatasetResult = await gisWebAPIManager.AppendYOLOTrainingDatasetAsync(yOLOTrainingDatasetOptions, progress, cancellationToken);
                    if (yOLOTrainingDatasetResult is null)
                    {
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Dataset), "The training dataset could not be attempted - see the log.");
                        return Result();
                    }

                    messages.AddRange(yOLOTrainingDatasetResult.Messages ?? []);
                    Report(string.Format(CultureInfo.InvariantCulture, "Dataset {0}: {1} image(s)", yOLOTrainingDatasetResult.OutputDirectory, yOLOTrainingDatasetResult.Total?.ImageCount ?? 0));

                    if (yOLOTrainingDatasetResult.Cancelled || Stopped())
                    {
                        cancelled = true;
                        return Result();
                    }

                    if (yOLOTrainingDatasetResult.FailedStepNames is List<string> failedStepNames_Dataset && failedStepNames_Dataset.Count != 0)
                    {
                        failedStepNames.AddRange(failedStepNames_Dataset.Where(x => !failedStepNames.Contains(x)));
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Dataset), "The dataset step reported a failure.");
                        return Result();
                    }
                }

                if (yOLOTrainingSteps.Contains(YOLOTrainingStep.LabelCheck))
                {
                    YOLOLabelCheckResult? yOLOLabelCheckResult = yOLOTrainingDatasetOptions.CheckYOLOTrainingDatasetLabels(cancellationToken);
                    if (yOLOLabelCheckResult is null)
                    {
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.LabelCheck), "The label check could not be attempted - see the log.");
                        return Result();
                    }

                    messages.AddRange(yOLOLabelCheckResult.Messages);
                    Report(string.Format(CultureInfo.InvariantCulture, "Label check over {0} positive image(s), {1} with a detection: IoU mean {2:0.000}, median {3:0.000}, share >= 0.5 {4:0.000}", yOLOLabelCheckResult.SampleCount, yOLOLabelCheckResult.DetectedCount, yOLOLabelCheckResult.MeanIntersectionOverUnion, yOLOLabelCheckResult.MedianIntersectionOverUnion, yOLOLabelCheckResult.ShareAboveHalf));

                    if (Stopped())
                    {
                        return Result();
                    }

                    if (yOLOLabelCheckResult.FailedStepNames.Count != 0)
                    {
                        failedStepNames.AddRange(yOLOLabelCheckResult.FailedStepNames.Where(x => !failedStepNames.Contains(x)));
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.LabelCheck), "The label check step reported a failure.");
                        return Result();
                    }
                }

                if (train)
                {
                    YOLOTrainingOptions? yOLOTrainingOptions = DiGi.YOLO.Create.YOLOTrainingOptions(pythonPath, startWeightsPath, path_Configuration, workingDirectory);
                    if (yOLOTrainingOptions is null)
                    {
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Train), "The training could not be set up.");
                        return Result();
                    }

                    yOLOTrainingOptions.Batch = yOLOTrainingRunOptions.Batch;
                    yOLOTrainingOptions.Device = yOLOTrainingRunOptions.Device;
                    yOLOTrainingOptions.Epochs = yOLOTrainingRunOptions.Epochs;
                    yOLOTrainingOptions.ImageSize = yOLOTrainingRunOptions.ImageSize;
                    yOLOTrainingOptions.Name = runName;
                    yOLOTrainingOptions.Patience = yOLOTrainingRunOptions.Patience;
                    yOLOTrainingOptions.Project = projectDirectory;
                    yOLOTrainingOptions.Seed = yOLOTrainingRunOptions.Seed;

                    Report(string.Format(CultureInfo.InvariantCulture, "Training run {0} from {1}", runName, startWeightsPath));

                    // Train is synchronous and can take hours; off the caller's thread, so a Ctrl+C handler is never starved.
                    YOLOTrainingResult? yOLOTrainingResult = await Task.Run(() => DiGi.YOLO.Modify.Train(yOLOTrainingOptions, cancellationToken), CancellationToken.None);

                    if (Stopped())
                    {
                        return Result();
                    }

                    if (yOLOTrainingResult is null || !yOLOTrainingResult.Succeeded || string.IsNullOrWhiteSpace(yOLOTrainingResult.WeightsPath) || string.IsNullOrWhiteSpace(yOLOTrainingResult.SHA256))
                    {
                        startWeightsSHA256 ??= yOLOTrainingResult?.StartModelSHA256;
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Train), string.Format(CultureInfo.InvariantCulture, "The training did not produce weights (exit code {0}). {1}", yOLOTrainingResult?.ExitCode.ToString(CultureInfo.InvariantCulture) ?? "none", string.Join(" ", (yOLOTrainingResult?.StandardError ?? []).TakeLast(3))));
                        return Result();
                    }

                    startWeightsSHA256 = yOLOTrainingResult.StartModelSHA256 ?? startWeightsSHA256;

                    try
                    {
                        Directory.CreateDirectory(runDirectory!);
                        File.Copy(yOLOTrainingResult.WeightsPath, path_Weights!, false);
                    }
                    catch (Exception exception)
                    {
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Train), string.Format(CultureInfo.InvariantCulture, "The trained weights {0} could not be copied to {1}: {2}", yOLOTrainingResult.WeightsPath, path_Weights, exception.Message));
                        return Result();
                    }

                    weightsSHA256 = DiGi.YOLO.Query.FileSHA256(path_Weights);
                    if (!string.Equals(weightsSHA256, yOLOTrainingResult.SHA256, StringComparison.OrdinalIgnoreCase))
                    {
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Train), string.Format(CultureInfo.InvariantCulture, "The copy {0} does not match the weights the training reported ({1} against {2}).", path_Weights, weightsSHA256, yOLOTrainingResult.SHA256));
                        weightsSHA256 = null;
                        return Result();
                    }

                    weightsPath = path_Weights;
                    Report(string.Format(CultureInfo.InvariantCulture, "Output weights {0} SHA256 {1}", weightsPath, weightsSHA256));
                }

                if (validate)
                {
                    string weightsPath_Validated = weightsPath ?? startWeightsPath!;

                    YOLOValidationOptions? yOLOValidationOptions = DiGi.YOLO.Create.YOLOValidationOptions(pythonPath, weightsPath_Validated, path_Configuration, workingDirectory);
                    if (yOLOValidationOptions is null)
                    {
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Validate), "The validation could not be set up.");
                        return Result();
                    }

                    yOLOValidationOptions.Batch = yOLOTrainingRunOptions.Batch;
                    yOLOValidationOptions.Device = yOLOTrainingRunOptions.Device;
                    yOLOValidationOptions.ImageSize = yOLOTrainingRunOptions.ImageSize;
                    yOLOValidationOptions.Split = DiGi.YOLO.Enums.Category.Test;

                    YOLOValidationResult? yOLOValidationResult = await Task.Run(() => DiGi.YOLO.Modify.Validate(yOLOValidationOptions, cancellationToken), CancellationToken.None);

                    if (Stopped())
                    {
                        return Result();
                    }

                    if (yOLOValidationResult is null || !yOLOValidationResult.Succeeded)
                    {
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Validate), string.Format(CultureInfo.InvariantCulture, "The validation of {0} did not complete (exit code {1}). {2}", weightsPath_Validated, yOLOValidationResult?.ExitCode.ToString(CultureInfo.InvariantCulture) ?? "none", string.Join(" ", (yOLOValidationResult?.StandardError ?? []).TakeLast(3))));
                        return Result();
                    }

                    mAP50 = yOLOValidationResult.MAP50;
                    mAP50_95 = yOLOValidationResult.MAP50_95;
                    Report(string.Format(CultureInfo.InvariantCulture, "Validation on Test of {0} SHA256 {1}: mAP50 {2:0.000}, mAP50-95 {3:0.000}", weightsPath_Validated, yOLOValidationResult.ModelSHA256, mAP50, mAP50_95));
                }

                if (yOLOTrainingSteps.Contains(YOLOTrainingStep.Evaluate))
                {
                    YOLOTrainingDatasetOptions yOLOTrainingDatasetOptions_Evaluation = new(yOLOTrainingDatasetOptions)
                    {
                        PythonPath = pythonPath,
                        WorkingDirectory = workingDirectory,
                        WeightsPaths = [.. (yOLOTrainingDatasetOptions.WeightsPaths ?? []), .. weightsPath is null ? [] : new List<string> { weightsPath }]
                    };

                    YOLODetectorEvaluationResult? yOLODetectorEvaluationResult = yOLOTrainingDatasetOptions_Evaluation.EvaluateYOLODetectors(cancellationToken);
                    if (yOLODetectorEvaluationResult is null)
                    {
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Evaluate), "The detector evaluation could not be attempted - see the log.");
                        return Result();
                    }

                    messages.AddRange(yOLODetectorEvaluationResult.Messages);
                    yOLODetectorEvaluations = yOLODetectorEvaluationResult.YOLODetectorEvaluations;

                    if (Stopped())
                    {
                        return Result();
                    }

                    if (yOLODetectorEvaluationResult.FailedStepNames.Count != 0)
                    {
                        failedStepNames.AddRange(yOLODetectorEvaluationResult.FailedStepNames.Where(x => !failedStepNames.Contains(x)));
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Evaluate), "The detector evaluation step reported a failure.");
                        return Result();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                cancelled = true;
                Report("Cancelled - what earlier steps wrote is left as it is.");
            }

            return Result();
        }
    }
}
