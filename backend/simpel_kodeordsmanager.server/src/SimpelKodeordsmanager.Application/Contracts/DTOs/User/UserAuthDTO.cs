namespace SimpelKodeordsmanager.Application.Contracts.DTOs.User;

/// <summary>
/// Oplysninger om bruger autentifikation
/// </summary>
public class UserAuthDTO
{
    /// <summary>
    /// Bruger Email
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Bearer token
    /// </summary>
    public required string Token { get; init; }

    /// <summary>
    /// Angiver hvor lang tid token er gyldigt i sekunder
    /// </summary>
    public required long ExpiresIn { get; init; }
}