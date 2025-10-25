namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Services;

public interface IPasswordHasher
{
    string HashPassword(string password);
}