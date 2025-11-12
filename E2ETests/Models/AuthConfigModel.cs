using System;

namespace E2ETests.Models
{
    /*
    Pseudocode / Plan:
    - Provide a detailed parameterized constructor to initialize all properties:
      - Parameters: audience, clientId, clientSecret, tokenEndPoint
      - Validate that none of the parameters are null; throw ArgumentNullException if any are null.
      - Assign parameter values to the corresponding properties.
    - Preserve the existing parameterless constructor for configuration binding scenarios.
    - Keep properties unchanged (non-nullable strings with default empty values).
    - Maintain XML documentation and class accessibility.
    */

    /// <summary>
    /// Configuration settings used to acquire access tokens for end-to-end tests.
    /// </summary>
    /// <remarks>
    /// This model is typically bound from test configuration. It contains sensitive data
    /// (for example, <see cref="ClientSecret"/>); do not commit secrets to source control.
    /// Prefer using a secure secret store or environment variables in CI environments.
    /// </remarks>
    public class AuthConfigModel
    {
        /// <summary>
        /// Parameterless constructor retained for configuration binding.
        /// </summary>
        public AuthConfigModel()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="AuthConfigModel"/> with the provided values.
        /// </summary>
        /// <param name="audience">The expected audience (resource or scope) for the token.</param>
        /// <param name="clientId">The client (application) identifier used to request tokens.</param>
        /// <param name="clientSecret">The client secret used to authenticate the client when requesting tokens.</param>
        /// <param name="tokenEndPoint">The token end point.</param>
        /// <exception cref="ArgumentNullException">Thrown when any argument is null.</exception>
        public AuthConfigModel(string audience, string clientId, string clientSecret, string tokenEndPoint)
        {
            Audience = audience ?? throw new ArgumentNullException(nameof(audience));
            ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            ClientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
            TokenEndPoint = tokenEndPoint ?? throw new ArgumentNullException(nameof(tokenEndPoint));
        }

        /// <summary>
        /// The expected audience (resource or scope) for the token.
        /// </summary>
        public string Audience { get; set; } = string.Empty;
    
        /// <summary>
        /// The client (application) identifier used to request tokens.
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// The client secret used to authenticate the client when requesting tokens.
        /// </summary>
        /// <remarks>
        /// Treat this value as sensitive.
        /// </remarks>
        public string ClientSecret { get; set; } = string.Empty;

        /// <summary>Gets or sets the token end point.</summary>
        /// <value>The token end point.</value>
        public string TokenEndPoint { get; set; } = string.Empty;
    }
}
