namespace SimpelKodeordsmanager.Domain.Entities;

public abstract class ManagerApplicationEntity
{
    public required int UserId { get; init; }
    public required string Name { get; init; }
    public required string Password { get; init; }
}