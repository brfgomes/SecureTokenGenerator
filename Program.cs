using System;
using System.Security.Cryptography;
using System.Text;

public class TokenGenerator
{
    private static readonly char[] SpecialCharacters = "!@#$%^&*()_-+=[]{}|;:,.<>?".ToCharArray();

    public static string GenerateToken(int size = 64, int specialCharCount = 10)
    {
        var data = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(data);
        }

        // Convert bytes to Base64 and remove padding ('=')
        var base64String = Convert.ToBase64String(data).TrimEnd('=');
        var tokenBuilder = new StringBuilder(base64String);

        // Add special characters
        var random = new Random();
        for (int i = 0; i < specialCharCount; i++)
        {
            int specialCharIndex = random.Next(SpecialCharacters.Length);
            int insertPosition = random.Next(tokenBuilder.Length);
            tokenBuilder.Insert(insertPosition, SpecialCharacters[specialCharIndex]);
        }

        return tokenBuilder.ToString();
    }

    public static void Main()
    {
        string token = GenerateToken(64, 10);
        Console.WriteLine($"Generated Token: {token}");
    }
}
