using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using E2ETests.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using E2ETests.Models;

namespace E2ETests.Services
{
    /// <summary>
    /// Middleware to support retrieving the bearer token from the http context or the cache
    /// If the token is vaild in the cache, it is returned by GetToken()
    /// If the token is not in the cache or expired, get the token from the http context and add it to the cache and give it back to the caller
    /// </summary>
    public class TokenCacheMiddleware : ITokenCacheMiddleware
    {
        private const int TOKEN_EXPIRATION_BUFFER_MINUTES = -1; //if the token is not expired but will be within a minute, let's grab a new token
        private const int TOKEN_EXPIRATION_LENGTH = 12;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _memoryCache;
        private readonly string _clientID;
        private readonly string _clientSecret;
        private readonly string _audience;
        private readonly string _tokenEndpoint;
        List<AuthConfigModel> _authConfigs;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenCacheMiddleware" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="clientID">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="audience">The audience.</param>
        /// <param name="tokenEndpoint">The token endpoint.</param>
        public TokenCacheMiddleware(IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache, List<AuthConfigModel> authConfigs)
        {
            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
            _authConfigs = authConfigs;
        }

        public string GetAudience()
        {
            return _audience;
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <returns>Returns a token from the HTTP Context or from the cache</returns>
        public string GetTokenFromContext(string tokenName)
        {
            string token = GetTokenFromCache(tokenName);

            if (token != null && token.Length > 0 && ValidateToken(token))
            {
                //cool all set
                return token;
            }
            else
            {
                //we need a new token
                token = GetTokenFromContext();
                if (ValidateToken(token))
                {
                    //cool all set
                    AddTokenToCache(token);

                    return token;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the token from third party.
        /// </summary>
        /// <param name="tokenName">Name of the token.</param>
        /// <returns>
        /// a brand new token from third party endpoint
        /// </returns>
        public string GetTokenFromThirdParty(string tokenName)
        {
            string token = GetTokenFromCache(tokenName);

            if (token != null && token.Length > 0 && ValidateToken(token))
            {
                //cool all set
                return token;
            }
            else
            {
                var clientID = string.Empty;
                var clientSecret = string.Empty;
                var audience = string.Empty;
                var tokenEndpoint = string.Empty;

                _authConfigs.Where(a => a.Audience == tokenName).ToList().ForEach(config =>
                {
                    clientID = config.ClientId;
                    clientSecret = config.ClientSecret;
                    audience = config.Audience;
                    tokenEndpoint = config.TokenEndPoint;
                });

                //we need a new token
                var client = new HttpClient();
                client.BaseAddress = new Uri(tokenEndpoint);
                var body = @$"{{""client_id"":""{clientID}"",""client_secret"":""{clientSecret}"",""audience"":""{audience}"",""grant_type"":""client_credentials""}}";
                using StringContent json = new(
                    body,
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json);

                using HttpResponseMessage httpResponse =
                    client.PostAsync("", json).Result;

                var responseJson = httpResponse.Content.ReadAsStringAsync().Result;
                var Auth0Result = JsonSerializer.Deserialize<Dictionary<string, object>>(responseJson);
                var newToken = Auth0Result.ContainsKey("access_token") ? Auth0Result["access_token"].ToString() : string.Empty;

                if (ValidateToken(newToken))
                {
                    //cool all set
                    AddTokenToCache(newToken);

                    return newToken;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the token from context.
        /// </summary>
        /// <returns>The token from the http context</returns>
        private string GetTokenFromContext()
        {
            //get the token from the http context, the access token is always called 'access_token' as it's the jwt standard
            var getTokenTask = _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            //if the task is completed successfully then we have the token
            if (getTokenTask.IsCompletedSuccessfully)
            {
                return getTokenTask.Result;
            }
            else
            {
                //there is no token available in the http context or we are unable to get the token from the context
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the token from cache.
        /// </summary>
        /// <returns>the token from the memory cache</returns>
        private string GetTokenFromCache(string tokenName)
        {
            _memoryCache.TryGetValue<string>(tokenName, out string tokenFromCache);

            return tokenFromCache;
        }

        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>True if the token is valid, false if not</returns>
        private bool ValidateToken(string token)
        {
            //new token handler
            var securityTokenHandler = new JwtSecurityTokenHandler();

            //if the jwt syntax is valid keep going
            if (securityTokenHandler.CanReadToken(token))
            {
                //let put the string token into a jwt token object
                JwtSecurityToken decriptedToken = securityTokenHandler.ReadJwtToken(token);

                //now we can get the claims in the token
                IEnumerable<System.Security.Claims.Claim> claims = decriptedToken.Claims;

                //get the token expiration based on the jwt standard "exp"
                double.TryParse(claims.Where(c => c.Type == "exp").FirstOrDefault().Value, out double expiredClaimValue);   //i.e. 1749053950 from exp in the token

                if (expiredClaimValue == 0)
                    return false; // Invalid or missing exp claim

                // expiredClaimValue is a double representing Unix seconds
                DateTime expirationUtc = DateTimeOffset.FromUnixTimeSeconds((long)expiredClaimValue).UtcDateTime;          //i.e. 6/4/2025 4:20:10 PM -04:00
                return DateTime.UtcNow.AddMinutes(TOKEN_EXPIRATION_BUFFER_MINUTES) < expirationUtc;                        //i.e. 6/3/2025 7:18:30 PM < 6/4/2025 4:20:10 PM -04:00 >  so not expired
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds the token to cache.
        /// </summary>
        /// <param name="Token">The token.</param>
        private void AddTokenToCache(string Token)
        {
            var securityTokenHandler = new JwtSecurityTokenHandler();

            //if the jwt syntax is valid keep going
            if (securityTokenHandler.CanReadToken(Token))
            {
                //let put the string token into a jwt token object
                JwtSecurityToken decriptedToken = securityTokenHandler.ReadJwtToken(Token);

                //now we can get the claims in the token
                IEnumerable<System.Security.Claims.Claim> claims = decriptedToken.Claims;

                //get the token expiration based on the jwt standard "exp"
                long.TryParse(claims.Where(c => c.Type == "exp").FirstOrDefault().Value, out long expirationClaimValue);

                //what is our token name from the audience
                string tokenName = claims.Where(c => c.Type == "aud").FirstOrDefault().Value;

                // let's put the token in the cache with the expiration time minus one minute
                // why one minute? because we want to make sure the token is still valid when we use it and don't want a race condition
                // UnixEpoch is the standard for jwt token expiration
                // we'll convert the Epoch date into a date time object and subtract date time of now - 1 minute
                _memoryCache.Remove(tokenName);

                // Get a TimeSpan representing 24 hours from now (UTC)
                TimeSpan timeSpan = DateTime.UtcNow.AddHours(TOKEN_EXPIRATION_LENGTH).AddMinutes(TOKEN_EXPIRATION_BUFFER_MINUTES) - DateTime.UtcNow;


                _memoryCache.Set(tokenName, Token, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeSpan
                });

            }
        }
    }
}