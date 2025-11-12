using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E2ETests.Helpers
{
    public class TokenService
    {
        private readonly string _auth0TokenEndpoint;
        private readonly string _clientID;
        private readonly string _clientSecret;
        private readonly string _audience;

        public TokenService(string auth0TokenEndpoint, string auth0ClientID, string auth0ClientSecret, string auth0Audience)
        {
            // Initialize fields with constructor parameters
            _auth0TokenEndpoint = auth0TokenEndpoint;
            _clientID = auth0ClientID;
            _clientSecret = auth0ClientSecret;
            _audience = auth0Audience;
        }

        /// <summary>
        /// Gets the token from third party.
        /// </summary>
        /// <param name="tokenName">Name of the token.</param>
        /// <returns>
        /// a brand new token from third party endpoint
        /// </returns>
        public string GetTokenFromAuth0()
        {
            //we need a new token
            var client = new HttpClient();
            client.BaseAddress = new Uri(_auth0TokenEndpoint);
            var body = @$"{{""client_id"":""{_clientID}"",""client_secret"":""{_clientSecret}"",""audience"":""{_audience}"",""grant_type"":""client_credentials""}}";
            using StringContent json = new(
                body,
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            using HttpResponseMessage httpResponse =
                client.PostAsync("", json).Result;

            var responseJson = httpResponse.Content.ReadAsStringAsync().Result;
            var Auth0Result = JsonSerializer.Deserialize<Dictionary<string, object>>(responseJson);

            string newToken = string.Empty;
            if (Auth0Result != null && Auth0Result.TryGetValue("access_token", out object? value) && value != null)
            {
                newToken = value.ToString() ?? string.Empty;
            }

            return newToken;
        }
    }
}
