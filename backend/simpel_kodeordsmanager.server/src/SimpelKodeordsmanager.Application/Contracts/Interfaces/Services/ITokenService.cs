namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Services;

public interface ITokenService
{
    (string token, int expiresIn) Generate(string email);
}