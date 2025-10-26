using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Infrastructure.Shared;

/// <summary>
/// Provides AES-based encryption and decryption for passwords.
/// </summary>
public class PasswordCrypto(IOptions<Crypto> crypto) : IPasswordCrypto
{
    /// <summary>
    /// Encrypts a plaintext password and returns a base64-encoded ciphertext that contains
    /// the IV (initialization vector) followed by the encrypted data.
    /// </summary>
    /// <returns>Base64 string containing IV + ciphertext.</returns>
    public string Encrypt(string password)
    {
        try
        {
            // Create and configure an Aes instance (key is set in CreateAes).
            using var aes = CreateAes();

            // Generate a cryptographically-random IV for this encryption operation.
            // The IV must be unique per encryption when using CBC mode.
            var iv = new byte[crypto.Value.IVSize];
            RandomNumberGenerator.Fill(iv);
            aes.IV = iv;

            // A MemoryStream receives the IV followed by the ciphertext bytes.
            using var memoryStream = new MemoryStream();

            // Write the IV at the beginning of the stream so Decrypt can read it back.
            memoryStream.Write(aes.IV, 0, crypto.Value.IVSize);

            // Create an encryptor from the AES instance and write the plaintext into a
            // CryptoStream which performs the encryption and writes to the memory stream.
            using (var encryptor = aes.CreateEncryptor())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(password);
            }

            // Convert the full stream (IV + ciphertext) to base64 for safe storage/transmission.
            var encrypted = Convert.ToBase64String(memoryStream.ToArray());
            return encrypted;
        }

        catch (CryptographicException ex)
        {
            throw new InvalidOperationException("Password encryption failed.", ex);
        }
    }

    /// <summary>
    /// Decrypts a base64-encoded ciphertext and returns the original plaintext password.
    /// </summary>
    /// <returns>The decrypted plaintext password.</returns>
    public string Decrypt(string cipherText)
    {
        try
        {
            // Decode the base64 string back into raw bytes (IV + ciphertext).
            var cipherData = Convert.FromBase64String(cipherText);

            // Basic validation: ensure we have at least enough bytes for an IV.
            if (cipherData.Length < crypto.Value.IVSize)
            {
                throw new CryptographicException("Invalid cipher text format.");
            }

            // Separate the IV and the actual encrypted payload.
            var iv = new byte[crypto.Value.IVSize];
            var encryptedData = new byte[cipherData.Length - crypto.Value.IVSize];

            // Copy the IV from the start of the buffer.
            Buffer.BlockCopy(cipherData, 0, iv, 0, crypto.Value.IVSize);

            // Copy the rest as the encrypted payload.
            Buffer.BlockCopy(cipherData, crypto.Value.IVSize, encryptedData, 0, encryptedData.Length);

            // Create an AES instance configured with the key. We'll set the IV we read
            // from the payload before creating the decryptor so decryption uses the
            // correct IV generated during encryption.
            using var aes = CreateAes();
            aes.IV = iv;

            // Place the encrypted payload into a MemoryStream and read it through a CryptoStream
            // using the AES decryptor; then read the plaintext using a StreamReader.
            using var memoryStream = new MemoryStream(encryptedData);
            using var decryptor = aes.CreateDecryptor();
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);

            var plainText = streamReader.ReadToEnd();
            return plainText;
        }

        catch (CryptographicException ex)
        {
            throw new InvalidOperationException("Password decryption failed.", ex);
        }
    }

    /// <summary>
    /// Helper that creates and configures an Aes instance using values from the injected crypto options.
    /// </summary>
    private Aes CreateAes()
    {
        var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(crypto.Value.Key);

        return aes;
    }
}