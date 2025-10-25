using SimpelKodeordsmanager.Domain.Entities.Shared;

namespace SimpelKodeordsmanager.Domain.Entities;

public class ManagerEntity : BaseEntity
{
    public List<ManagerApplicationEntity> ManagerApplications { get; init; } = [];
}