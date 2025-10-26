using SimpelKodeordsmanager.Domain.Entities.Shared;

namespace SimpelKodeordsmanager.Domain.Entities;

public class UserEntity : BaseEntity
{
    public required string Email { get; init; }
    
    public required string Name { get; init; }
    public required string Password { get; init; }
}