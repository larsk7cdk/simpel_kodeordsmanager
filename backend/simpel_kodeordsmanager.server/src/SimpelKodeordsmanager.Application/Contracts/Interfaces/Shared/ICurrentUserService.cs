using System.Security.Claims;

namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;

public interface ICurrentUserService
{
    string? GetUserId();
    ClaimsPrincipal? User { get; }
}