using SimpelKodeordsmanager.Domain.Entities;

namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;

public interface IUserRepository : IGenericRepository<UserEntity>
{
    Task<UserEntity?> GetByEmailAsync(string email);
}