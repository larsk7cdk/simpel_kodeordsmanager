using SimpelKodeordsmanager.Domain.Entities;
using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;

public interface IManagerRepository : IGenericRepository<ManagerEntity>
{
    Task<ManagerEntity?> GetByNameAsync(string name, string userId);

    Task<IReadOnlyList<ManagerEntity>?> GetByUserIdAsync(string userId);
}