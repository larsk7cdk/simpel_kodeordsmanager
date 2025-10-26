using SimpelKodeordsmanager.Domain.Entities;

namespace SimpelKodeordsmanager.Domain.Models;

public class Manager : BaseEntity
{
    public required string Name { get; init; }

    public required string Password { get; init; }

    public required int UserId { get; init; }

    public User? User { get; init; }
}