using E2ETests.Fixtures;
using E2ETests.Helpers;
using E2ETests.Interfaces;
using E2ETests.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace E2ETests.Tests
{
    /// <summary>
    /// XUnit tests for 4DAPI
    /// </summary>
    public class FourDAPITests : IClassFixture<E2ETestFixture>
    {
        private readonly ICaseRequestService _caseRequestService;
        private readonly ICaseDataService _caseDataService;

        // Update the constructor to assign the fields (remove the local variable shadowing)
        public FourDAPITests(E2ETestFixture testFixture)
        {
            _caseRequestService = testFixture.ServiceProvider.GetService<ICaseRequestService>()
                ?? throw new Exception("Case Request Service is missing");

            _caseDataService = testFixture.ServiceProvider.GetService<ICaseDataService>()
                ?? throw new Exception("Case Data Service is missing");
        }

        [Fact]
        public async Task CaseRequest_4DAPI_ProcessesSuccessfully()
        {
            // Arrange
            // Read the request body from the JSON file
            var requestBodyPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "CaseRequest.json");
            var requestBodyContent = await File.ReadAllTextAsync(requestBodyPath);

            // Use the JsonSerializerOptions to configure serialization settings
            var options = new JsonSerializerOptions
            {
                WriteIndented = false, // Disable pretty-printing to avoid special characters like \n
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Allow relaxed escaping
                PropertyNameCaseInsensitive = true //ignore case
            };

            // Deserialize the JSON content into a CaseRequest object
            var caseRequest = JsonSerializer.Deserialize<CaseRequest>(requestBodyContent, options)
                ?? throw new InvalidOperationException("Failed to deserialize CaseRequest.");

            // Use the CaseRequest helper to set the case code to a new GUID
            caseRequest.CaseCd = CaseRequestHelper.GenerateRandomCaseCD();

            // Serialize the updated CaseRequest object back to JSON
            var updatedRequestBodyContent = JsonSerializer.Serialize(caseRequest);
            
            // Act
            var response = await _caseRequestService.PostCaseRequestAsync(updatedRequestBodyContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Asserts that the status code is 2xx
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseContent); // Add more assertions as needed

            var job = await GetJob(caseRequest);
            if (job == null)
            {
                Assert.Fail("Job was not found for the given case request.");
                return;
            }

            var caseRequestResult = await PollJobStatusAsync(caseRequest.CountryCd, caseRequest.OrgCd, caseRequest.AccountCd, caseRequest.BatchCd, caseRequest.CaseCd, job.JobCd);

            if (caseRequestResult == 2)
            {
                // Job completed successfully
                Assert.True(true, "Job completed successfully.");
            }
            else if (caseRequestResult == -1)
            {
                // Job encountered an error
                Assert.Fail("Job encountered an error from the algo");
            }
            else
            {
                // Unexpected job status
                Assert.Fail($"Unexpected job status: {caseRequestResult}");
            }
        }

        private async Task<Job?> GetJob(CaseRequest caseRequest)
        {
            var countryCd = caseRequest.CountryCd; // Set from caseRequest parameter
            var orgCd = caseRequest.OrgCd; // Set from caseRequest parameter
            var accountCd = caseRequest.AccountCd; // Set from caseRequest parameter
            var batchCd = caseRequest.BatchCd; // Set from caseRequest parameter
            var caseCd = caseRequest.CaseCd; // Set from caseRequest parameter

            var jobs = await _caseDataService.GetJobsByCaseCdAsync(countryCd, orgCd, accountCd, batchCd, caseCd);
            return jobs.FirstOrDefault();
        }

        /// 1 In progress
        /// 2 Complete
        /// -1 Error</returns>
        private async Task<int> GetJobStatusAsync(string countryCd, string orgCd, string accountCd, string batchCd, string caseCd, string jobCd)
        {
            var response = await _caseDataService.GetJobStatusAsync(countryCd,orgCd,accountCd,batchCd,caseCd,jobCd);

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Read the response content as a string
            var responseContent = await response.Content.ReadAsStringAsync();

            // Parse the response content as an integer and return it
            if (int.TryParse(responseContent, out var jobStatus))
            {
                return jobStatus;
            }

            throw new InvalidOperationException("Failed to parse job status from response.");
        }

        /// <summary>
        /// Polls the job status asynchronous.
        /// </summary>
        /// <param name="countryCd">The country cd.</param>
        /// <param name="orgCd">The org cd.</param>
        /// <param name="accountCd">The account cd.</param>
        /// <param name="batchCd">The batch cd.</param>
        /// <param name="caseCd">The case cd.</param>
        /// <exception cref="System.TimeoutException">Polling job status exceeded the maximum time limit of 20 minutes.</exception>
        private async Task<int> PollJobStatusAsync(string countryCd, string orgCd, string accountCd, string batchCd, string caseCd, string jobCd)
        {
            const int maxRetries = 20; // Maximum number of retries (20 minutes)
            const int delayInMilliseconds = 60000; // Delay between retries (1 minute)

            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                var jobStatus = await GetJobStatusAsync(countryCd, orgCd, accountCd, batchCd, caseCd, jobCd);

                if (jobStatus != 1) // If the status is not "In Progress"
                {
                    return jobStatus; // Exit the polling loop
                }

                await Task.Delay(delayInMilliseconds); // Wait for 1 minute before the next attempt
            }

            throw new TimeoutException("Polling job status exceeded the maximum time limit of 20 minutes.");
        }
    }
}