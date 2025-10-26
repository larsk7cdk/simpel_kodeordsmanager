namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;

public interface IPasswordCrypto
{
    string Encrypt(string password);

    string Decrypt(string cipherText);
}