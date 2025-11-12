using E2ETests.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace E2ETests.Services
{
    /// <summary>
    /// Methods for calling the 4DPathAPI
    /// </summary>
    public class CaseRequestService : ICaseRequestService
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>s
        /// Initializes a new instance of the <see cref="CaseRequestService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public CaseRequestService(IHttpClientFactory httpClientFactory, IHttpHelperService httpHelper, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("CaseRequestServiceHttpClient");
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", httpHelper.GetAuth0BearerToken(configuration["auth0-4dpath-public-api-audience"]));
        }

        public async Task<HttpResponseMessage> PostCaseRequestAsync(string caseRequestJson)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "case-request")
            {
                Content = new StringContent(caseRequestJson, Encoding.UTF8, "application/json")
            };

            // If you use a token, print (masked) token for debugging:
            // var token = _httpHelper.GetAuth0BearerToken(_configuration["auth0-4dpath-public-api-audience"]);
            // Console.WriteLine($"Token (first 8 chars): {token?.Substring(0, Math.Min(8, token.Length))}");

            // Print outgoing request (important)
            Console.WriteLine("=== OUTGOING 4D REQUEST ===");
            Console.WriteLine($"URI: {_httpClient.BaseAddress}{request.RequestUri}");
            Console.WriteLine($"Method: {request.Method}");
            Console.WriteLine("Headers:");
            foreach (var h in request.Headers) Console.WriteLine($"  {h.Key}: {string.Join(", ", h.Value)}");
            if (request.Content != null)
            {
                Console.WriteLine($"Content-Type: {request.Content.Headers.ContentType}");
                Console.WriteLine("Body:");
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Console.WriteLine("===========================");

            try
            {
                var resp = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                Console.WriteLine($"4D HTTP: {(int)resp.StatusCode} {resp.ReasonPhrase}");
                var respBody = await resp.Content.ReadAsStringAsync();
                Console.WriteLine("4D RESPONSE BODY:");
                Console.WriteLine(respBody);
                return resp;
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("Request timeout/TaskCanceledException:");
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Request failed:");
                Console.WriteLine(ex);
                throw;
            }
        }



    }
}
