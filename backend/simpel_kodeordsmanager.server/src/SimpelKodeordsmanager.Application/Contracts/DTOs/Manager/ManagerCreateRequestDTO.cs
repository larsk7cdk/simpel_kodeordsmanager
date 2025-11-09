namespace SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;

/// <summary>
///     Opret et nyt kodord
/// </summary>
public class
    ManagerCreateRequestDTO
{
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
}