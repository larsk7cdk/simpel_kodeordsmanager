namespace SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;

public class ManagerResponseDTO
{
    /// <summary>
    ///     Bruger ID
    /// </summary>
    public required string UserID { get; init; }

    /// <summary>
    /// Navn på kodeord
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Brugernavn
    /// </summary>
    public required string Username { get; init; }
    /// <summary>
    /// Kodeord
    /// </summary>
    public required string Password { get; init; }
    
    /// <summary>
    /// Krypteret kodeord
    /// </summary>
    public required string EncryptedPassword { get; init; }
}