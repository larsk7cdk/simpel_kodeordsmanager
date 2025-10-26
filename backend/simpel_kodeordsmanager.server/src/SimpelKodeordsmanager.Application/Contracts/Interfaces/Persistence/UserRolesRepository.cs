using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;

public interface IUserRoleRepository : IGenericRepository<UserRole>
{
    Task<List<string>> GetRoleNamesByUserId(int userId);
}