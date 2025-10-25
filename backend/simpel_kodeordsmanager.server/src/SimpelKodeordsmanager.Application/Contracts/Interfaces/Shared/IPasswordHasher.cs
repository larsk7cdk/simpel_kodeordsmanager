namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;

public interface IPasswordHasher
{
    string Hash(string password);

    bool Verify(string password, string passwordHash);
}