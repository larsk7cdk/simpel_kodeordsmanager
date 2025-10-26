namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;

public interface ITokenService
{
    Task<(string token, int expiresIn)> GenerateAsync(int userId, string email);
}