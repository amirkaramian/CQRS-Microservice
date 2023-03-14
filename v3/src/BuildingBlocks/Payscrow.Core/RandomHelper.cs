using System;
using System.Security.Cryptography;

public static class RandomHelper
{
    /// <summary>
    /// Common method for creating an instance of System.Random, initialized with Guid.NewGuid().GetHashCode()
    /// as the seed.
    /// </summary>
    public static Random CreateRandom()
    {
        return new Random(Guid.NewGuid().GetHashCode());
    }

    public static string RandomString(int length = 64)
    {
        byte[] randomBytes = new Byte[length];
        using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomBytes);
        }
        SHA256 ShaHashFunction = SHA256.Create();
        byte[] hashedBytes = ShaHashFunction.ComputeHash(randomBytes);
        string randomString = string.Empty;
        foreach (byte b in hashedBytes)
        {
            randomString += string.Format("{0:x2}", b);
        }
        return randomString;
    }

    public static string GenerateRandomCode(int length, bool includeAlphabets = false)

    {
        string characters = includeAlphabets ? "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890" : "1234567890";
        char[] sOTP = new char[length];
        Random rand = new Random();

        for (int i = 0; i < length; i++)
        {
            //int p = rand.Next(0, saAllowedCharacters.Length);

            //string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
            sOTP[i] = characters[rand.Next(0, characters.Length)];
        }

        return new string(sOTP);
    }
}