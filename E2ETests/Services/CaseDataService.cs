using System.Text.Json;
using E2ETests.Models;
using E2ETests.Interfaces;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace E2ETests.Services
{
    /// <summary>
    /// Case Data Service class for accessing the Data API.
    /// </summary>
    /// <seealso cref="ICaseDataService" />
    public class CaseDataService : ICaseDataService
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>s
        /// Initializes a new instance of the <see cref="CaseDataService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public CaseDataService(IHttpClientFactory httpClientFactory, IHttpHelperService httpHelper, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("CaseDataServiceHttpClient");
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", httpHelper.GetAuth0BearerToken(configuration["auth0-4dpath-api-audience"]));
        }

        /// <summary>Gets the case.</summary>
        /// <param name="orgCd">The organization code.</param>
        /// <param name="accountCd">The account code.</param>
        /// <param name="caseCd">The case code.</param>
        /// <returns>Case</returns>
        public async Task<Case> GetCaseAsync(string orgCd, string accountCd, string caseCd)
        {
            try
            {
                Case? response = await _httpClient.GetFromJsonAsync<Case>($"Case/{orgCd}/{accountCd}/{caseCd}",
                _jsonSerializerOptions);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>Gets the Job.</summary>
        /// <param name="countryCd">The country code.</param>
        /// <param name="orgCd">The organization code.</param>
        /// <param name="accountCd">The account code.</param>
        /// <param name="batchCd">The batch code.</param>
        /// <param name="caseCd">The case code.</param>
        /// <param name="jobCd"></param>
        /// <returns>Job</returns>
        public async Task<Job> GetJobByJobCdAsync(string countryCd, string orgCd, string accountCd, string batchCd, string caseCd, string jobCd)
        {
            try
            {
                Job? response = await _httpClient.GetFromJsonAsync<Job>($"Job/{countryCd}/{orgCd}/{accountCd}/{batchCd}/{caseCd}/{jobCd}",
                _jsonSerializerOptions);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>Gets the list of Jobs for the speicfied case code.</summary>
        /// <param name="countryCd">The country code.</param>
        /// <param name="orgCd">The organization code.</param>
        /// <param name="accountCd">The account code.</param>
        /// <param name="batchCd">The batch code.</param>
        /// <param name="caseCd">The case code.</param>
        /// <returns>List of Job</returns>
        public async Task<List<Job>> GetJobsByCaseCdAsync(string countryCd, string orgCd, string accountCd, string batchCd, string caseCd)
        {
            try
            {
                List<Job>? response = await _httpClient.GetFromJsonAsync<List<Job>>($"Job/{countryCd}/{orgCd}/{accountCd}/{batchCd}/{caseCd}");
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>Gets the slide.</summary>
        /// <param name="orgCd">The org cd.</param>
        /// <param name="accountCd">The account cd.</param>
        /// <param name="caseCd">The case cd.</param>
        /// <param name="slideId">The slide identifier.</param>
        /// <returns>Slide</returns>
        public async Task<Slide> GetSlideAsync(string orgCd, string accountCd, string caseCd, string slideId)
        {
            try
            {
                Slide? response = await _httpClient.GetFromJsonAsync<Slide>($"v2/Slide/{orgCd}/{accountCd}/{caseCd}/{slideId}",
                _jsonSerializerOptions);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

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
        public async Task<HttpResponseMessage> GetJobStatusAsync(string countryCd, string orgCd, string accountCd, string batchCd, string caseCd, string jobCd)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"JobStatus/{countryCd}/{orgCd}/{accountCd}/{batchCd}/{caseCd}/{jobCd}");
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
