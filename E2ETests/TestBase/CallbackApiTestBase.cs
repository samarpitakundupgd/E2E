using E2ETests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace E2ETests.TestBase
{
    // CallbackApiTestBase.cs
    public abstract class CallbackApiTestBase : IAsyncLifetime
    {
        protected HttpClient Client { get; private set; }
        protected IServiceProvider TestServices { get; }

        protected CallbackApiTestBase(
            CallbackApiApplicationFactory<CallbackAPI.Program> factory,
            E2ETestFixture testFixture)
        {
            // Point to the real running host
            var baseUrl = Environment.GetEnvironmentVariable("CALLBACK_BASEURL") ?? "http://localhost:7002";
            Client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            TestServices = testFixture.ServiceProvider;
        }

        public Task InitializeAsync() => Task.CompletedTask;
        public Task DisposeAsync() { Client.Dispose(); return Task.CompletedTask; }
    }

}
