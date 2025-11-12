using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Azure.Identity;
using E2ETests.Interfaces;
using E2ETests.Models;
using E2ETests.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace E2ETests.Fixtures
{
    public class E2ETestFixture : IDisposable
    {
        private bool _disposed = false;

        public IServiceProvider ServiceProvider { get; private set; } = default!;
        public IConfiguration Configuration { get; private set; } = default!;

        public E2ETestFixture()
        {
            // Load base config to get Key Vault URL
            var tempConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var keyVaultURL = tempConfig["ConnectionStrings:KeyVaultURL"]
                ?? throw new InvalidOperationException("Missing ConnectionStrings:KeyVaultURL in appsettings.json");

            // Final config
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddAzureKeyVault(new Uri(keyVaultURL), new DefaultAzureCredential())
                .Build();

            // Required settings
            var storageAccount = GetRequired("azure-filesystem-connectionstring");
            var casesContainer = GetRequired("azure-filesystem-cases-container");
            var qaDropZone = Configuration["AppSettings:AzureDropZone"];

            var dataAPIURL = GetRequired("dataapi-rooturl");
            var fourdAPIURL = GetRequired("fourdpathapi-rooturl");

            // Auth0 config for TokenCacheMiddleware
            var auth0TokenEndpoint = Configuration["auth0-token-endpoint"];
            var auth0PublicAPIClientID = Configuration["auth0-4dpath-public-api-clientid"];
            var auth0PublicAPIClientSecret = Configuration["auth0-4dpath-public-api-client-secret"];
            var auth0PublicAPIAudience = Configuration["auth0-4dpath-public-api-audience"];
            var auth0PrivateAPIClientID = Configuration["auth0-4dpath-api-clientid"];
            var auth0PrivateAPIClientSecret = Configuration["auth0-4dpath-api-client-secret"];
            var auth0PrivateAudience = Configuration["auth0-4dpath-api-audience"];

            var authConfigs = new List<AuthConfigModel>
            {
                new AuthConfigModel(
                    audience:     auth0PublicAPIAudience,
                    clientId:     auth0PublicAPIClientID,
                    clientSecret: auth0PublicAPIClientSecret,
                    tokenEndPoint:auth0TokenEndpoint
                ),
                new AuthConfigModel(
                    audience:     auth0PrivateAudience,
                    clientId:     auth0PrivateAPIClientID,
                    clientSecret: auth0PrivateAPIClientSecret,
                    tokenEndPoint:auth0TokenEndpoint
                )
            };

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging();
                    services.AddMemoryCache();
                    services.AddHttpContextAccessor();
                    services.AddHttpClient();

                    services.AddSingleton<IConfiguration>(Configuration);

                    services.AddSingleton<ITokenCacheMiddleware>(sp =>
                        new TokenCacheMiddleware(
                            sp.GetRequiredService<IHttpContextAccessor>(),
                            sp.GetRequiredService<IMemoryCache>(),
                            authConfigs
                        ));

                    services.AddTransient<IHttpHelperService, HttpHelperService>();

                    var dataBase = new Uri($"{TrimTrailingSlash(dataAPIURL)}/api/");
                    var fourdBase = new Uri($"{TrimTrailingSlash(fourdAPIURL)}/api/");

                    services.AddHttpClient("CaseDataServiceHttpClient", c => c.BaseAddress = dataBase);
                    services.AddHttpClient("CaseRequestServiceHttpClient", c => c.BaseAddress = fourdBase);

                    services.AddTransient<ICaseDataService>(sp =>
                    {
                        var type = typeof(CaseDataService);
                        if (CtorAcceptsHttpClient(type))
                        {
                            var http = sp.GetRequiredService<IHttpClientFactory>()
                                         .CreateClient("CaseDataServiceHttpClient");
                            return (ICaseDataService)ActivatorUtilities.CreateInstance(sp, type, http);
                        }
                        return (ICaseDataService)ActivatorUtilities.CreateInstance(sp, type);
                    });

                    services.AddTransient<ICaseRequestService>(sp =>
                    {
                        var type = typeof(CaseRequestService);
                        if (CtorAcceptsHttpClient(type))
                        {
                            var http = sp.GetRequiredService<IHttpClientFactory>()
                                         .CreateClient("CaseRequestServiceHttpClient");
                            return (ICaseRequestService)ActivatorUtilities.CreateInstance(sp, type, http);
                        }
                        return (ICaseRequestService)ActivatorUtilities.CreateInstance(sp, type);
                    });

                    services.AddScoped<IDropZoneService>(_ => new DropZoneService(storageAccount, qaDropZone));

                    services.AddTransient<IMessageQueueService, MessageQueueService>();
                })
                .Build();

            ServiceProvider = host.Services;

            string GetRequired(string key) =>
                Configuration[key] ?? throw new InvalidOperationException($"Missing configuration value: {key}");

            static string TrimTrailingSlash(string url) =>
                string.IsNullOrWhiteSpace(url) ? url : url.TrimEnd('/');

            static bool CtorAcceptsHttpClient(Type t)
            {
                return t.GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                        .SelectMany(c => c.GetParameters())
                        .Any(p => p.ParameterType == typeof(HttpClient));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && ServiceProvider is IDisposable d)
                    d.Dispose();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
