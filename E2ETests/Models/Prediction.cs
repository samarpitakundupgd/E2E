
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace E2ETests.Models
{
    /// <summary>
    /// Prediction model
    /// </summary>
    public class Prediction
    {
        /// <summary>
        /// Gets or sets the prediction identifier.
        /// </summary>
        /// <value>
        /// The prediction identifier.
        /// </value>
        [JsonPropertyName("predictionId")]
        [DefaultValue("TST001_1_440423")]
        public string PredictionId { get; set; }

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
        /// Gets or sets the org cd.
        /// </summary>
        /// <value>
        /// The org cd.
        /// </value>
        [JsonPropertyName("orgCd")]
        [DefaultValue("JATEST")]
        public string OrgCd { get; set; }

        /// <summary>
        /// Gets or sets the account cd.
        /// </summary>
        /// <value>
        /// The account cd.
        /// </value>
        [JsonPropertyName("accountCd")]
        [DefaultValue("TESTACCT")]
        public string AccountCd { get; set; }

        /// <summary>
        /// Gets or sets the slide identifier.
        /// </summary>
        /// <value>
        /// The slide identifier.
        /// </value>
        [JsonPropertyName("slideId")]
        [DefaultValue("440423")]
        public string SlideId { get; set; }

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
        [DefaultValue("TST001_1")]
        public string JobCd { get; set; }

        /// <summary>
        /// Disease setting for the slide i.e. Adjuvant, Neoadjuvant, Metastatic.
        /// Used for algorithm input selection.
        /// </summary>
        [JsonPropertyName("diseaseSetting")]
        [DefaultValue("Unknown")]
        public string DiseaseSetting { get; set; }

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
        /// Gets or sets the observations json.
        /// </summary>
        /// <value>
        /// The observations json.
        /// </value>
        [JsonPropertyName("prediction")]
        [DefaultValue("{JSON Observations}")]
        public JsonDocument ObservationsJSON { get; set; }

        /// <summary>
        /// Gets or sets the case results json.
        /// </summary>
        /// <value>
        /// The case results json.
        /// </value>
        [JsonPropertyName("caseResults")]
        [DefaultValue("{Case Results}")]
        public JsonDocument CaseResultsJSON { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        [JsonPropertyName("ts")]
        public DateTime TimeStamp { get; set; }
    }
}
