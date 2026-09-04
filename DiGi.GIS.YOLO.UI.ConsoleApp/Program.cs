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
        /// <param name="args">Optional arguments. The first argument specifies the path to the options JSON file.</param>
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

            string? path_Options = args.Length > 0 ? args[0] : null;
            YearBuiltPredictionPipelineOptions? options = Query.YearBuiltPredictionPipelineOptions(path_Options);

            if (options is null)
            {
                Console.WriteLine("Usage: DiGi.GIS.YOLO.UI.ConsoleApp [path-to-options.json]");
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
                    // running.
                    bool preflightFailed = failedStepNames.Contains(nameof(DiGi.YOLO.Query.YOLOEnvironmentResult))
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
    }
}
