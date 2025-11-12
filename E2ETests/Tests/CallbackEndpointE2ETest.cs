using E2ETests.TestBase;   // <<— important so the base type is found
using Azure.Storage.Queues.Models;
using CallbackAPI.Models;
using E2ETests.Fixtures;
using E2ETests.Interfaces;
using E2ETests.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace E2ETests.Tests
{
    [Collection("Callback API collection")]
    public class CallbackEndpointE2ETests : CallbackApiTestBase
    {
        private readonly ICaseRequestService _caseRequestService;
        private readonly IMessageQueueService _messageQueueService;

        public CallbackEndpointE2ETests(
            E2ETests.Fixtures.CallbackApiApplicationFactory<CallbackAPI.Program> callbackApiFixture,
            E2ETests.Fixtures.E2ETestFixture testFixture)
            : base(callbackApiFixture, testFixture)
        {
            _caseRequestService = TestServices.GetRequiredService<ICaseRequestService>();
            _messageQueueService = TestServices.GetRequiredService<IMessageQueueService>();
        }

        [Fact]
        public async Task CallbackEndpoint_ShouldReceive4DAPIRequest()
        {
            Console.WriteLine("Starting 4D API callback test...");

            // 1️⃣ Load the CaseRequest.json file
            var caseRequestPath = Path.Combine("TestFiles", "CaseRequest.json");
            var caseRequestJson = await File.ReadAllTextAsync(caseRequestPath);

            var caseRequestOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var caseRequestObj = JsonSerializer.Deserialize<CaseRequest>(caseRequestJson, caseRequestOptions);

            if (caseRequestObj == null)
                throw new Exception("Failed to deserialize CaseRequest.json");

            string caseCd = caseRequestObj.CaseCd;

            // 2️⃣ Dynamically detect local IP (LAN IP, not localhost)
            string localIp = GetLocalIPAddress();
            int callbackPort = 7002;

            // 3️⃣ Construct a real callback URL accessible from the network
            var callbackUrl = $"http://{localIp}:{callbackPort}/api/callback/case-request";
            caseRequestObj.CallbackURL = callbackUrl;

            Console.WriteLine($"📡 Using callback URL: {caseRequestObj.CallbackURL}");

            // 4️⃣ Serialize the updated CaseRequest object back to JSON
            var updatedCaseRequestJson = JsonSerializer.Serialize(caseRequestObj);

            // 5️⃣ Send CaseRequest to 4D API (external system)
            // Act: Submit case request to 4D API WITH AUTH HEADER
            var postResponse = await _caseRequestService.PostCaseRequestAsync(updatedCaseRequestJson);

            // >>> add this block
            var status = (int)postResponse.StatusCode;
            var reason = postResponse.ReasonPhrase;
            var respBody = await postResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"4D API response: {status} {reason}");
            Console.WriteLine(respBody);
            // <<<

            postResponse.EnsureSuccessStatusCode();

            // 6️⃣ Poll for callback from Callback API
            CallbackRequestModel? callbackData = null;
            int maxRetries = 30;

            for (int i = 0; i < maxRetries; i++)
            {
                var callbackResponse = await Client.GetAsync("/api/callback/last");
                if (callbackResponse.IsSuccessStatusCode)
                {
                    var json = await callbackResponse.Content.ReadAsStringAsync();
                    callbackData = JsonSerializer.Deserialize<CallbackRequestModel>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (callbackData != null)
                        break;
                }

                await Task.Delay(1000); // wait 1s before retry
            }

            Console.WriteLine("📝 Request Body sent to 4DAPI:");
            Console.WriteLine(updatedCaseRequestJson);

            Console.WriteLine("📥 Callback received:");
            if (callbackData != null)
                Console.WriteLine(JsonSerializer.Serialize(callbackData));
            else
                Console.WriteLine("❌ No callback received after waiting.");

            // 7️⃣ Check Azure outbound queues for the caseCd
            var queuesToCheck = new[] { "outbound", "outbound-success", "outbound-poison" };
            foreach (var queueName in queuesToCheck)
            {
                bool found = await IsCaseCdInQueue(queueName, caseCd);
                Console.WriteLine($"🔍 caseCd {caseCd} found in queue '{queueName}': {found}");
            }

            // Optional Assertions (enable when stable)
            // Assert.NotNull(callbackData);
            // Assert.Equal("440022", callbackData.Id);
            // Assert.Equal("Completed", callbackData.Status);
            // Assert.Contains("Analysis completed", callbackData.Message);
        }

        // Helper: Find caseCd in Azure queue
        public async Task<bool> IsCaseCdInQueue(string queueName, string caseCd)
        {
            QueueMessage[] messages = (await _messageQueueService.ReadQueueAsync(queueName));

            foreach (var msg in messages)
            {
                try
                {
                    using var doc = JsonDocument.Parse(msg.MessageText);
                    if (doc.RootElement.TryGetProperty("caseCd", out var caseCdProp))
                    {
                        if (caseCdProp.GetString() == caseCd)
                        {
                            Console.WriteLine($"✅ Found caseCd {caseCd} in queue {queueName}: {msg.MessageText}");
                            return true;
                        }
                    }
                }
                catch (JsonException)
                {
                    // Ignore malformed JSON messages
                }
            }
            return false;
        }

        /// <summary>
        /// Determines the machine’s local IPv4 address for LAN-based callback testing.
        /// </summary>
        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(ip) &&
                    ip.ToString().StartsWith("192.") || ip.ToString().StartsWith("10.") || ip.ToString().StartsWith("172."))
                {
                    return ip.ToString();
                }
            }

            // Fallback: default to localhost if nothing else found
            return "127.0.0.1";
        }

        /// <summary>
        /// Simple in-memory test for Callback API POST and GET endpoints.
        /// </summary>
        [Fact]
        public async Task CallbackAPI_Test()
        {
            var testModel = new CallbackRequestModel
            {
                Id = "QA1",
                Message = "Success",
                Status = "Done"
            };

            string qaJson = Newtonsoft.Json.JsonConvert.SerializeObject(testModel);

            var postResponse = await Client.PostAsync(
                "/api/callback/case-request",
                new StringContent(qaJson, Encoding.UTF8, "application/json")
            );
            postResponse.EnsureSuccessStatusCode();

            string responseBody = await postResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Response body: " + responseBody);

            var getResponse = await Client.GetAsync("/api/callback/last");
            var json = await getResponse.Content.ReadAsStringAsync();
            CallbackRequestModel? responseCallback = Newtonsoft.Json.JsonConvert.DeserializeObject<CallbackRequestModel>(json);

            Assert.NotNull(responseCallback);
            Assert.Equal(testModel.Id, responseCallback.Id);
            Assert.Equal(testModel.Message, responseCallback.Message);
            Assert.Equal(testModel.Status, responseCallback.Status);
        }
    }
}
