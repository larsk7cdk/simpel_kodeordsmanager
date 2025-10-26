namespace SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;

/// <summary>
///     Opret et nyt kodord
/// </summary>
public class ManagerCreateRequestDTO
{
    /// <summary>
    ///     Bruger ID
    /// </summary>
    public required int UserID { get; init; }

    /// <summary>
    /// Navn på kodeord
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Kodeord
    /// </summary>
    public required string Password { get; init; }
}