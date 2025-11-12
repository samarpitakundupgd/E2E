namespace E2ETests.Interfaces
{
    /// <summary>
    /// Interface for token access.
    /// </summary>
    public interface ITokenCacheMiddleware
    {
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <returns>Gets a token from cache or http context if not in cache</returns>
        string GetTokenFromContext(string tokenName);

        /// <summary>
        /// Gets the token from third party.
        /// </summary>
        /// <param name="tokenName">Name of the token.</param>
        /// <returns>Gets a token from third party if not in cache</returns>
        string GetTokenFromThirdParty(string tokenName);

        string GetAudience();
    }
}
