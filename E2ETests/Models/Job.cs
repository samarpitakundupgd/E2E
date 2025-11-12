using System;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E2ETests.Models
{
    /// <summary>
    /// Job Model
    /// </summary>
    public partial class Job
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Job"/> class.
        /// </summary>
        public Job()
        {

        }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        [JsonPropertyName("jobId")]
        [DefaultValue("1")]
        public int JobId { get; set; }

        /// <summary>
        /// Gets or sets the job cd.
        /// </summary>
        /// <value>
        /// The job cd.
        /// </value>
        [JsonPropertyName("jobCd")]
        [DefaultValue("Job1")]
        public string JobCd { get; set; }

        /// <summary>
        /// Gets or sets the case cd.
        /// </summary>
        /// <value>
        /// The case cd.
        /// </value>
        [JsonPropertyName("caseCd")]
        [DefaultValue("Case1")]
        public string CaseCd { get; set; }

        /// <summary>
        /// Gets or sets the org cd.
        /// </summary>
        /// <value>
        /// The org cd.
        /// </value>
        [JsonPropertyName("orgCd")]
        [DefaultValue("SRHF")]
        public string OrgCd { get; set; }

        /// <summary>
        /// Gets or sets the account cd.
        /// </summary>
        /// <value>The account cd.</value>
        [JsonPropertyName("accountCd")]
        [DefaultValue("TESTACCT")]
        public string AccountCd { get; set; }

        /// <summary>
        /// Gets or sets the batch cd.
        /// </summary>
        /// <value>The batch cd.</value>
        [JsonPropertyName("batchCd")]
        [DefaultValue("TESTBATCH")]
        public string BatchCd { get; set; }

        /// <summary>
        /// Gets or sets the country cd.
        /// </summary>
        /// <value>The country cd.</value>
        [JsonPropertyName("countryCd")]
        [DefaultValue("TESTCOUNTRY")]
        public string CountryCd { get; set; }

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
        /// Gets or sets the run dt.
        /// </summary>
        /// <value>
        /// The run dt.
        /// </value>
        [JsonPropertyName("runDt")]
        [DefaultValue("2021-04-30T19:32:15")]
        public DateTime RunDt { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        [JsonPropertyName("startTime")]
        [DefaultValue("2022-10-04T19:32:15")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        [JsonPropertyName("endTime")]
        [DefaultValue("2022-10-04T23:32:15")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the elapsed time in seconds
        /// </summary>
        /// <value>
        /// The ellapsed time in seconds
        /// </value>
        [DefaultValue(0)]
        [JsonPropertyName("elapsedTime")]
        public int ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets the job dir.
        /// </summary>
        /// <value>
        /// The job dir.
        /// </value>
        [JsonPropertyName("jobDir")]
        [DefaultValue("Cases/USA/SRHF/Batch1/Case1/Jobs/TST001_1")]
        public string JobDir { get; set; }

        /// <summary>
        /// Gets or sets the slide ids.
        /// </summary>
        /// <value>
        /// The slide ids.
        /// </value>
        [JsonPropertyName("slideIds")]
        public JobSlide[] SlideIds { get; set; }

        /// <summary>Gets or sets the callback URL.</summary>
        /// <value>The callback URL.</value>
        [JsonPropertyName("callbackURL")]
        [DefaultValue("")]
        public string CallbackURL { get; set; }

        /// <summary>Gets or sets the HTTP method for the callback.</summary>
        /// <value>The HTTP method.</value>
        [JsonPropertyName("callbackHttpMethod")]
        [DefaultValue("")]
        public string HTTPMethod { get; set; }

        /// <summary>
        /// Gets or sets the part block results.
        /// </summary>
        /// <value>
        /// The part block results.
        /// </value>
        [JsonPropertyName("PartBlockResults")]
        [DefaultValue("")]
        public JsonDocument PartBlockResults { get; set; }

        /// <summary>
        /// Gets or sets the custom properties.
        /// </summary>
        /// <value>
        /// The custom properties. Useful for partner-specific data.
        /// </value>
        [JsonPropertyName("customProperties")]
        [DefaultValue("")]
        public JsonDocument CustomProperties { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Job"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("deleted")]
        [DefaultValue(false)]
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

        /// <summary>
        /// Gets or sets a value indicating whether [automatic create PDF].
        /// If set to true the PDF Will be generated automatically at the completion of the run
        /// If set to false, the pdf will onle be generated on demand by clicking a button in the dashboard (for example)
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic create PDF]; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("autoCreatePDF")]
        [DefaultValue(false)]
        public bool AutoCreatePDF { get; set; }

        /// <summary>
        /// Gets or sets the job status.
        /// </summary>
        /// <value>
        /// The job status.
        /// </value>
        [JsonPropertyName("jobStatus")]
        [DefaultValue(JobStatus.New)]
        public JobStatus JobStatus { get; set; }
    }

    /// <summary>
    ///   Enumeration for Job Status
    /// </summary>
    public enum JobStatus
    {
        /// <summary>The new</summary>
        New,
        /// <summary>The in progress</summary>
        InProgress,
        /// <summary>The error</summary>
        Error,
        /// <summary>The done</summary>
        Done
    }
}