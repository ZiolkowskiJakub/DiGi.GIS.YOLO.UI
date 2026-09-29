using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.YOLO.UI.Classes;
using DiGi.YOLO.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Checks the label boxes of a training dataset against the current detector before anything is trained on them: the detector of <see cref="YOLOTrainingDatasetOptions.ModelPath"/> is run over a seeded sample of <b>positive</b> Train and Validate images, and each label box is compared with the best-overlapping detection by intersection over union.
        /// <para>Reported as the mean, the median and the share at 0.5 or more, overall and per county part. An image the detector found nothing on counts as 0. A low mean means the new boxes are shifted or scaled against what the detector learned - stop and raise it on the tracking issue before training.</para>
        /// <para>The first <see cref="YOLOTrainingDatasetOptions.LabelCheckOverlayCount"/> sampled images are also written, with the label box in green and the best detection in red, to a <c>label_check</c> folder under <see cref="YOLOTrainingDatasetOptions.ReportsDirectory"/>. The sample is copied to a <c>label_check</c> folder beside the dataset, so the detector reads nothing but the sample; the dataset itself is not changed.</para>
        /// </summary>
        /// <param name="yOLOTrainingDatasetOptions">The options naming the dataset, the weights, the interpreter and the sample.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the detector.</param>
        /// <returns>The check, or null when there are no options or no absolute dataset directory.</returns>
        [SupportedOSPlatform("windows")]
        public static YOLOLabelCheckResult? CheckYOLOTrainingDatasetLabels(this YOLOTrainingDatasetOptions? yOLOTrainingDatasetOptions, CancellationToken cancellationToken = default)
        {
            string? outputDirectory = yOLOTrainingDatasetOptions?.OutputDirectory;
            if (yOLOTrainingDatasetOptions is null || string.IsNullOrWhiteSpace(outputDirectory) || !Path.IsPathRooted(outputDirectory))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: no options or no absolute dataset directory", nameof(CheckYOLOTrainingDatasetLabels));
                return null;
            }

            outputDirectory = Path.GetFullPath(outputDirectory);

            DateTimeOffset start = DateTimeOffset.Now;
            List<string> failedStepNames = [];
            List<string> messages = [];

            string directory_Overlays = Path.Combine(Path.GetFullPath(string.IsNullOrWhiteSpace(yOLOTrainingDatasetOptions.ReportsDirectory) ? "user files/reports" : yOLOTrainingDatasetOptions.ReportsDirectory!), "label_check");

            void Fail(string stepName, string message)
            {
                if (!failedStepNames.Contains(stepName))
                {
                    failedStepNames.Add(stepName);
                }

                messages.Add(message);

                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "{Method}: step {Step} did not complete - {Message}", nameof(CheckYOLOTrainingDatasetLabels), stepName, message);
            }

            YOLOLabelCheckResult Result(List<(string County, double IntersectionOverUnion, bool Detected)>? samples)
            {
                if (samples is null || samples.Count == 0)
                {
                    return new YOLOLabelCheckResult(0, 0, 0, 0, 0, null, directory_Overlays, failedStepNames, messages, start, DateTimeOffset.Now);
                }

                List<double> intersectionOverUnions = [.. samples.Select(x => x.IntersectionOverUnion).OrderBy(x => x)];
                double median = intersectionOverUnions.Count % 2 == 1
                    ? intersectionOverUnions[intersectionOverUnions.Count / 2]
                    : (intersectionOverUnions[(intersectionOverUnions.Count / 2) - 1] + intersectionOverUnions[intersectionOverUnions.Count / 2]) / 2;

                Dictionary<string, double> countyIntersectionOverUnions = [];
                foreach (IGrouping<string, (string County, double IntersectionOverUnion, bool Detected)> grouping in samples.GroupBy(x => x.County, StringComparer.Ordinal).OrderBy(x => x.Key, StringComparer.Ordinal))
                {
                    countyIntersectionOverUnions[grouping.Key] = grouping.Average(x => x.IntersectionOverUnion);
                }

                return new YOLOLabelCheckResult(samples.Count, samples.Count(x => x.Detected), intersectionOverUnions.Average(), median, samples.Count(x => x.IntersectionOverUnion >= 0.5) / (double)samples.Count, countyIntersectionOverUnions, directory_Overlays, failedStepNames, messages, start, DateTimeOffset.Now);
            }

            string path_Configuration = Path.Combine(outputDirectory, DiGi.YOLO.Constants.FileName.Conf);
            YOLOModel? yOLOModel = DiGi.YOLO.Modify.Read(path_Configuration);
            if (yOLOModel is null)
            {
                Fail(nameof(DiGi.YOLO.Modify.Read), string.Format(CultureInfo.InvariantCulture, "The dataset {0} could not be read.", path_Configuration));
                return Result(null);
            }

            Dictionary<string, int> countyIds_ByReference = new(StringComparer.Ordinal);
            List<DatasetReference>? datasetReferences = Query.DatasetReferences(Path.Combine(outputDirectory, Constants.FileName.DatasetReferences));
            if (datasetReferences is not null)
            {
                foreach (DatasetReference datasetReference in datasetReferences)
                {
                    countyIds_ByReference[datasetReference.Reference!] = datasetReference.CountyId;
                }
            }

            // Positive images only: a negative has no box to compare. Train and Validate only: Test is kept for the gate.
            List<string> paths_Positive = [];
            List<DiGi.YOLO.Enums.Category> categories = [DiGi.YOLO.Enums.Category.Train, DiGi.YOLO.Enums.Category.Validate];
            foreach (DiGi.YOLO.Enums.Category category in categories)
            {
                foreach (DiGi.YOLO.Classes.Image image in yOLOModel.GetImages(category))
                {
                    if (image?.Path is string path && yOLOModel.GetLabelFile(path) is LabelFile labelFile && labelFile.Count != 0)
                    {
                        paths_Positive.Add(path);
                    }
                }
            }

            if (paths_Positive.Count == 0)
            {
                Fail(nameof(DiGi.YOLO.Enums.Category.Train), string.Format(CultureInfo.InvariantCulture, "The dataset {0} holds no positive Train or Validate image.", outputDirectory));
                return Result(null);
            }

            // Seeded, over the ordinally sorted paths, so the same dataset and seed always check the same images.
            paths_Positive.Sort(StringComparer.Ordinal);
            Random random = new(yOLOTrainingDatasetOptions.Seed);
            for (int i = paths_Positive.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (paths_Positive[i], paths_Positive[j]) = (paths_Positive[j], paths_Positive[i]);
            }

            List<string> paths_Sample = [.. paths_Positive.Take(Math.Max(1, yOLOTrainingDatasetOptions.LabelCheckSampleSize))];

            string directory_Check = Path.Combine(outputDirectory, "label_check");
            string directory_Images = Path.Combine(directory_Check, "images");
            if (Directory.Exists(directory_Images))
            {
                Directory.Delete(directory_Images, true);
            }

            Directory.CreateDirectory(directory_Images);
            foreach (string path in paths_Sample)
            {
                File.Copy(path, Path.Combine(directory_Images, Path.GetFileName(path)), true);
            }

            string? path_Model = Query.ModelPath(yOLOTrainingDatasetOptions.ModelPath);
            if (string.IsNullOrWhiteSpace(path_Model) || !File.Exists(path_Model))
            {
                Fail(nameof(Query.ModelPath), string.Format(CultureInfo.InvariantCulture, "The detector weights were not found - {0}.", yOLOTrainingDatasetOptions.ModelPath ?? "no weights were named"));
                return Result(null);
            }

            string path_Results = Path.Combine(directory_Check, Constants.FileName.PredictionResults);

            YOLOPredictionOptions? yOLOPredictionOptions = DiGi.YOLO.Create.YOLOPredictionOptions(yOLOTrainingDatasetOptions.PythonPath, path_Model, directory_Images, path_Results, yOLOTrainingDatasetOptions.WorkingDirectory, yOLOTrainingDatasetOptions.Confidence);
            if (yOLOPredictionOptions is null)
            {
                Fail(nameof(DiGi.YOLO.Create.YOLOPredictionOptions), "The detector could not be set up - check PythonPath.");
                return Result(null);
            }

            YOLOPredictionResult? yOLOPredictionResult = DiGi.YOLO.Modify.Predict(yOLOPredictionOptions, cancellationToken);
            BoundingBoxResultFile? boundingBoxResultFile = DiGi.YOLO.Create.BoundingBoxResultFile(yOLOPredictionResult);
            if (boundingBoxResultFile is null)
            {
                if (yOLOPredictionResult?.StandardError is List<string> standardError && standardError.Count != 0)
                {
                    messages.Add(string.Join("; ", standardError));
                }

                Fail(nameof(DiGi.YOLO.Modify.Predict), "The detector did not run.");
                return Result(null);
            }

            Dictionary<string, List<BoundingBox2D>> detections_ByName = new(StringComparer.OrdinalIgnoreCase);
            foreach (BoundingBoxResult boundingBoxResult in boundingBoxResultFile)
            {
                if (string.IsNullOrWhiteSpace(boundingBoxResult?.Name))
                {
                    continue;
                }

                string name = Path.GetFileNameWithoutExtension(boundingBoxResult!.Name!);
                if (!detections_ByName.TryGetValue(name, out List<BoundingBox2D>? boundingBox2Ds))
                {
                    boundingBox2Ds = [];
                    detections_ByName[name] = boundingBox2Ds;
                }

                // The detector reports the top-left corner and the size in pixels of the source image.
                boundingBox2Ds.Add(new BoundingBox2D(boundingBoxResult.X, boundingBoxResult.Y, boundingBoxResult.Width, boundingBoxResult.Height));
            }

            Directory.CreateDirectory(directory_Overlays);

            List<(string County, double IntersectionOverUnion, bool Detected)> samples = [];
            for (int i = 0; i < paths_Sample.Count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string path = paths_Sample[i];
                string name = Path.GetFileNameWithoutExtension(path);

                LabelFile? labelFile = yOLOModel.GetLabelFile(path);
                if (labelFile is null || labelFile.Count == 0)
                {
                    continue;
                }

                int width;
                int height;
                using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
                {
                    width = image.Width;
                    height = image.Height;
                }

                // The label box is normalised centre and size; back to pixels for the comparison.
                DiGi.YOLO.Classes.BoundingBox boundingBox = labelFile.GetBoundingBox(0);
                BoundingBox2D boundingBox2D_Label = new((boundingBox.X - (boundingBox.Width / 2)) * width, (boundingBox.Y - (boundingBox.Height / 2)) * height, boundingBox.Width * width, boundingBox.Height * height);

                detections_ByName.TryGetValue(name, out List<BoundingBox2D>? boundingBox2Ds_Detection);

                double intersectionOverUnion = 0;
                BoundingBox2D? boundingBox2D_Best = null;
                if (boundingBox2Ds_Detection is not null)
                {
                    foreach (BoundingBox2D boundingBox2D_Detection in boundingBox2Ds_Detection)
                    {
                        double intersectionOverUnion_Detection = Query.IntersectionOverUnion(boundingBox2D_Label, boundingBox2D_Detection);
                        if (!double.IsNaN(intersectionOverUnion_Detection) && (boundingBox2D_Best is null || intersectionOverUnion_Detection > intersectionOverUnion))
                        {
                            intersectionOverUnion = intersectionOverUnion_Detection;
                            boundingBox2D_Best = boundingBox2D_Detection;
                        }
                    }
                }

                string county = Query.TryParseImageFileName(path, out string? reference, out _) && countyIds_ByReference.TryGetValue(reference!, out int countyId) ? countyId.ToString(CultureInfo.InvariantCulture) : string.Empty;

                samples.Add((county, intersectionOverUnion, boundingBox2Ds_Detection is not null && boundingBox2Ds_Detection.Count != 0));

                if (i >= yOLOTrainingDatasetOptions.LabelCheckOverlayCount)
                {
                    continue;
                }

                try
                {
                    using System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                    using System.Drawing.Bitmap bitmap = new(image);
                    using System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
                    using System.Drawing.Pen pen_Label = new(System.Drawing.Color.Lime, 2);
                    using System.Drawing.Pen pen_Detection = new(System.Drawing.Color.Red, 2);

                    graphics.DrawRectangle(pen_Label, (float)boundingBox2D_Label.Min.X, (float)boundingBox2D_Label.Min.Y, (float)boundingBox2D_Label.Width, (float)boundingBox2D_Label.Height);
                    if (boundingBox2D_Best is not null)
                    {
                        graphics.DrawRectangle(pen_Detection, (float)boundingBox2D_Best.Min.X, (float)boundingBox2D_Best.Min.Y, (float)boundingBox2D_Best.Width, (float)boundingBox2D_Best.Height);
                    }

                    bitmap.Save(Path.Combine(directory_Overlays, string.Format(CultureInfo.InvariantCulture, "{0}_iou{1:0.00}.jpeg", name, intersectionOverUnion)), System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception exception)
                {
                    // An overlay is evidence for a person, not part of the measurement.
                    Serilog.Modify.Log(exception, "{Method}: the overlay of {Image} could not be written", nameof(CheckYOLOTrainingDatasetLabels), path);
                }
            }

            return Result(samples);
        }
    }
}
