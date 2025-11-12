using System;
using System.Collections.Generic;

namespace E2ETests.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CaseRequest
    {
        /// <summary>
        /// Gets or sets the country cd.
        /// </summary>
        /// <value>
        /// The country cd.
        /// </value>
        public required string CountryCd { get; set; }
        /// <summary>
        /// Gets or sets the name of the org.
        /// </summary>
        /// <value>
        /// The name of the org.
        /// </value>
        public required string OrgName { get; set; }
        /// <summary>
        /// Gets or sets the org cd.
        /// </summary>
        /// <value>
        /// The org cd.
        /// </value>
        public required string OrgCd { get; set; }
        /// <summary>
        /// Gets or sets the account cd.
        /// </summary>
        /// <value>
        /// The account cd.
        /// </value>
        public required string AccountCd { get; set; }
        /// <summary>
        /// Gets or sets the batch cd.
        /// </summary>
        /// <value>
        /// The batch cd.
        /// </value>
        public required string BatchCd { get; set; }
        /// <summary>
        /// Gets or sets the case cd.
        /// </summary>
        /// <value>
        /// The case cd.
        /// </value>
        public required string CaseCd { get; set; }
        /// <summary>
        /// Gets or sets the submitter identifier.
        /// </summary>
        /// <value>
        /// The submitter identifier.
        /// </value>
        public required string SubmitterId { get; set; }
        /// <summary>
        /// Gets or sets the callback URL.
        /// </summary>
        /// <value>
        /// The callback URL.
        /// </value>
        public required string CallbackURL { get; set; }
        /// <summary>
        /// Gets or sets the callback HTTP method.
        /// </summary>
        /// <value>
        /// The callback HTTP method.
        /// </value>
        public required string CallbackHttpMethod { get; set; }
        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public required List<Image> Images { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Gets or sets the slide identifier.
        /// </summary>
        /// <value>
        /// The slide identifier.
        /// </value>
        public required string SlideId { get; set; }
        /// <summary>
        /// Gets or sets the name of the img.
        /// </summary>
        /// <value>
        /// The name of the img.
        /// </value>
        public required string ImgName { get; set; }
        /// <summary>
        /// Gets or sets the origin organ.
        /// </summary>
        /// <value>
        /// The origin organ.
        /// </value>
        public required string OriginOrgan { get; set; }
        /// <summary>
        /// Gets or sets the sample organ.
        /// </summary>
        /// <value>
        /// The sample organ.
        /// </value>
        public required string SampleOrgan { get; set; }
        /// <summary>
        /// Gets or sets the type of the sample.
        /// </summary>
        /// <value>
        /// The type of the sample.
        /// </value>
        public required string SampleType { get; set; }
        /// <summary>
        /// Gets or sets the sample part.
        /// </summary>
        /// <value>
        /// The sample part.
        /// </value>
        public required string SamplePart { get; set; }
        /// <summary>
        /// Gets or sets the subpart cd.
        /// </summary>
        /// <value>
        /// The subpart cd.
        /// </value>
        public required string SubpartCd { get; set; }
        /// <summary>
        /// Gets or sets the block cd.
        /// </summary>
        /// <value>
        /// The block cd.
        /// </value>
        public required string BlockCd { get; set; }
        /// <summary>
        /// Gets or sets the disease setting.
        /// </summary>
        /// <value>
        /// The disease setting.
        /// </value>
        public required string DiseaseSetting { get; set; }
        /// <summary>
        /// Gets or sets the image hash.
        /// </summary>
        /// <value>
        /// The image hash.
        /// </value>
        public required string ImageHash { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Image"/> is archive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if archive; otherwise, <c>false</c>.
        /// </value>
        public required bool Archive { get; set; }
        /// <summary>
        /// Gets or sets the slide stain.
        /// </summary>
        /// <value>
        /// The slide stain.
        /// </value>
        public required string SlideStain { get; set; }
        /// <summary>
        /// Gets or sets the slide link identifier.
        /// </summary>
        /// <value>
        /// The slide link identifier.
        /// </value>
        public required string SlideLinkID { get; set; }
    }
}