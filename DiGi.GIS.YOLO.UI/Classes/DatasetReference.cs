using DiGi.Core.Classes;
using DiGi.GIS.YOLO.UI.Enums;
using DiGi.GIS.YOLO.UI.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.YOLO.UI.Classes
{
    /// <summary>
    /// One building of a YOLO training dataset: the county part it was read from, the split it went to, its label year, and whether the <c>train8</c> detector may have seen it.
    /// <para>One row of the <c>dataset_references.tsv</c> manifest. The label check and the detector evaluation read the dataset through it, so the split and the Legacy decision are taken once, when the dataset is built, and never re-derived.</para>
    /// </summary>
    public class DatasetReference : SerializableObject, IGISYOLOUISerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Category))]
        private readonly DiGi.YOLO.Enums.Category category;

        [JsonInclude, JsonPropertyName(nameof(CountyId))]
        private readonly int countyId;

        [JsonInclude, JsonPropertyName(nameof(Label))]
        private readonly short label;

        [JsonInclude, JsonPropertyName(nameof(LegacySource))]
        private readonly LegacySource legacySource;

        [JsonInclude, JsonPropertyName(nameof(Reference))]
        private readonly string? reference;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetReference"/> class.
        /// </summary>
        /// <param name="reference">The building reference.</param>
        /// <param name="countyId">The identifier of the county part the building was read from.</param>
        /// <param name="category">The split the building went to.</param>
        /// <param name="label">The label year - the building&apos;s exact user year built.</param>
        /// <param name="legacySource">Which record says the <c>train8</c> detector may have seen the building.</param>
        public DatasetReference(string? reference, int countyId, DiGi.YOLO.Enums.Category category, short label, LegacySource legacySource)
        {
            this.reference = reference;
            this.countyId = countyId;
            this.category = category;
            this.label = label;
            this.legacySource = legacySource;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetReference"/> class by copying an existing one.
        /// </summary>
        /// <param name="datasetReference">The <see cref="DatasetReference"/> to copy from.</param>
        public DatasetReference(DatasetReference? datasetReference)
            : base(datasetReference)
        {
            if (datasetReference is not null)
            {
                category = datasetReference.category;
                countyId = datasetReference.countyId;
                label = datasetReference.label;
                legacySource = datasetReference.legacySource;
                reference = datasetReference.reference;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetReference"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data.</param>
        public DatasetReference(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the split the building went to. A held-out building is always <see cref="DiGi.YOLO.Enums.Category.Test"/>.
        /// </summary>
        [JsonIgnore]
        public DiGi.YOLO.Enums.Category Category
        {
            get
            {
                return category;
            }
        }

        /// <summary>
        /// Gets the identifier of the county part the building was read from.
        /// </summary>
        [JsonIgnore]
        public int CountyId
        {
            get
            {
                return countyId;
            }
        }

        /// <summary>
        /// Gets the label year - the building&apos;s exact user year built. An image of a year before it is a negative, one of this year or later carries the building&apos;s box.
        /// </summary>
        [JsonIgnore]
        public short Label
        {
            get
            {
                return label;
            }
        }

        /// <summary>
        /// Gets whether the <c>train8</c> detector may have seen the building - true for anything but <see cref="LegacySource.None"/>, including <see cref="LegacySource.Unknown"/>.
        /// </summary>
        [JsonIgnore]
        public bool Legacy
        {
            get
            {
                return legacySource != LegacySource.None;
            }
        }

        /// <summary>
        /// Gets which record says the <c>train8</c> detector may have seen the building.
        /// <para>For a Train or Validate building only the legacy reference list is consulted, because their Legacy decision feeds no metric.</para>
        /// </summary>
        [JsonIgnore]
        public LegacySource LegacySource
        {
            get
            {
                return legacySource;
            }
        }

        /// <summary>
        /// Gets the building reference.
        /// </summary>
        [JsonIgnore]
        public string? Reference
        {
            get
            {
                return reference;
            }
        }
    }
}
