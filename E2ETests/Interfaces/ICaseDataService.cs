using E2ETests.Models;

namespace E2ETests.Interfaces
{
    /// <summary>
    /// Interface for accessing case data.
    /// </summary>
    public interface ICaseDataService
    {
        /// <summary>Gets the case.</summary>
        /// <param name="orgCd">The organization code.</param>
        /// <param name="accountCd">The account code.</param>
        /// <param name="caseCd">The case code.</param>
        /// <returns>Case</returns>
        Task<Case> GetCaseAsync(string orgCd, string accountCd, string caseCd);

        /// <summary>Gets the Job.</summary>
        /// <param name="countryCd">The country code.</param>
        /// <param name="orgCd">The organization code.</param>
        /// <param name="accountCd">The account code.</param>
        /// <param name="batchCd">The account cd.</param>
        /// <param name="caseCd">The case code.</param>
        /// <param name="jobCd">The job code.</param>
        /// <returns>Job</returns>
        Task<Job> GetJobByJobCdAsync(string countryCd, string orgCd, string accountCd, string batchCd, string caseCd, string jobCd);

        /// <summary>Gets the Job.</summary>
        /// <param name="countryCd">The country code.</param>
        /// <param name="orgCd">The organization code.</param>
        /// <param name="accountCd">The account code.</param>
        /// <param name="batchCd">The account cd.</param>
        /// <param name="caseCd">The case code.</param>
        /// <returns>Job</returns>
        Task<List<Job>> GetJobsByCaseCdAsync(string countryCd, string orgCd, string accountCd, string batchCd, string caseCd);


        /// <summary>Gets the slide.</summary> 
        /// <param name="orgCd">The org cd.</param>
        /// <param name="accountCd">The account cd.</param>
        /// <param name="caseCd">The case cd.</param>
        /// <param name="slideId">The slide identifier.</param>
        /// <returns>Slide</returns>
        Task<Slide> GetSlideAsync(string orgCd, string accountCd, string caseCd, string slideId);

        /// <summary>
        /// Get the job status for the specified job code.
        /// </summary>
        /// <param name="countryCd">The country code.</param>
        /// <param name="orgCd">The organization code.</param>
        /// <param name="accountCd">The account code.</param>
        /// <param name="batchCd">The batch code.</param>
        /// <param name="caseCd">The case code.</param>
        /// <param name="jobCd">The job code.</param>
        /// <returns>Int representing job status.</returns>
        /// 1 In progress
        /// 2 Complete
        /// -1 Error
        Task<HttpResponseMessage> GetJobStatusAsync(string countryCd, string orgCd, string accountCd, string batchCd, string caseCd, string jobCd);
    }
}
