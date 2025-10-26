namespace SimpelKodeordsmanager.Application.Contracts.DTOs.User;

/// <summary>
/// Oplysninger om bruger
/// </summary>
public record UserDetailsResponseDTO
{
    /// <summary>
    /// Bruger ID
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Bruger Email
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Bruger navn
    /// </summary>
    public required string Name { get; init; }
}