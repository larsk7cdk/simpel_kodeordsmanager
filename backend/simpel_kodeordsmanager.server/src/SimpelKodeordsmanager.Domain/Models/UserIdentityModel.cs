namespace SimpelKodeordsmanager.Domain.Models;

public class UserIdentityModel
{
    public required string Email { get; init; }
    public required bool IsAuthenticated { get; init; }
    public required string JwtToken { get; init; }
    public required int ExpiresIn { get; init; }
    public required string Status { get; init; }
}