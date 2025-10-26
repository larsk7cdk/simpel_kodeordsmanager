using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;

public interface IManagerRepository : IGenericRepository<Manager>
{
    Task<Manager?> GetByNameAsync(string name, int userId);

    Task<IReadOnlyList<Manager>?> GetByUserIdAsync(int userId);
}