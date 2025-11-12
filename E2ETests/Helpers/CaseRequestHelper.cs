namespace E2ETests.Helpers
{
    /// <summary>
    /// Case Request helpers
    /// </summary>
    internal static class CaseRequestHelper
    {
        /// <summary>
        /// Generates the random case cd.
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomCaseCD()
        {
            // Generate a random GUID and return it as a string
            //extend for a specific naming convention if needed
            return Guid.NewGuid().ToString();
        }
    }
}
