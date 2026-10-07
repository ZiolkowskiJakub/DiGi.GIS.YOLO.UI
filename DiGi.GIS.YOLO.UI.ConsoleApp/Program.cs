using DiGi.GIS.IO.Interfaces;
using DiGi.GIS.ML.Classes;
using DiGi.GIS.WebAPI.Classes;
using DiGi.GIS.YOLO.UI.Classes;
using DiGi.GIS.YOLO.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.YOLO.UI.ConsoleApp
{
    /// <summary>
    /// Provides the main entry point for the headless YOLO Year Built prediction runner.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Executes the headless Year Built prediction pipeline from command-line arguments.
        /// </summary>
        /// <param name="args">Optional arguments. With no flag, the first argument is the path of the prediction options file. A leading <c>--dataset</c>, <c>--check-labels</c> or <c>--evaluate-detector</c> selects a training dataset mode instead, and the argument after it is the path of the <see cref="YOLOTrainingDatasetOptions"/> file. A leading <c>--train</c> runs the whole retraining, and the argument after it is the path of the <see cref="YOLOTrainingRunOptions"/> file.</param>
        /// <returns>One of <see cref="Enums.YearBuiltPredictionExitCode"/> as an integer. Only <see cref="Enums.YearBuiltPredictionExitCode.Succeeded"/> means a run finished; the rest say why one did not, and a caller reads them through that enumeration rather than against literals of its own.</returns>
        public static async Task<int> Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine(" DiGi.GIS.YOLO.UI Headless Prediction Runner");
            Console.WriteLine("=================================================");

            int Fail(string message, YearBuiltPredictionExitCode yearBuiltPredictionExitCode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {message}");
                Console.ResetColor();
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, message);
                return (int)yearBuiltPredictionExitCode;
            }

            // A leading flag selects one of the training dataset modes; no flag keeps the prediction run, so every
            // caller that passes just an options path - the tray application among them - is unchanged.
            if (args.Length > 0 && args[0] == "--train")
            {
                return await TrainModeAsync(args.Length > 1 ? args[1] : null);
            }

            if (args.Length > 0 && args[0].StartsWith("--", StringComparison.Ordinal))
            {
                return await DatasetModeAsync(args[0], args.Length > 1 ? args[1] : null);
            }

            string? path_Options = args.Length > 0 ? args[0] : null;
            YearBuiltPredictionPipelineOptions? options = Query.YearBuiltPredictionPipelineOptions(path_Options);

            if (options is null)
            {
                Console.WriteLine(Usage);
                return Fail($"Year Built prediction pipeline options could not be loaded from {(string.IsNullOrWhiteSpace(path_Options) ? "default location" : path_Options)}.", YearBuiltPredictionExitCode.Configuration);
            }

            if (options.CountyIds is null || !options.CountyIds.Any(x => x > 0))
            {
                return Fail("Pipeline options must specify at least one positive CountyId.", YearBuiltPredictionExitCode.Configuration);
            }

            if (string.IsNullOrWhiteSpace(options.ScratchDirectory))
            {
                return Fail("Pipeline options must specify a non-empty ScratchDirectory.", YearBuiltPredictionExitCode.Configuration);
            }

            string? key = Query.Key();
            if (string.IsNullOrWhiteSpace(key))
            {
                return Fail($"WebAPI authorization key not found in '{Constants.FileName.GISWebAPIClientConfigurationFile}'.", YearBuiltPredictionExitCode.Authorization);
            }

            GISWebAPIManager? gisWebAPIManager = WebAPI.Create.GISWebAPIManager(key);
            if (gisWebAPIManager is null)
            {
                return Fail("Failed to initialize GISWebAPIManager with the provided key.", YearBuiltPredictionExitCode.Authorization);
            }

            string? modelPath_Resolved = Query.ModelPath(options.ModelPath);
            if (!string.IsNullOrWhiteSpace(modelPath_Resolved))
            {
                options.ModelPath = modelPath_Resolved;
            }

            // Not disposed through a using: the handler below outlives the statement it would be scoped to, and
            // cancelling through a disposed source throws inside the handler rather than stopping the run. It is
            // detached and disposed together in the finally.
            CancellationTokenSource cancellationTokenSource = new();

            void CancelKeyPress(object? sender, ConsoleCancelEventArgs eventArgs)
            {
                eventArgs.Cancel = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[INFO] Cancellation requested by user (Ctrl+C)...");
                Console.ResetColor();
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Cancellation requested by user");
                cancellationTokenSource.Cancel();
            }

            Console.CancelKeyPress += CancelKeyPress;

            try
            {
                // Through the shared builder rather than a format literal: the tray application's background task
                // reads these lines back with Query.ProgressCount, and a format written twice is a contract nothing
                // checks.
                Progress<long> progress = new(count =>
                {
                    Console.WriteLine(Create.ProgressMessage(count));
                });

                IYearBuiltPredictor yearBuiltPredictor = new YearBuiltPredictor();

                Console.WriteLine($"[INFO] Starting Year Built prediction pipeline for county IDs: {string.Join(", ", options.CountyIds)}");
                Serilog.Modify.Log("Starting Year Built prediction pipeline for county IDs: {CountyIds}", string.Join(", ", options.CountyIds));

                YearBuiltPredictionResult? result;
                try
                {
                    // The environment preflight is the orchestrator's own first step, so it is not repeated here -
                    // each run of it starts an interpreter, and a second one could only agree with the first.
                    result = await gisWebAPIManager.RunYearBuiltPredictionsAsync(yearBuiltPredictor, options, progress, cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[INFO] Pipeline execution cancelled.");
                    Console.ResetColor();
                    return (int)YearBuiltPredictionExitCode.Cancelled;
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[FATAL] Unhandled pipeline error: {exception.Message}");
                    Console.ResetColor();
                    Serilog.Modify.Log(exception, "Unhandled error during pipeline execution");
                    return (int)YearBuiltPredictionExitCode.Failed;
                }

                // Everything the run has to say beyond its tallies arrives here - the mis-scoped county, the county
                // rows it could not read. Printing only the counts is how a run that did nothing reads as a run that
                // found nothing.
                if (result?.Messages is List<string> messages && messages.Count != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    foreach (string message in messages)
                    {
                        Console.WriteLine($"[NOTE] {message}");
                    }
                    Console.ResetColor();
                }

                if (cancellationTokenSource.IsCancellationRequested || (result != null && result.Cancelled))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[INFO] Pipeline execution cancelled.");
                    Console.ResetColor();
                    return (int)YearBuiltPredictionExitCode.Cancelled;
                }

                if (result is null)
                {
                    return Fail("Pipeline execution returned a null result.", YearBuiltPredictionExitCode.Failed);
                }

                if (result.FailedStepNames is List<string> failedStepNames && failedStepNames.Count != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[ERROR] Pipeline completed with {failedStepNames.Count} failed step(s):");
                    foreach (string failedStepName in failedStepNames)
                    {
                        Console.WriteLine($"  - {failedStepName}");
                    }
                    Console.ResetColor();

                    // The preflights keep their own exit code: a machine that cannot run the detector at all, or
                    // score with the model it was given, is a different thing to fix than a step that failed while
                    // running. The feature-contract refusal is a mistake in the options file, not in the machine, so
                    // it is a Configuration rather than an Environment, and so is a reference manifest that does not read.
                    if (failedStepNames.Contains(nameof(DiGi.GIS.IO.Query.YearBuiltPredictionInputColumnNames)) || failedStepNames.Contains(nameof(YearBuiltPredictionPipelineOptions.ReferencesFilePath)))
                    {
                        return (int)YearBuiltPredictionExitCode.Configuration;
                    }

                    bool preflightFailed = failedStepNames.Contains(nameof(DiGi.YOLO.Query.YOLOEnvironmentResult))
                        || failedStepNames.Contains(nameof(Query.ModelPath))
                        || failedStepNames.Contains(nameof(DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness));
                    return (int)(preflightFailed ? YearBuiltPredictionExitCode.Environment : YearBuiltPredictionExitCode.Failed);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=================================================");
                Console.WriteLine(" Pipeline execution completed successfully!");
                Console.WriteLine($" Buildings: {result.BuildingCount}");
                Console.WriteLine($" Images: {result.ImageCount}");
                Console.WriteLine($" Detections: {result.DetectionCount}");
                Console.WriteLine($" Predictions: {result.PredictionCount}");
                Console.WriteLine($" Building Data updated: {result.BuildingDataUpdatedCount}");
                Console.WriteLine($" Year Built Data updated: {result.YearBuiltDataUpdatedCount}");
                Console.WriteLine("=================================================");
                Console.ResetColor();

                return (int)YearBuiltPredictionExitCode.Succeeded;
            }
            finally
            {
                Console.CancelKeyPress -= CancelKeyPress;
                cancellationTokenSource.Dispose();
            }
        }

        /// <summary>
        /// The usage text printed when the arguments or the options cannot be read.
        /// </summary>
        private const string Usage = "Usage: DiGi.GIS.YOLO.UI.ConsoleApp [path-to-options.json]\n       DiGi.GIS.YOLO.UI.ConsoleApp --dataset|--check-labels|--evaluate-detector [path-to-YOLOTrainingDatasetOptions.json]\n       DiGi.GIS.YOLO.UI.ConsoleApp --train [path-to-YOLOTrainingRunOptions.json]";

        /// <summary>
        /// Runs one of the YOLO training dataset modes: <c>--dataset</c> builds (or, with <see cref="YOLOTrainingDatasetOptions.CountOnly"/>, only counts) a training dataset from the deployed data, <c>--check-labels</c> checks its label boxes against the current detector, and <c>--evaluate-detector</c> compares weights files on its Test buildings.
        /// <para>The exit codes are the prediction run's: <see cref="YearBuiltPredictionExitCode.Configuration"/> for options that cannot be used, <see cref="YearBuiltPredictionExitCode.Environment"/> for weights or an interpreter the machine does not have, <see cref="YearBuiltPredictionExitCode.Authorization"/> for a missing key (<c>--dataset</c> only - the other two read nothing from the Web API), <see cref="YearBuiltPredictionExitCode.Failed"/> for a step that failed while running, and <see cref="YearBuiltPredictionExitCode.Cancelled"/>.</para>
        /// </summary>
        /// <param name="mode">The mode flag.</param>
        /// <param name="path_Options">The path of the options file, or null for <see cref="Constants.FileName.YOLOTrainingDatasetOptions"/> beside the executable.</param>
        /// <returns>One of <see cref="YearBuiltPredictionExitCode"/> as an integer.</returns>
        private static async Task<int> DatasetModeAsync(string mode, string? path_Options)
        {
            int Fail(string message, YearBuiltPredictionExitCode yearBuiltPredictionExitCode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {message}");
                Console.ResetColor();
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, message);
                return (int)yearBuiltPredictionExitCode;
            }

            void Notes(IEnumerable<string>? messages)
            {
                if (messages is null)
                {
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (string message in messages)
                {
                    Console.WriteLine($"[NOTE] {message}");
                }

                Console.ResetColor();
            }

            int Failed(List<string> failedStepNames, IEnumerable<string> stepNames_Configuration, IEnumerable<string> stepNames_Environment)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] Completed with {failedStepNames.Count} failed step(s):");
                foreach (string failedStepName in failedStepNames)
                {
                    Console.WriteLine($"  - {failedStepName}");
                }

                Console.ResetColor();

                if (failedStepNames.Intersect(stepNames_Configuration).Any())
                {
                    return (int)YearBuiltPredictionExitCode.Configuration;
                }

                if (failedStepNames.Intersect(stepNames_Environment).Any())
                {
                    return (int)YearBuiltPredictionExitCode.Environment;
                }

                return (int)YearBuiltPredictionExitCode.Failed;
            }

            if (mode != "--dataset" && mode != "--check-labels" && mode != "--evaluate-detector")
            {
                Console.WriteLine(Usage);
                return Fail($"Unknown mode '{mode}'.", YearBuiltPredictionExitCode.Configuration);
            }

            YOLOTrainingDatasetOptions? options = Query.YOLOTrainingDatasetOptions(path_Options);
            if (options is null)
            {
                Console.WriteLine(Usage);
                return Fail($"YOLO training dataset options could not be loaded from {(string.IsNullOrWhiteSpace(path_Options) ? "default location" : path_Options)}.", YearBuiltPredictionExitCode.Configuration);
            }

            if (string.IsNullOrWhiteSpace(options.OutputDirectory) || !System.IO.Path.IsPathRooted(options.OutputDirectory))
            {
                return Fail("The options must name an absolute OutputDirectory - the dataset root.", YearBuiltPredictionExitCode.Configuration);
            }

            GISWebAPIManager? gisWebAPIManager = null;
            if (mode == "--dataset")
            {
                if (options.CountyIds is null || !options.CountyIds.Any(x => x > 0))
                {
                    return Fail("The options must name at least one positive CountyId.", YearBuiltPredictionExitCode.Configuration);
                }

                string? key = Query.Key();
                if (string.IsNullOrWhiteSpace(key))
                {
                    return Fail($"WebAPI authorization key not found in '{Constants.FileName.GISWebAPIClientConfigurationFile}'.", YearBuiltPredictionExitCode.Authorization);
                }

                gisWebAPIManager = WebAPI.Create.GISWebAPIManager(key);
                if (gisWebAPIManager is null)
                {
                    return Fail("Failed to initialize GISWebAPIManager with the provided key.", YearBuiltPredictionExitCode.Authorization);
                }
            }

            // Not disposed through a using, for the same reason as the prediction run: the handler outlives the statement.
            CancellationTokenSource cancellationTokenSource = new();

            void CancelKeyPress(object? sender, ConsoleCancelEventArgs eventArgs)
            {
                eventArgs.Cancel = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[INFO] Cancellation requested by user (Ctrl+C)...");
                Console.ResetColor();
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Cancellation requested by user");
                cancellationTokenSource.Cancel();
            }

            Console.CancelKeyPress += CancelKeyPress;

            try
            {
                if (mode == "--dataset")
                {
                    Progress<long> progress = new(count =>
                    {
                        Console.WriteLine(Create.ProgressMessage(count));
                    });

                    YOLOTrainingDatasetResult? result = await gisWebAPIManager.AppendYOLOTrainingDatasetAsync(options, progress, cancellationTokenSource.Token);
                    if (result is null)
                    {
                        return Fail("The training dataset could not be attempted - see the log.", YearBuiltPredictionExitCode.Configuration);
                    }

                    Notes(result.Messages);

                    Console.WriteLine(result.CountOnly ? "[INFO] Count only - no orthophoto was requested and nothing was written." : $"[INFO] Dataset: {result.OutputDirectory}");
                    Console.WriteLine("County\tLabelled\tDuplicates\tConflicts\tBuildings\tTrain\tValidate\tTest\tLegacyTsv\tLegacyTimestamp\tLegacyBoth\tLegacyNone\tLegacyUnknown\tBounded\tRefDuplicates\tEstRequests\tEstImages\tEstBytes\tResumed\tNoFootprint\tNoImagery\tFailed\tImages\tPositive\tNegative\tClamped\tDroppedBoxes\tSameYear\tIdenticalDropped\tIdenticalMerged");

                    List<YOLOTrainingDatasetCount> yOLOTrainingDatasetCounts = result.YOLOTrainingDatasetCounts;
                    if (result.Total is YOLOTrainingDatasetCount total)
                    {
                        yOLOTrainingDatasetCounts.Add(total);
                    }

                    foreach (YOLOTrainingDatasetCount yOLOTrainingDatasetCount in yOLOTrainingDatasetCounts)
                    {
                        Console.WriteLine(string.Join("\t",
                            yOLOTrainingDatasetCount.CountyId?.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? "Total",
                            yOLOTrainingDatasetCount.LabelledCount,
                            yOLOTrainingDatasetCount.DuplicateReferenceCount,
                            yOLOTrainingDatasetCount.LabelConflictCount,
                            yOLOTrainingDatasetCount.BuildingCount,
                            yOLOTrainingDatasetCount.TrainCount,
                            yOLOTrainingDatasetCount.ValidateCount,
                            yOLOTrainingDatasetCount.TestCount,
                            yOLOTrainingDatasetCount.LegacyTsvCount,
                            yOLOTrainingDatasetCount.LegacyTimestampCount,
                            yOLOTrainingDatasetCount.LegacyBothCount,
                            yOLOTrainingDatasetCount.LegacyNoneCount,
                            yOLOTrainingDatasetCount.LegacyUnknownCount,
                            yOLOTrainingDatasetCount.BoundedEntryCount,
                            yOLOTrainingDatasetCount.ReferenceDuplicateCount,
                            yOLOTrainingDatasetCount.EstimatedRequestCount,
                            yOLOTrainingDatasetCount.EstimatedImageCount,
                            yOLOTrainingDatasetCount.EstimatedByteCount,
                            yOLOTrainingDatasetCount.ResumedCount,
                            yOLOTrainingDatasetCount.WithoutFootprintCount,
                            yOLOTrainingDatasetCount.WithoutImageryCount,
                            yOLOTrainingDatasetCount.FailedBuildingCount,
                            yOLOTrainingDatasetCount.ImageCount,
                            yOLOTrainingDatasetCount.PositiveImageCount,
                            yOLOTrainingDatasetCount.NegativeImageCount,
                            yOLOTrainingDatasetCount.ClampedBoxCount,
                            yOLOTrainingDatasetCount.DroppedBoxCount,
                            yOLOTrainingDatasetCount.SameYearImageCount,
                            yOLOTrainingDatasetCount.IdenticalImageDroppedCount,
                            yOLOTrainingDatasetCount.IdenticalImageMergedCount));
                    }

                    if (cancellationTokenSource.IsCancellationRequested || result.Cancelled)
                    {
                        Console.WriteLine("[INFO] Cancelled - the manifest lets a re-run continue.");
                        return (int)YearBuiltPredictionExitCode.Cancelled;
                    }

                    if (result.FailedStepNames is List<string> failedStepNames_Dataset && failedStepNames_Dataset.Count != 0)
                    {
                        return Failed(failedStepNames_Dataset, [nameof(Query.UnknownCountyIds), nameof(Query.LegacyReferences), nameof(YOLOTrainingDatasetOptions.OutputDirectory), nameof(YOLOTrainingDatasetOptions.Resume), nameof(Constants.LabelName)], []);
                    }

                    return (int)YearBuiltPredictionExitCode.Succeeded;
                }

                List<string> stepNames_Configuration = [nameof(Query.DatasetReferences), nameof(DiGi.YOLO.Modify.Read), nameof(YOLOTrainingDatasetOptions.WeightsPaths), nameof(DiGi.YOLO.Enums.Category.Train), nameof(DiGi.YOLO.Enums.Category.Test)];
                List<string> stepNames_Environment = [nameof(Query.ModelPath), nameof(DiGi.YOLO.Create.YOLOPredictionOptions)];

                if (mode == "--check-labels")
                {
                    YOLOLabelCheckResult? result = options.CheckYOLOTrainingDatasetLabels(cancellationTokenSource.Token);
                    if (result is null)
                    {
                        return Fail("The label check could not be attempted - see the log.", YearBuiltPredictionExitCode.Configuration);
                    }

                    Notes(result.Messages);

                    Console.WriteLine($"[INFO] Label check over {result.SampleCount} positive image(s), {result.DetectedCount} with a detection:");
                    Console.WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "  IoU mean {0:0.000}, median {1:0.000}, share >= 0.5 {2:0.000}", result.MeanIntersectionOverUnion, result.MedianIntersectionOverUnion, result.ShareAboveHalf));
                    foreach (KeyValuePair<string, double> keyValuePair in result.CountyIntersectionOverUnions)
                    {
                        Console.WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "  County {0}: IoU mean {1:0.000}", string.IsNullOrEmpty(keyValuePair.Key) ? "(not in manifest)" : keyValuePair.Key, keyValuePair.Value));
                    }

                    Console.WriteLine($"[INFO] Overlays: {result.OverlayDirectory}");

                    if (result.FailedStepNames is List<string> failedStepNames_Check && failedStepNames_Check.Count != 0)
                    {
                        return Failed(failedStepNames_Check, stepNames_Configuration, stepNames_Environment);
                    }

                    return (int)YearBuiltPredictionExitCode.Succeeded;
                }

                YOLODetectorEvaluationResult? yOLODetectorEvaluationResult = options.EvaluateYOLODetectors(cancellationTokenSource.Token);
                if (yOLODetectorEvaluationResult is null)
                {
                    return Fail("The detector evaluation could not be attempted - see the log.", YearBuiltPredictionExitCode.Configuration);
                }

                Notes(yOLODetectorEvaluationResult.Messages);

                Console.WriteLine("Weights\tSHA256\tSubset\tCount\tMAE\tRMSE\tExact");
                foreach (YOLODetectorEvaluation yOLODetectorEvaluation in yOLODetectorEvaluationResult.YOLODetectorEvaluations)
                {
                    Console.WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}\t{1}\t{2}\t{3}\t{4:0.000}\t{5:0.000}\t{6:0.000}", yOLODetectorEvaluation.WeightsPath, yOLODetectorEvaluation.SHA256, yOLODetectorEvaluation.Subset, yOLODetectorEvaluation.Count, yOLODetectorEvaluation.MeanAbsoluteError, yOLODetectorEvaluation.RootMeanSquareError, yOLODetectorEvaluation.ExactShare));
                }

                if (yOLODetectorEvaluationResult.FailedStepNames is List<string> failedStepNames_Evaluation && failedStepNames_Evaluation.Count != 0)
                {
                    return Failed(failedStepNames_Evaluation, stepNames_Configuration, stepNames_Environment);
                }

                return (int)YearBuiltPredictionExitCode.Succeeded;
            }
            catch (OperationCanceledException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[INFO] Execution cancelled.");
                Console.ResetColor();
                return (int)YearBuiltPredictionExitCode.Cancelled;
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FATAL] Unhandled error: {exception.Message}");
                Console.ResetColor();
                Serilog.Modify.Log(exception, "Unhandled error in {Mode}", mode);
                return (int)YearBuiltPredictionExitCode.Failed;
            }
            finally
            {
                Console.CancelKeyPress -= CancelKeyPress;
                cancellationTokenSource.Dispose();
            }
        }

        /// <summary>
        /// Runs the <c>--train</c> mode: the dataset build, the label check, the training, the validation on the Test split and the detector evaluation as one run that stops at the first failed step.
        /// <para>A run whose <see cref="Classes.YOLOTrainingRunOptions.ResumeTraining"/> is set continues the interrupted run of the same name instead of starting a new one; its start row says which epoch it resumed from.</para>
        /// <para>The start weights and the output weights are reported with their SHA-256 as soon as each is known, and the identities and the evaluation rows are printed again at the end, so the table that gates a candidate names exactly which file each row is. The exit codes are <see cref="YearBuiltPredictionExitCode"/>, mapped by <see cref="Query.YOLOTrainingRunExitCode(YOLOTrainingRunResult?)"/>.</para>
        /// </summary>
        /// <param name="path_Options">The path of the options file, or null for <see cref="Constants.FileName.YOLOTrainingRunOptions"/> beside the executable.</param>
        /// <returns>One of <see cref="YearBuiltPredictionExitCode"/> as an integer.</returns>
        private static async Task<int> TrainModeAsync(string? path_Options)
        {
            static int Fail(string message, YearBuiltPredictionExitCode yearBuiltPredictionExitCode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {message}");
                Console.ResetColor();
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, message);
                return (int)yearBuiltPredictionExitCode;
            }

            YOLOTrainingRunOptions? options = Query.YOLOTrainingRunOptions(path_Options);
            if (options is null)
            {
                Console.WriteLine(Usage);
                return Fail($"YOLO training run options could not be loaded from {(string.IsNullOrWhiteSpace(path_Options) ? "default location" : path_Options)}.", YearBuiltPredictionExitCode.Configuration);
            }

            GISWebAPIManager? gisWebAPIManager = null;
            if (options.Steps is null || options.Steps.Count == 0 || options.Steps.Contains(YOLOTrainingStep.Dataset))
            {
                string? key = Query.Key();
                if (string.IsNullOrWhiteSpace(key))
                {
                    return Fail($"WebAPI authorization key not found in '{Constants.FileName.GISWebAPIClientConfigurationFile}'.", YearBuiltPredictionExitCode.Authorization);
                }

                gisWebAPIManager = WebAPI.Create.GISWebAPIManager(key);
                if (gisWebAPIManager is null)
                {
                    return Fail("Failed to initialize GISWebAPIManager with the provided key.", YearBuiltPredictionExitCode.Authorization);
                }
            }

            // Not disposed through a using, for the same reason as the other modes: the handler outlives the statement.
            CancellationTokenSource cancellationTokenSource = new();

            void CancelKeyPress(object? sender, ConsoleCancelEventArgs eventArgs)
            {
                eventArgs.Cancel = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[INFO] Cancellation requested by user (Ctrl+C)...");
                Console.ResetColor();
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Cancellation requested by user");
                cancellationTokenSource.Cancel();
            }

            Console.CancelKeyPress += CancelKeyPress;

            try
            {
                Progress<long> progress = new(count =>
                {
                    Console.WriteLine(Create.ProgressMessage(count));
                });

                Progress<string> information = new(message =>
                {
                    Console.WriteLine($"[INFO] {message}");
                });

                YOLOTrainingRunResult? result = await gisWebAPIManager.RunYOLOTrainingAsync(options, progress, information, cancellationTokenSource.Token);
                if (result is null)
                {
                    return Fail("The training run could not be attempted - see the log.", YearBuiltPredictionExitCode.Configuration);
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (string message in result.Messages)
                {
                    Console.WriteLine($"[NOTE] {message}");
                }

                Console.ResetColor();

                if (result.AutoResumes.Count != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    // AutoResume.Epoch is the last epoch the checkpoint completed; the summary names the epoch each
                    // attempt was interrupted in, the same number the live "... at epoch E - automatic resume" lines carry.
                    Console.WriteLine($"[NOTE] Resumed automatically {result.AutoResumes.Count} time(s): {string.Join(", ", result.AutoResumes.Select(x => string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0} at epoch {1} ({2:HH:mm:ss})", x.Reason, x.Epoch is int epoch_Completed ? (epoch_Completed + 1).ToString(System.Globalization.CultureInfo.InvariantCulture) : "?", x.Time)))}");
                    Console.ResetColor();
                }

                if (!string.IsNullOrWhiteSpace(result.StartWeightsSHA256) || !string.IsNullOrWhiteSpace(result.WeightsSHA256))
                {
                    string role_Start = result.Resumed && result.ResumedFromEpoch is not null ? string.Format(System.Globalization.CultureInfo.InvariantCulture, "Start (resume of epoch {0})", result.ResumedFromEpoch) : "Start";
                    Console.WriteLine("Role\tWeights\tSHA256");
                    Console.WriteLine($"{role_Start}\t{result.StartWeightsPath}\t{result.StartWeightsSHA256}");
                    Console.WriteLine($"Output\t{result.WeightsPath}\t{result.WeightsSHA256}");
                }

                if (result.MAP50 is not null || result.MAP50_95 is not null)
                {
                    Console.WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "[INFO] Test mAP50 {0:0.000}, mAP50-95 {1:0.000}", result.MAP50, result.MAP50_95));
                }

                List<YOLODetectorEvaluation> yOLODetectorEvaluations = result.YOLODetectorEvaluations;
                if (yOLODetectorEvaluations.Count != 0)
                {
                    Console.WriteLine("Weights\tSHA256\tSubset\tCount\tMAE\tRMSE\tExact");
                    foreach (YOLODetectorEvaluation yOLODetectorEvaluation in yOLODetectorEvaluations)
                    {
                        Console.WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}\t{1}\t{2}\t{3}\t{4:0.000}\t{5:0.000}\t{6:0.000}", yOLODetectorEvaluation.WeightsPath, yOLODetectorEvaluation.SHA256, yOLODetectorEvaluation.Subset, yOLODetectorEvaluation.Count, yOLODetectorEvaluation.MeanAbsoluteError, yOLODetectorEvaluation.RootMeanSquareError, yOLODetectorEvaluation.ExactShare));
                    }
                }

                YearBuiltPredictionExitCode yearBuiltPredictionExitCode = Query.YOLOTrainingRunExitCode(result);
                if (yearBuiltPredictionExitCode != YearBuiltPredictionExitCode.Succeeded && yearBuiltPredictionExitCode != YearBuiltPredictionExitCode.Cancelled)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[ERROR] Completed with {result.FailedStepNames.Count} failed step(s):");
                    foreach (string failedStepName in result.FailedStepNames)
                    {
                        Console.WriteLine($"  - {failedStepName}");
                    }

                    Console.ResetColor();
                }

                return (int)yearBuiltPredictionExitCode;
            }
            catch (OperationCanceledException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[INFO] Execution cancelled.");
                Console.ResetColor();
                return (int)YearBuiltPredictionExitCode.Cancelled;
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FATAL] Unhandled error: {exception.Message}");
                Console.ResetColor();
                Serilog.Modify.Log(exception, "Unhandled error in --train");
                return (int)YearBuiltPredictionExitCode.Failed;
            }
            finally
            {
                Console.CancelKeyPress -= CancelKeyPress;
                cancellationTokenSource.Dispose();
            }
        }
    }
}
