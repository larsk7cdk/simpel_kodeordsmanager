using System.Security.Cryptography;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;

namespace SimpelKodeordsmanager.Infrastructure.Shared;

internal class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16; // Byte array size of Salt
    private const int HashSize = 32; // Byte array size of Hash
    private const int HashIterations = 100000; // Number of iterations to calculate the Hash

    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA512; // The Algorithm to use 

    public string Hash(string password)
    {
        // Generate a random salt
        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        // Generate the hash for the password
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashIterations, HashAlgorithmName, HashSize);

        // Return the password hash and the salt used
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool Verify(string password, string passwordHash)
    {
        // Split the password hash into hash and salt
        var parts = passwordHash.Split("-");
        var hash = Convert.FromHexString(parts[0]);
        var salt = Convert.FromHexString(parts[1]);

        // Generate the hash for the saved password 
        var inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashIterations, HashAlgorithmName, HashSize);

        // Compare the hash with a fixed time based upon hash soze
        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}