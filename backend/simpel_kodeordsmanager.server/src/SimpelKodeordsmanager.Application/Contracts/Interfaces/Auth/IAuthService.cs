using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Auth;

public interface IAuthService
{
    Task<UserIdentityModel> LoginAsync(string email, string password);
}