using E2ETests.Interfaces;

namespace E2ETests.Interfaces
{
    /// <summary>
    /// Abstraction for HTTP helper operations (token retrieval).
    /// </summary>
    public interface IHttpHelperService
    {
        /// <summary>
        /// Gets the Auth0 bearer token for the configured audience.
        /// </summary>
        /// <returns>Auth0 bearer token</returns>
        string GetAuth0BearerToken(string audience);

        /// <summary>
        /// Gets a token from the third party (bypassing context cache).
        /// </summary>
        /// <param name="tokenName">Name of the token.</param>
        /// <returns>Token string</returns>
        string GetTokenFromThirdParty(string tokenName);

        /// <summary>
        /// Gets a token from context (cached context token if available).
        /// </summary>
        /// <param name="tokenName">Name of the token.</param>
        /// <returns>Token string</returns>
        string GetTokenFromContext(string tokenName);
    }
}