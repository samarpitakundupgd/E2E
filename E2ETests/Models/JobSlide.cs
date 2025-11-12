using System.ComponentModel;
using System.Text.Json.Serialization;

namespace E2ETests.Models
{
    /// <summary>
    /// JobSlide Model
    /// </summary>
    public partial class JobSlide
    {

        /// <summary>Gets or sets the slide identifier.</summary>
        /// <value>The slide identifier.</value>
        [JsonPropertyName("slideId")]
        [DefaultValue("slide1")]
        public required string slideId { get; set; }


        /// <summary>Gets or sets the algo executable name.</summary>
        /// <value>The algo executable name.</value>
        [JsonPropertyName("exe")]
        [DefaultValue("QPOR_Breast_Biopsy_Lesion_top.exe")]
        public required string exe { get; set; }

        /// <summary>Gets or sets the version.</summary>
        /// <value>The version.</value>
        [JsonPropertyName("version")]
        [DefaultValue("QPOR_Breast_Biopsy_Lesion_top_v2_4_0_0")]
        public required string version { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Slide"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("status")]
        [DefaultValue(true)]
        public required bool Status { get; set; }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        /// <value>
        /// The status message.
        /// </value>
        [JsonPropertyName("statusMessage")]
        [DefaultValue("")]
        public required string StatusMessage { get; set; }
    }
}