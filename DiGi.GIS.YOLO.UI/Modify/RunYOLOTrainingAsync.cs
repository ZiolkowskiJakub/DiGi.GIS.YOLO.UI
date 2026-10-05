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
        /// <para>With <see cref="YOLOTrainingRunOptions.ResumeTraining"/> the run continues the interrupted checkpoint in <c>&lt;ProjectDirectory&gt;\&lt;RunName&gt;\weights\last.pt</c> instead of starting a new one. The preflight then refuses a run without the <see cref="YOLOTrainingStep.Train"/> step, a selected <see cref="YOLOTrainingStep.Dataset"/> step, a completed run, a missing checkpoint, a finished checkpoint, a checkpoint whose dataset is gone and one whose recorded run folder was moved or renamed - each named by the option it concerns, before the training starts. The tail is identical to a fresh run, and the result reports the resume and the epoch it entered.</para>
        /// <para>With <see cref="YOLOTrainingRunOptions.AutoResumeCount"/> the run continues a training that stalled or crashed, up to that many times and without an operator, each time from the run's own <c>weights\last.pt</c> after copying it aside. A refusal raised before a process started, a finished checkpoint and a stop requested through the token are never resumed; each automatic resume is logged with the epoch it interrupted and recorded in <see cref="YOLOTrainingRunResult.AutoResumes"/>. A crash is preceded in the log by the last <see cref="Constants.Count.CrashErrorOutputLines"/> lines of the attempt's standard error, its only record once the resumed attempt replaces its result.</para>
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
            List<YOLOTrainingAutoResume> autoResumes = [];
            string? startWeightsSHA256 = null;
            string? weightsPath = null;
            string? weightsSHA256 = null;
            double? mAP50 = null;
            double? mAP50_95 = null;
            bool cancelled = false;
            bool resumed = false;
            int? resumedFromEpoch = null;

            string? runName = yOLOTrainingRunOptions.RunName;
            bool resumeTraining = yOLOTrainingRunOptions.ResumeTraining;
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
                return new YOLOTrainingRunResult(runName, startWeightsPath, startWeightsSHA256, weightsPath, weightsSHA256, mAP50, mAP50_95, yOLODetectorEvaluations, cancelled, failedStepNames, messages, start, DateTimeOffset.Now, resumed, resumedFromEpoch, autoResumes);
            }

            // A checkpoint records its dataset path as ultralytics saw it, which may be relative to the working
            // directory the run executed in; the same directory resolves it here.
            static string? ResolveCheckpointDataPath(string? dataPath, string? baseDirectory)
            {
                if (string.IsNullOrWhiteSpace(dataPath))
                {
                    return null;
                }

                if (Path.IsPathRooted(dataPath) || string.IsNullOrWhiteSpace(baseDirectory))
                {
                    return dataPath;
                }

                return Path.Combine(baseDirectory, dataPath);
            }

            // The project and the name a checkpoint records are compared as full paths, case-insensitively, because a
            // run folder is the same one whether or not a trailing separator or a different case was recorded.
            static bool PathsEqual(string? left, string? right)
            {
                if (string.IsNullOrWhiteSpace(left) || string.IsNullOrWhiteSpace(right))
                {
                    return false;
                }

                try
                {
                    return string.Equals(Path.GetFullPath(left), Path.GetFullPath(right), StringComparison.OrdinalIgnoreCase);
                }
                catch
                {
                    return false;
                }
            }

            // The stall reason DiGi.YOLO appends to standard error carries the moment the last line arrived
            // ("... (last output at <timestamp>)"), which is the figure the automatic-resume log line reports.
            static string? StalledLastOutput(IEnumerable<string>? standardError)
            {
                const string marker = "(last output at ";

                if (standardError is null)
                {
                    return null;
                }

                foreach (string line in standardError)
                {
                    int index = line.IndexOf(marker, StringComparison.Ordinal);
                    if (index < 0)
                    {
                        continue;
                    }

                    int end = line.IndexOf(')', index + marker.Length);
                    if (end <= index + marker.Length)
                    {
                        continue;
                    }

                    string value = line.Substring(index + marker.Length, end - index - marker.Length);
                    if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset lastOutput))
                    {
                        return string.Format(CultureInfo.InvariantCulture, " (last output {0:HH:mm:ss})", lastOutput);
                    }

                    return string.Concat(" (last output ", value, ")");
                }

                return null;
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
            string? path_Resume = null;
            string? projectDirectory = null;

            if (resumeTraining)
            {
                // A resume is a continuation, not a rebuild: the dataset is the one the checkpoint was trained on.
                if (!train)
                {
                    Fail(nameof(YOLOTrainingRunOptions.ResumeTraining), "ResumeTraining needs the Train step - the training is the step it continues.");
                }

                if (yOLOTrainingSteps.Contains(YOLOTrainingStep.Dataset))
                {
                    Fail(nameof(YOLOTrainingRunOptions.ResumeTraining), "A resume never rebuilds the dataset it was trained on; remove the Dataset step from Steps.");
                }
            }

            if (train || validate)
            {
                if (!(resumeTraining && train))
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

                        if (resumeTraining)
                        {
                            // The folder is expected to exist on a resume; only the run's own weights file, written after a
                            // completed training, is a refusal, because it marks the run as finished rather than interrupted.
                            if (File.Exists(path_Weights))
                            {
                                Fail(nameof(YOLOTrainingRunOptions.RunName), string.Format(CultureInfo.InvariantCulture, "The run '{0}' completed; choose a new RunName. A completed run is not resumed.", runName));
                            }
                            else
                            {
                                path_Resume = Path.Combine(runDirectory, Constants.DirectoryName.Weights, Constants.FileName.LastWeights);
                                if (!Directory.Exists(runDirectory) || !File.Exists(path_Resume))
                                {
                                    Fail(nameof(YOLOTrainingRunOptions.ResumeTraining), string.Format(CultureInfo.InvariantCulture, "Nothing to resume: {0} has no {1}\\{2}.", runDirectory, Constants.DirectoryName.Weights, Constants.FileName.LastWeights));
                                }
                                else
                                {
                                    // The checkpoint is the start identity of a resume; StartWeightsPath is ignored.
                                    startWeightsPath = path_Resume;
                                }
                            }
                        }
                        else if (Directory.Exists(runDirectory) || File.Exists(path_Weights))
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

                if (resumeTraining && path_Resume is not null && startWeightsPath is not null)
                {
                    // The interpreter is proven runnable before the checkpoint is read, so a broken interpreter stays an
                    // environment failure instead of being answered as an unreadable checkpoint.
                    DiGi.YOLO.Classes.YOLOCheckpointInformation? yOLOCheckpointInformation = DiGi.YOLO.Query.YOLOCheckpointInformation(startWeightsPath, pythonPath, workingDirectory ?? outputDirectory, cancellationToken);

                    if (yOLOCheckpointInformation is null)
                    {
                        Fail(nameof(DiGi.YOLO.Query.YOLOCheckpointInformation), string.Format(CultureInfo.InvariantCulture, "The checkpoint {0} could not be read; nothing to resume.", startWeightsPath));
                        return Result();
                    }

                    if (yOLOCheckpointInformation.Finished)
                    {
                        Fail(nameof(DiGi.YOLO.Query.YOLOCheckpointInformation), string.Format(CultureInfo.InvariantCulture, "The checkpoint {0} is finished; nothing to resume.", startWeightsPath));
                        return Result();
                    }

                    string? path_Data = ResolveCheckpointDataPath(yOLOCheckpointInformation.DataPath, workingDirectory ?? outputDirectory);
                    if (string.IsNullOrWhiteSpace(path_Data) || !File.Exists(path_Data))
                    {
                        Fail(nameof(DiGi.YOLO.Query.YOLOCheckpointInformation), string.Format(CultureInfo.InvariantCulture, "The dataset the checkpoint {0} records is missing or was not named: {1}", startWeightsPath, yOLOCheckpointInformation.DataPath ?? "(none)"));
                        return Result();
                    }

                    if (!string.Equals(yOLOCheckpointInformation.Name, runName, StringComparison.OrdinalIgnoreCase) || !PathsEqual(yOLOCheckpointInformation.Project, projectDirectory))
                    {
                        Fail(nameof(DiGi.YOLO.Query.YOLOCheckpointInformation), string.Format(CultureInfo.InvariantCulture, "The checkpoint {0} records the run folder '{1}\\{2}', which is not '{3}\\{4}'; it was moved or renamed, and ultralytics would write elsewhere.", startWeightsPath, yOLOCheckpointInformation.Project ?? "(none)", yOLOCheckpointInformation.Name ?? "(none)", projectDirectory, runName));
                        return Result();
                    }

                    // The resume is certain from here on: it is recorded even if the training then fails.
                    resumed = true;
                    resumedFromEpoch = yOLOCheckpointInformation.Epoch + 1;
                    startWeightsSHA256 = DiGi.YOLO.Query.FileSHA256(startWeightsPath);
                    Report(string.Format(CultureInfo.InvariantCulture, "Resuming {0} from epoch {1} of {2}, {3} SHA256 {4}", runName, resumedFromEpoch?.ToString(CultureInfo.InvariantCulture) ?? "(unknown)", yOLOCheckpointInformation.Epochs?.ToString(CultureInfo.InvariantCulture) ?? "(unknown)", startWeightsPath, startWeightsSHA256 ?? "(unreadable)"));
                }
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
                    // The checkpoint an automatic resume continues is always the run's own weights\last.pt, whether the
                    // first attempt was fresh or an operator's resume.
                    string? path_LastWeights = runDirectory is null ? null : Path.Combine(runDirectory, Constants.DirectoryName.Weights, Constants.FileName.LastWeights);

                    YOLOTrainingResult? yOLOTrainingResult = null;

                    while (true)
                    {
                        bool autoResume = autoResumes.Count != 0;
                        YOLOTrainingOptions? yOLOTrainingOptions;

                        if (path_Resume is not null)
                        {
                            // A resume sends only the checkpoint, the device and the interpreter context: the checkpoint
                            // restores the dataset, the ceiling, the schedule and the run folder, and the rest of the
                            // options are ignored. Building them directly rather than through Create keeps that honest -
                            // Create would demand a start model and a conf.yaml the resume does not take from here.
                            yOLOTrainingOptions = new DiGi.YOLO.Classes.YOLOTrainingOptions()
                            {
                                Device = yOLOTrainingRunOptions.Device,
                                PythonPath = pythonPath,
                                ResumePath = path_Resume,
                                WorkingDirectory = workingDirectory ?? outputDirectory
                            };

                            if (!autoResume && resumeTraining)
                            {
                                Report("ResumeTraining is set: StartWeightsPath, Epochs, Patience, ImageSize, Batch and Seed are ignored - the checkpoint restores them.");
                            }
                        }
                        else
                        {
                            yOLOTrainingOptions = DiGi.YOLO.Create.YOLOTrainingOptions(pythonPath, startWeightsPath, path_Configuration, workingDirectory);
                            if (yOLOTrainingOptions is not null)
                            {
                                yOLOTrainingOptions.Batch = yOLOTrainingRunOptions.Batch;
                                yOLOTrainingOptions.Device = yOLOTrainingRunOptions.Device;
                                yOLOTrainingOptions.Epochs = yOLOTrainingRunOptions.Epochs;
                                yOLOTrainingOptions.ImageSize = yOLOTrainingRunOptions.ImageSize;
                                yOLOTrainingOptions.Name = runName;
                                yOLOTrainingOptions.Patience = yOLOTrainingRunOptions.Patience;
                                yOLOTrainingOptions.Project = projectDirectory;
                                yOLOTrainingOptions.Seed = yOLOTrainingRunOptions.Seed;
                            }
                        }

                        if (yOLOTrainingOptions is null)
                        {
                            Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Train), "The training could not be set up.");
                            return Result();
                        }

                        // Null leaves DiGi.YOLO's default (15 minutes); assigning it would disable the limit.
                        if (yOLOTrainingRunOptions.InactivityTimeout is not null)
                        {
                            yOLOTrainingOptions.InactivityTimeout = yOLOTrainingRunOptions.InactivityTimeout;
                        }

                        if (path_Resume is null)
                        {
                            Report(string.Format(CultureInfo.InvariantCulture, "Training run {0} from {1}", runName, startWeightsPath));
                        }

                        // Train is synchronous and can take hours; off the caller's thread, so a Ctrl+C handler is never starved.
                        yOLOTrainingResult = await Task.Run(() => DiGi.YOLO.Modify.Train(yOLOTrainingOptions, cancellationToken), CancellationToken.None);

                        if (Stopped())
                        {
                            return Result();
                        }

                        if (yOLOTrainingResult is not null && yOLOTrainingResult.Succeeded && !string.IsNullOrWhiteSpace(yOLOTrainingResult.WeightsPath) && !string.IsNullOrWhiteSpace(yOLOTrainingResult.SHA256))
                        {
                            // The identity of the run's own start is not rewritten by an automatic resume: it started where
                            // the operator said, and the checkpoints it passed through are named in AutoResumes.
                            if (!autoResume)
                            {
                                startWeightsSHA256 = yOLOTrainingResult.StartModelSHA256 ?? startWeightsSHA256;
                            }

                            if (path_Resume is not null)
                            {
                                // The success block names the epoch the run actually entered; fall back to the checkpoint's next epoch.
                                resumedFromEpoch = yOLOTrainingResult.ResumedFromEpoch ?? resumedFromEpoch;
                            }

                            break;
                        }

                        bool stalled = yOLOTrainingResult?.Stalled ?? false;
                        int exitCode = yOLOTrainingResult?.ExitCode ?? -1;

                        startWeightsSHA256 ??= yOLOTrainingResult?.StartModelSHA256;

                        // A refusal before a process started ends at exit code -1, and so do a cancellation and a stall;
                        // only a real crash leaves the interpreter's positive code. That is what makes a stall or a crash
                        // resumable and a refusal not, without reading messages.
                        YOLOCheckpointInformation? yOLOCheckpointInformation = null;
                        if (
                            !cancelled
                            && (stalled || exitCode > 0)
                            && autoResumes.Count < yOLOTrainingRunOptions.AutoResumeCount
                            && path_LastWeights is not null
                            && File.Exists(path_LastWeights))
                        {
                            yOLOCheckpointInformation = DiGi.YOLO.Query.YOLOCheckpointInformation(path_LastWeights, pythonPath, workingDirectory ?? outputDirectory, cancellationToken);
                        }

                        if (yOLOCheckpointInformation is null || yOLOCheckpointInformation.Finished)
                        {
                            Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Train), string.Format(CultureInfo.InvariantCulture, "The training did not produce weights (exit code {0}). {1}", yOLOTrainingResult?.ExitCode.ToString(CultureInfo.InvariantCulture) ?? "none", string.Join(" ", (yOLOTrainingResult?.StandardError ?? []).TakeLast(3))));
                            return Result();
                        }

                        // Back up the checkpoint before it is continued: a process killed while saving can leave it truncated.
                        int resumeNumber = autoResumes.Count + 1;
                        string backupFileName = string.Format(CultureInfo.InvariantCulture, "{0}{1}_{2:yyyyMMdd_HHmmss}.pt", Constants.FileName.AutoResumeWeightsPrefix, resumeNumber, DateTimeOffset.Now);
                        try
                        {
                            File.Copy(path_LastWeights!, Path.Combine(Path.GetDirectoryName(path_LastWeights!)!, backupFileName), false);
                        }
                        catch (Exception exception)
                        {
                            Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Train), string.Format(CultureInfo.InvariantCulture, "The checkpoint {0} could not be backed up before an automatic resume: {1}", path_LastWeights, exception.Message));
                            return Result();
                        }

                        int epoch = (yOLOCheckpointInformation.Epoch ?? 0) + 1;
                        string line = stalled
                            ? string.Format(CultureInfo.InvariantCulture, "Training stalled at epoch {0}{1} - automatic resume {2} of {3}", epoch, StalledLastOutput(yOLOTrainingResult?.StandardError), resumeNumber, yOLOTrainingRunOptions.AutoResumeCount)
                            : string.Format(CultureInfo.InvariantCulture, "Training exited with code {0} at epoch {1} - automatic resume {2} of {3}", exitCode, epoch, resumeNumber, yOLOTrainingRunOptions.AutoResumeCount);

                        // The resumed attempt replaces this one's result, so its error output is reported now or never:
                        // a crash leaves no other record of why it exited (no traceback reaches the operating system's logs).
                        if (!stalled)
                        {
                            List<string> standardError_Crash = [.. (yOLOTrainingResult?.StandardError ?? []).Where(x => !string.IsNullOrWhiteSpace(x)).TakeLast(Constants.Count.CrashErrorOutputLines)];
                            Report(string.Format(CultureInfo.InvariantCulture, "Error output of the attempt that exited with code {0} (last {1} line(s)):", exitCode, standardError_Crash.Count));
                            foreach (string value in standardError_Crash)
                            {
                                Report("  | " + value.TrimEnd());
                            }
                        }

                        Report(line);

                        autoResumes.Add(new YOLOTrainingAutoResume(stalled ? "Stalled" : string.Format(CultureInfo.InvariantCulture, "Exited with code {0}", exitCode), yOLOCheckpointInformation.Epoch, DateTimeOffset.Now, backupFileName));

                        resumedFromEpoch = epoch;
                        path_Resume = path_LastWeights;
                    }

                    // The loop leaves only on success, so the result and its weights are both present here.
                    YOLOTrainingResult yOLOTrainingResult_Success = yOLOTrainingResult!;

                    try
                    {
                        Directory.CreateDirectory(runDirectory!);
                        File.Copy(yOLOTrainingResult_Success.WeightsPath!, path_Weights!, false);
                    }
                    catch (Exception exception)
                    {
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Train), string.Format(CultureInfo.InvariantCulture, "The trained weights {0} could not be copied to {1}: {2}", yOLOTrainingResult_Success.WeightsPath, path_Weights, exception.Message));
                        return Result();
                    }

                    weightsSHA256 = DiGi.YOLO.Query.FileSHA256(path_Weights);
                    if (!string.Equals(weightsSHA256, yOLOTrainingResult_Success.SHA256, StringComparison.OrdinalIgnoreCase))
                    {
                        Fail(Query.YOLOTrainingStepName(YOLOTrainingStep.Train), string.Format(CultureInfo.InvariantCulture, "The copy {0} does not match the weights the training reported ({1} against {2}).", path_Weights, weightsSHA256, yOLOTrainingResult_Success.SHA256));
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
