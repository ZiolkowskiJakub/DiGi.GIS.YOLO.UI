using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Enums;
using DiGi.GIS.YOLO.UI.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// Provides the settings of the <c>--train</c> console mode: the training dataset it builds and checks, the weights it starts from, the hyper-parameters of the run, and which steps run.
    /// <para>The dataset half is a nested <see cref="YOLOTrainingDatasetOptions"/> rather than a copy of its members, so one dataset file keeps meaning the same thing to <c>--dataset</c> and <c>--train</c>. The interpreter and working directory named here take precedence over the nested ones.</para>
    /// <para>The defaults of the hyper-parameters are the ones in the README of <c>DiGi.YOLO</c>. <see cref="StartWeightsPath"/>, <see cref="RunName"/> and <see cref="ProjectDirectory"/> have none, so a run never starts from weights nobody named or writes into a folder nobody chose.</para>
    /// </summary>
    public class YOLOTrainingRunOptions : SerializableOptions, IGISYOLOUISerializableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingRunOptions"/> class with default values.
        /// </summary>
        public YOLOTrainingRunOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingRunOptions"/> class by copying an existing options instance.
        /// </summary>
        /// <param name="yOLOTrainingRunOptions">The source options instance to copy from.</param>
        public YOLOTrainingRunOptions(YOLOTrainingRunOptions? yOLOTrainingRunOptions)
            : base(yOLOTrainingRunOptions)
        {
            if (yOLOTrainingRunOptions is not null)
            {
                Batch = yOLOTrainingRunOptions.Batch;
                DatasetOptions = Core.Query.Clone(yOLOTrainingRunOptions.DatasetOptions);
                Device = yOLOTrainingRunOptions.Device;
                Epochs = yOLOTrainingRunOptions.Epochs;
                ImageSize = yOLOTrainingRunOptions.ImageSize;
                Patience = yOLOTrainingRunOptions.Patience;
                ProjectDirectory = yOLOTrainingRunOptions.ProjectDirectory;
                PythonPath = yOLOTrainingRunOptions.PythonPath;
                RunName = yOLOTrainingRunOptions.RunName;
                Seed = yOLOTrainingRunOptions.Seed;
                StartWeightsPath = yOLOTrainingRunOptions.StartWeightsPath;
                Steps = yOLOTrainingRunOptions.Steps is null ? null : [.. yOLOTrainingRunOptions.Steps];
                WorkingDirectory = yOLOTrainingRunOptions.WorkingDirectory;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingRunOptions"/> class using a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the configuration settings.</param>
        public YOLOTrainingRunOptions(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets or sets the training batch size.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Batch))]
        public int Batch { get; set; } = 16;

        /// <summary>
        /// Gets or sets the dataset the run builds, checks and evaluates on. Its <see cref="YOLOTrainingDatasetOptions.OutputDirectory"/> is also the dataset the training reads, so it is required even when the <see cref="YOLOTrainingStep.Dataset"/> step is skipped.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(DatasetOptions))]
        public YOLOTrainingDatasetOptions? DatasetOptions { get; set; } = null;

        /// <summary>
        /// Gets or sets the device the training and validation run on, such as <c>0</c> or <c>cpu</c>. Null leaves the choice to ultralytics.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Device))]
        public string? Device { get; set; } = null;

        /// <summary>
        /// Gets or sets the upper bound of training epochs.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Epochs))]
        public int Epochs { get; set; } = 150;

        /// <summary>
        /// Gets or sets the image size, in pixels, of the training and validation.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ImageSize))]
        public int ImageSize { get; set; } = 640;

        /// <summary>
        /// Gets or sets the number of epochs without improvement after which the training stops early.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Patience))]
        public int Patience { get; set; } = 50;

        /// <summary>
        /// Gets or sets the absolute directory the run folder is created in. It may not lie inside a <c>YOLO\models</c> folder, where the frozen weights live.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(ProjectDirectory))]
        public string? ProjectDirectory { get; set; } = null;

        /// <summary>
        /// Gets or sets the CPython interpreter of the training and validation. Null uses <see cref="YOLOTrainingDatasetOptions.PythonPath"/> of <see cref="DatasetOptions"/>.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(PythonPath))]
        public string? PythonPath { get; set; } = null;

        /// <summary>
        /// Gets or sets the name of the run: the name of its folder under <see cref="ProjectDirectory"/> and of the weights file copied out of it, <c>&lt;RunName&gt;.pt</c>.
        /// <para>A name that already has a folder or a weights file is refused before anything starts, so a run never overwrites an earlier one, and <c>model</c> is refused so the production weights are never a target.</para>
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(RunName))]
        public string? RunName { get; set; } = null;

        /// <summary>
        /// Gets or sets the seed of the training.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Seed))]
        public int Seed { get; set; } = 0;

        /// <summary>
        /// Gets or sets the weights the training starts from: a <c>.pt</c> checkpoint, either an earlier detector or the base <c>yolo26x.pt</c>.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(StartWeightsPath))]
        public string? StartWeightsPath { get; set; } = null;

        /// <summary>
        /// Gets or sets the steps to run. Null runs all of them. They run in the order of <see cref="YOLOTrainingStep"/>, and the run stops at the first one that fails.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(Steps))]
        public List<YOLOTrainingStep>? Steps { get; set; } = null;

        /// <summary>
        /// Gets or sets the directory the training process runs in. Null uses <see cref="YOLOTrainingDatasetOptions.WorkingDirectory"/> of <see cref="DatasetOptions"/>, and then the dataset folder.
        /// </summary>
        [JsonInclude, JsonPropertyName(nameof(WorkingDirectory))]
        public string? WorkingDirectory { get; set; } = null;
    }
}
