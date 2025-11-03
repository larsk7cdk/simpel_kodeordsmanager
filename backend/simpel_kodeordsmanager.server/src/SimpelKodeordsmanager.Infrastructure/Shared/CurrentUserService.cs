using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;

namespace SimpelKodeordsmanager.Infrastructure.Shared;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public string? GetUserId()
    {
        var user = User;
        if (user == null) return null;

        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? user.FindFirst("sub")?.Value;
    }
}