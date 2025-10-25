using SimpelKodeordsmanager.Application.Contracts.Interfaces.Services;

namespace SimpelKodeordsmanager.Application.Services;

internal class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return password;
    }
}