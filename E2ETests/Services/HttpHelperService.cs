using System;
using Microsoft.Extensions.Configuration;
using E2ETests.Interfaces;

namespace E2ETests.Services
{
    /// <summary>
    /// Helper class for executing API calls
    /// </summary>
    public sealed class HttpHelperService : IHttpHelperService
    {
        private readonly ITokenCacheMiddleware _tokenCacheMiddleware;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration</param>
        /// <param name="tokenCacheMiddleware">Token cache middleware</param>
        public HttpHelperService(IConfiguration configuration, ITokenCacheMiddleware tokenCacheMiddleware)
        {
            _tokenCacheMiddleware = tokenCacheMiddleware ?? throw new ArgumentNullException(nameof(tokenCacheMiddleware));
        }

        /// <summary>
        /// Gets Auth0 Bearer Token
        /// </summary>
        /// <returns>auth0 bearer token</returns>
        public string GetAuth0BearerToken(string audience)
        {
            return _tokenCacheMiddleware.GetTokenFromThirdParty(audience);
        }

        /// <summary>
        /// Proxy to token middleware to get a token from third party.
        /// </summary>
        public string GetTokenFromThirdParty(string tokenName)
        {
            return _tokenCacheMiddleware.GetTokenFromThirdParty(tokenName);
        }

        /// <summary>
        /// Proxy to token middleware to get a token from context/cache.
        /// </summary>
        public string GetTokenFromContext(string tokenName)
        {
            return _tokenCacheMiddleware.GetTokenFromContext(tokenName);
        }
    }

    /// <summary>
    /// HTTP Extension Helper (Token Cache) - retained for backward compatibility.
    /// </summary>
    public static class HttpHelperExtensions
    {
        private static ITokenCacheMiddleware _tokenCacheMiddleware;

        /// <summary>
        /// Configures the specified token cache middleware.
        /// </summary>
        /// <param name="tokenCacheMiddleware">The token cache middleware.</param>
        public static void Configure(ITokenCacheMiddleware tokenCacheMiddleware)
        {
            _tokenCacheMiddleware = tokenCacheMiddleware;
        }

        /// <summary>
        /// Gets the token from third party.
        /// </summary>
        public static string GetTokenFromThirdParty(string tokenName)
        {
            if (_tokenCacheMiddleware == null) throw new InvalidOperationException("TokenCacheMiddleware not configured.");
            return _tokenCacheMiddleware.GetTokenFromThirdParty(tokenName);
        }

        /// <summary>
        /// Gets the token from context.
        /// </summary>
        public static string GetTokenFromContext(string tokenName)
        {
            if (_tokenCacheMiddleware == null) throw new InvalidOperationException("TokenCacheMiddleware not configured.");
            return _tokenCacheMiddleware.GetTokenFromContext(tokenName);
        }
    }
}