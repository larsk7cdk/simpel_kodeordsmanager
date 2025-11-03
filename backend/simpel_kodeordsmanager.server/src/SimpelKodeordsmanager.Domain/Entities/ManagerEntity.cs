namespace SimpelKodeordsmanager.Domain.Entities;

public class ManagerEntity : BaseEntity
{
    public required string Name { get; init; }

    public required string Password { get; init; }

    public required string UserId { get; init; }
}