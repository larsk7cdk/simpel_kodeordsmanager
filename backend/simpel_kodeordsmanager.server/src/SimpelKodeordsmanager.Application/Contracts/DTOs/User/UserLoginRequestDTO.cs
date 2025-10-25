namespace SimpelKodeordsmanager.Application.Contracts.DTOs.User;

/// <summary>
///     Brugers oplysninger for login
/// </summary>
public class UserLoginRequestDTO
{
    /// <summary>
    ///     Bruger Email
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Bruger kodeord
    /// </summary>
    public required string Password { get; init; }
}