namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;

public interface ITokenService
{
    (string token, int expiresIn) Generate(string email);
}