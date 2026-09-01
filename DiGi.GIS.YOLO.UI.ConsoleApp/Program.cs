using DiGi.GIS.IO.Interfaces;
using DiGi.GIS.ML.Classes;
using DiGi.GIS.WebAPI.Classes;
using DiGi.GIS.YOLO.UI.Classes;
using DiGi.YOLO.Classes;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// <returns>
        /// An exit code indicating the result:
        /// <list type="bullet">
        /// <item><description>0: Pipeline executed successfully.</description></item>
        /// <item><description>1: Configuration or argument validation error.</description></item>
        /// <item><description>2: Preflight environment check failed.</description></item>
        /// <item><description>3: WebAPI key or client configuration missing.</description></item>
        /// <item><description>4: Pipeline execution failure.</description></item>
        /// <item><description>5: Execution cancelled by user.</description></item>
        /// </list>
        /// </returns>
        public static async Task<int> Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine(" DiGi.GIS.YOLO.UI Headless Prediction Runner");
            Console.WriteLine("=================================================");

            string? path_Options = args.Length > 0 ? args[0] : null;
            YearBuiltPredictionPipelineOptions? options = Query.YearBuiltPredictionPipelineOptions(path_Options);

            if (options is null)
            {
                string message = $"Year Built prediction pipeline options could not be loaded from {(string.IsNullOrWhiteSpace(path_Options) ? "default location" : path_Options)}.";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {message}");
                Console.ResetColor();
                Console.WriteLine("Usage: DiGi.GIS.YOLO.UI.ConsoleApp [path-to-options.json]");
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, message);
                return 1;
            }

            if (options.CountyIds is null || !options.CountyIds.Any(x => x > 0))
            {
                string message = "Pipeline options must specify at least one positive CountyId.";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {message}");
                Console.ResetColor();
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, message);
                return 1;
            }

            if (string.IsNullOrWhiteSpace(options.ScratchDirectory))
            {
                string message = "Pipeline options must specify a non-empty ScratchDirectory.";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {message}");
                Console.ResetColor();
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, message);
                return 1;
            }

            string? key = Query.Key();
            if (string.IsNullOrWhiteSpace(key))
            {
                string message = $"WebAPI authorization key not found in '{Constants.FileName.GISWebAPIClientConfigurationFile}'.";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {message}");
                Console.ResetColor();
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, message);
                return 3;
            }

            GISWebAPIManager? gisWebAPIManager = WebAPI.Create.GISWebAPIManager(key);
            if (gisWebAPIManager is null)
            {
                string message = "Failed to initialize GISWebAPIManager with the provided key.";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {message}");
                Console.ResetColor();
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, message);
                return 3;
            }

            using CancellationTokenSource cancellationTokenSource = new();
            Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs eventArgs) =>
            {
                eventArgs.Cancel = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[INFO] Cancellation requested by user (Ctrl+C)...");
                Console.ResetColor();
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "Cancellation requested by user");
                cancellationTokenSource.Cancel();
            };

            string? modelPath_Resolved = Query.ModelPath(options.ModelPath);
            if (!string.IsNullOrWhiteSpace(modelPath_Resolved))
            {
                options.ModelPath = modelPath_Resolved;
            }

            Console.WriteLine("[INFO] Running YOLO environment preflight check...");
            YOLOEnvironmentResult? yoloEnvironmentResult = DiGi.YOLO.Query.YOLOEnvironmentResult(options.PythonPath, options.ModelPath, options.WorkingDirectory, cancellationTokenSource.Token);
            if (yoloEnvironmentResult is null || !yoloEnvironmentResult.Runnable)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] YOLO environment preflight check failed. Environment is not runnable.");
                if (yoloEnvironmentResult != null && yoloEnvironmentResult.Messages != null)
                {
                    foreach (string message_Temp in yoloEnvironmentResult.Messages)
                    {
                        Console.WriteLine($"  - {message_Temp}");
                        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "Preflight: {Message}", message_Temp);
                    }
                }
                Console.ResetColor();
                return 2;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[OK] YOLO environment preflight check passed.");
            Console.ResetColor();

            IYearBuiltPredictor yearBuiltPredictor = new YearBuiltPredictor();

            Progress<long> progress = new(count =>
            {
                Console.WriteLine($"[PROGRESS] Processed {count} items...");
            });

            Console.WriteLine($"[INFO] Starting Year Built prediction pipeline for county IDs: {string.Join(", ", options.CountyIds)}");
            Serilog.Modify.Log("Starting Year Built prediction pipeline for county IDs: {CountyIds}", string.Join(", ", options.CountyIds));

            YearBuiltPredictionResult? result;
            try
            {
                result = await gisWebAPIManager.RunYearBuiltPredictionsAsync(yearBuiltPredictor, options, progress, cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[INFO] Pipeline execution cancelled.");
                Console.ResetColor();
                return 5;
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FATAL] Unhandled pipeline error: {exception.Message}");
                Console.ResetColor();
                Serilog.Modify.Log(exception, "Unhandled error during pipeline execution");
                return 4;
            }

            if (cancellationTokenSource.IsCancellationRequested || (result != null && result.Cancelled))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[INFO] Pipeline execution cancelled.");
                Console.ResetColor();
                return 5;
            }

            if (result is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Pipeline execution returned a null result.");
                Console.ResetColor();
                return 4;
            }

            if (result.FailedStepNames != null && result.FailedStepNames.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] Pipeline completed with {result.FailedStepNames.Count} failed step(s):");
                foreach (string step in result.FailedStepNames)
                {
                    Console.WriteLine($"  - {step}");
                }
                Console.ResetColor();
                return 4;
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

            return 0;
        }
    }
}
