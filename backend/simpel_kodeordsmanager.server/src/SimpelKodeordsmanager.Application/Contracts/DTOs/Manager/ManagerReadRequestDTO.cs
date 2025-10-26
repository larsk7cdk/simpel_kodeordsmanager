namespace SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;

/// <summary>
///     Hent brugers kodeord
/// </summary>
public class ManagerReadRequestDTO
{
    /// <summary>
    ///     Bruger ID
    /// </summary>
    public required int UserID { get; init; }
}