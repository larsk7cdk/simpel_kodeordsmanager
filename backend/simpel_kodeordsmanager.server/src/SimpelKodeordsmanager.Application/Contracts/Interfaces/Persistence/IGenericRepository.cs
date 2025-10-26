using SimpelKodeordsmanager.Domain.Entities;

namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task<int> CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}