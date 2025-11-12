using System.ComponentModel;
using System.Text.Json.Serialization;

namespace E2ETests.Models
{
    /// <summary>
    /// Side model
    /// </summary>
    public class Slide
    {
        /// <summary>
        /// Unique identifier for the slide, used in the application.
        /// </summary>
        [JsonPropertyName("slideId")]
        [DefaultValue("HE27")]
        public string SlideId { get; set; }

        /// <summary>
        /// Case code associated with the slide
        /// </summary>
        [JsonPropertyName("caseCd")]
        [DefaultValue("TST001")]
        public string CaseCd { get; set; }

        /// <summary>
        /// Organization code associated with the slide.
        /// </summary>
        [JsonPropertyName("orgCd")]
        [DefaultValue("OLYMPUS")]
        public string OrgCd { get; set; }

        /// <summary>
        /// Account code associated with the slide.
        /// </summary>
        [JsonPropertyName("accountCd")]
        [DefaultValue("TESTACCT")]
        public string AccountCd { get; set; }

        /// <summary>
        /// Identifier for the user who submitted the slide.
        /// </summary>
        [JsonPropertyName("submitterId")]
        [DefaultValue("UsersId")]
        public string SubmitterId { get; set; }

        /// <summary>
        /// Date and time when the slide was submitted.
        /// </summary>
        [JsonPropertyName("submissionDt")]
        [DefaultValue("2021-04-01T15:56:52")]
        public DateTime SubmissionDt { get; set; }

        /// <summary>
        /// Name of the image file associated with the slide.
        /// </summary>
        [JsonPropertyName("imgName")]
        [DefaultValue("HE27.svs")]
        public string ImgName { get; set; }

        /// <summary>
        /// Format of the image file.
        /// </summary>
        [JsonPropertyName("imgFormat")]
        [DefaultValue("svs")]
        public string ImgFormat { get; set; }

        /// <summary>
        /// Size of the image file in bytes.
        /// </summary>
        [JsonPropertyName("imgSize")]
        [DefaultValue("382349133")]
        public long ImgSize { get; set; }

        /// <summary>
        /// Location of the image file in the storage system.
        /// </summary>
        [JsonPropertyName("imgLoc")]
        [DefaultValue("Cases/US/4DPATH/SB_TST/TST001/Images/HE27.svs")]
        public string ImgLoc { get; set; }

        /// <summary>
        /// Date and time when the slide was last modified.
        /// </summary>
        [JsonPropertyName("lastModifiedDate")]
        [DefaultValue("2021-04-01T15:57:00")]
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Storage account name where the slide image is stored.
        /// </summary>
        [JsonPropertyName("storageAccount")]
        [DefaultValue("devstclienteastus")]
        public string StorageAccount { get; set; }

        /// <summary>
        /// File share name where the slide image is stored.
        /// </summary>
        [JsonPropertyName("fileShare")]
        [DefaultValue("4dpathprod")]
        public string FileShare { get; set; }

        /// <summary>
        /// Origin organ of the sample, used for algorithm selection.
        /// </summary>
        [JsonPropertyName("originOrgan")]
        [DefaultValue("Breast")]
        public string OriginOrgan { get; set; }

        /// <summary>
        /// Type of sample represented by the slide.
        /// This could be a biopsy, resection, etc.
        /// </summary>
        [JsonPropertyName("sampleType")]
        [DefaultValue("Biopsy")]
        public string SampleType { get; set; }

        /// <summary>
        /// Sample part of the slide, such as lesion or lymph node.
        /// </summary>
        [JsonPropertyName("samplePart")]
        [DefaultValue("Lesion")]
        public string SamplePart { get; set; }

        /// <summary>
        /// Organ from which the sample was derived.
        /// </summary>
        [JsonPropertyName("sampleOrgan")]
        [DefaultValue("")]
        public string SampleOrgan { get; set; }

        /// <summary>
        /// Subpart code for the slide, used for categorization.
        /// </summary>
        [JsonPropertyName("subpartCd")]
        [DefaultValue("A")]
        public string SubpartCd { get; set; }

        /// <summary>
        /// Block code for the slide, used for categorization.
        /// </summary>
        [JsonPropertyName("blockCd")]
        [DefaultValue("A")]
        public string BlockCd { get; set; }

        /// <summary>
        /// The PMA file path for the slide.
        /// </summary>
        [JsonPropertyName("pmaFilePath")]
        [DefaultValue("C:\\Temp\\image1.svs")]
        public string PMAFilePath { get; set; }

        /// <summary>
        /// MD5 Hash of the image file, used for integrity checks.
        /// </summary>
        [JsonPropertyName("imageHash")]
        public string ImageHash { get; set; }

        /// <summary>
        /// Magnification level of the image, used for determining algorithm inputs.
        /// </summary>
        [JsonPropertyName("imageMagnification")]
        public string ImageMagnification { get; set; }

        /// <summary>
        /// Pixel dimensions of the image, used for rendering and processing.
        /// </summary>
        [JsonPropertyName("imagePixelWidth")]
        public int ImagePixelWidth { get; set; }

        /// <summary>
        /// Pixel height of the image, used for rendering and processing.
        /// </summary>
        [JsonPropertyName("imagePixelHeight")]
        public int ImagePixelHeight { get; set; }

        /// <summary>
        /// Maximum zoom level for the image.
        /// </summary>
        [JsonPropertyName("imageMaxZoomLevel")]
        public int ImageMaxZoomLevel { get; set; }

        /// <summary>
        /// Disease setting for the slide i.e. Adjuvant, Neoadjuvant, Metastatic.
        /// Used for algorithm input selection.
        /// </summary>
        [JsonPropertyName("diseaseSetting")]
        [DefaultValue("Unknown")]
        public string DiseaseSetting { get; set; }

        /// <summary>
        /// Status of the slide, indicating whether it is has been processed successfully.
        /// </summary>
        [Obsolete]
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        /// <summary>
        /// Status message for the slide, providing additional information about the processing status.
        /// </summary>
        [Obsolete]
        [JsonPropertyName("statusMessage")]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Indicates whether the slide has been deleted.
        /// This is used to mark slides as deleted without removing them from the database.
        /// </summary>
        [JsonPropertyName("deleted")]
        [DefaultValue("false")]
        public bool Deleted { get; set; }

        /// <summary>
        /// Timestamp of the slide, used for tracking when the slide was created or last modified.
        /// </summary>
        [JsonPropertyName("timeStamp")]
        [DefaultValue("2021-04-30T19:32:15")]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Stain used for the slide, such as H&E, IHC, etc.
        /// </summary>
        [JsonPropertyName("slideStain")]
        public string SlideStain { get; set; }

        /// <summary>
        /// Unique identifier for the slide link, used to associate slides.
        /// For instance linking an H&amp;E slide with its corresponding IHC slide.
        /// </summary>
        [JsonPropertyName("slideLinkID")]
        public string SlideLinkID { get; set; }
    }
}