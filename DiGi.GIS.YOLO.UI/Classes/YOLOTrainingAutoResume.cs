using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Interfaces;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// One automatic resume a <c>--train</c> run made after its training stalled or crashed.
    /// <para>Automatic resumes are distinct from the operator-requested resume of <see cref="YOLOTrainingRunOptions.ResumeTraining"/>: that one is reported by <see cref="YOLOTrainingRunResult.Resumed"/> and <see cref="YOLOTrainingRunResult.ResumedFromEpoch"/>, while each automatic one is recorded here, so the run log and the result can say what happened without an operator watching.</para>
    /// </summary>
    public class YOLOTrainingAutoResume : SerializableObject, IGISYOLOUISerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(BackupFileName))]
        private readonly string? backupFileName;

        [JsonInclude, JsonPropertyName(nameof(Epoch))]
        private readonly int? epoch;

        [JsonInclude, JsonPropertyName(nameof(Reason))]
        private readonly string? reason;

        [JsonInclude, JsonPropertyName(nameof(Time))]
        private readonly DateTimeOffset time;

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingAutoResume"/> class.
        /// </summary>
        /// <param name="reason">Why the previous attempt ended, such as <c>Stalled</c> or <c>Exited with code 3</c>.</param>
        /// <param name="epoch">The epoch the checkpoint held when it was resumed, or null when it was not read.</param>
        /// <param name="time">When the resume was started.</param>
        /// <param name="backupFileName">The name of the copy taken of the checkpoint before the resume, or null when none was taken.</param>
        public YOLOTrainingAutoResume(string? reason, int? epoch, DateTimeOffset time, string? backupFileName)
        {
            this.reason = reason;
            this.epoch = epoch;
            this.time = time;
            this.backupFileName = backupFileName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingAutoResume"/> class by copying an existing one.
        /// </summary>
        /// <param name="yOLOTrainingAutoResume">The <see cref="YOLOTrainingAutoResume"/> to copy from.</param>
        public YOLOTrainingAutoResume(YOLOTrainingAutoResume? yOLOTrainingAutoResume)
            : base(yOLOTrainingAutoResume)
        {
            if (yOLOTrainingAutoResume is not null)
            {
                backupFileName = yOLOTrainingAutoResume.backupFileName;
                epoch = yOLOTrainingAutoResume.epoch;
                reason = yOLOTrainingAutoResume.reason;
                time = yOLOTrainingAutoResume.time;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YOLOTrainingAutoResume"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public YOLOTrainingAutoResume(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the name of the copy taken of <c>weights\last.pt</c> before the resume, or null when none was taken.
        /// <para>A process killed while saving can leave a truncated <c>last.pt</c>, so the copy keeps the previous good checkpoint recoverable by hand.</para>
        /// </summary>
        [JsonIgnore]
        public string? BackupFileName
        {
            get
            {
                return backupFileName;
            }
        }

        /// <summary>
        /// Gets the epoch the checkpoint held when it was resumed, or null when it was not read.
        /// </summary>
        [JsonIgnore]
        public int? Epoch
        {
            get
            {
                return epoch;
            }
        }

        /// <summary>
        /// Gets why the previous attempt ended, such as <c>Stalled</c> or <c>Exited with code 3</c>.
        /// </summary>
        [JsonIgnore]
        public string? Reason
        {
            get
            {
                return reason;
            }
        }

        /// <summary>
        /// Gets when the resume was started.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset Time
        {
            get
            {
                return time;
            }
        }
    }
}
