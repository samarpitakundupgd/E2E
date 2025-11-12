using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace E2ETests.Models
{
    /// <summary>
    /// Case Model
    /// </summary>
    public class Case
    {
        /// <summary>
        /// Gets or sets the country cd.
        /// </summary>
        /// <value>
        /// The country cd.
        /// </value>
        [JsonPropertyName("countryCd")]
        [DefaultValue("US")]
        public string CountryCd { get; set; }

        /// <summary>
        /// Gets or sets the name of the org.
        /// </summary>
        /// <value>
        /// The name of the org.
        /// </value>
        [JsonPropertyName("orgName")]
        [DefaultValue("Olympus Corporation")]
        public string OrgName { get; set; }


        /// <summary>
        /// Gets or sets the org cd.
        /// </summary>
        /// <value>
        /// The org cd.
        /// </value>
        [JsonPropertyName("orgCd")]
        [DefaultValue("OLYMPUS")]
        public string OrgCd { get; set; }

        /// <summary>Gets or sets the account cd.</summary>
        /// <value>The account cd.</value>
        [JsonPropertyName("accountCd")]
        [DefaultValue("TESTACCT")]
        public string AccountCd { get; set; }

        /// <summary>
        /// Gets or sets the case cd.
        /// </summary>
        /// <value>
        /// The case cd.
        /// </value>
        [JsonPropertyName("caseCd")]
        [DefaultValue("TST001")]
        public string CaseCd { get; set; }


        /// <summary>
        /// Gets or sets the batch cd.
        /// </summary>
        /// <value>
        /// The batch cd.
        /// </value>
        [JsonPropertyName("batchCd")]
        [DefaultValue("SB_TST")]
        public string BatchCd { get; set; }


        /// <summary>
        /// Gets or sets the batch description.
        /// </summary>
        /// <value>The batch description.</value>
        [JsonPropertyName("batchDescription")]
        [DefaultValue("TESTBATCHDESCRIPTION")]
        public string BatchDescription { get; set; }


        /// <summary>
        /// Gets or sets the submitter identifier.
        /// </summary>
        /// <value>
        /// The submitter identifier.
        /// </value>
        [JsonPropertyName("submitterId")]
        [DefaultValue("UsersID")]
        public string SubmitterId { get; set; }


        /// <summary>
        /// Gets or sets the submission dt.
        /// </summary>
        /// <value>
        /// The submission dt.
        /// </value>
        [JsonPropertyName("submissionDt")]
        [DefaultValue("2021-04-01T15:56:38")]
        public DateTime SubmissionDt { get; set; }


        /// <summary>
        /// Gets or sets the case link identifier.
        /// </summary>
        /// <value>
        /// The case link identifier.
        /// </value>
        [JsonPropertyName("caseLinkId")]
        [DefaultValue("c4ed45f4-a9ea-11eb-b8d1-000d3a9a2fea")]
        public string CaseLinkId { get; set; }


        /// <summary>
        /// Is Deleted
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("deleted")]
        [DefaultValue("false")]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the ts.
        /// </summary>
        /// <value>
        /// The ts.
        /// </value>
        [JsonPropertyName("ts")]
        [DefaultValue("2021-04-30T19:32:15")]
        public DateTime TimeStamp { get; set; }
    }
}