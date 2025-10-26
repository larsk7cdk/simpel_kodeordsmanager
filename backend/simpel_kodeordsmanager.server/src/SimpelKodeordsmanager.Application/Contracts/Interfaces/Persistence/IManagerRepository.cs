using SimpelKodeordsmanager.Domain.Entities;

namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;

public interface IManagerRepository : IGenericRepository<ManagerEntity>
{
    Task<ManagerEntity?> GetByNameAsync(string name, int userId);

    Task<IReadOnlyList<ManagerEntity>?> GetByUserIdAsync(int userId);
}