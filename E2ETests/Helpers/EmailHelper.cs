using System;
using System.Text;

namespace E2ETests.Helpers
{
    public static class EmailHelper
    {
        private static Random random = new Random();

        public static string GenerateRandomEmail(int usernameLength)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder username = new StringBuilder();

            for (int i = 0; i < usernameLength; i++)
            {
                username.Append(chars[random.Next(chars.Length)]);
            }

            return username.ToString() + "@testing.com";
        }
    }
}
