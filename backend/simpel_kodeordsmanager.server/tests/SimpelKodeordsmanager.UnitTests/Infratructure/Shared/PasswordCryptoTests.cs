using Microsoft.Extensions.Options;
using SimpelKodeordsmanager.Domain.Models;
using SimpelKodeordsmanager.Infrastructure.Shared;


namespace SimpelKodeordsmanager.UnitTests.Infratructure.Shared;

public class PasswordCryptoTests
{
    [Fact]
    public void Encrypt_SamePlaintext_TwoCalls_ProduceDifferentCiphertexts()
    {
        // Arrange
        var cryptoModel = new Crypto
        {
            Key = "0123456789ABCDEF0123456789ABCDEF",
            IVSize = 16
        };
        var options = Options.Create(cryptoModel);
        var sut = new PasswordCrypto(options);

        const string password = "My$ecretP@ssw0rd";

        // Act - encrypt the same plaintext twice
        var cipher1 = sut.Encrypt(password);
        var cipher2 = sut.Encrypt(password);

        // Assert - with a random IV, ciphertexts should differ
        Assert.False(string.IsNullOrWhiteSpace(cipher1));
        Assert.False(string.IsNullOrWhiteSpace(cipher2));
        Assert.NotEqual(cipher1, cipher2);

        // Also verify both decrypt correctly
        Assert.Equal(password, sut.Decrypt(cipher1));
        Assert.Equal(password, sut.Decrypt(cipher2));
    }
}