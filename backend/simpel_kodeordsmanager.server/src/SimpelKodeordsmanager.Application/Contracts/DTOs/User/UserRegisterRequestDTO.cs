namespace SimpelKodeordsmanager.Application.Contracts.DTOs.User;

/// <summary>
///     Bruger oplysninger for registrering
/// </summary>
public record UserRegisterRequestDTO
{
    /// <summary>
    ///     Fulde navn
    /// </summary>
    public required string Name { get; init; }


    /// <summary>
    ///     Bruger Email 
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Bruger kodeord
    /// </summary>
    public required string Password { get; init; }
}