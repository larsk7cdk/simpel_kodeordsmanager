using Microsoft.EntityFrameworkCore;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Domain.Entities;
using SimpelKodeordsmanager.Persistence.DatabaseContext;

namespace SimpelKodeordsmanager.Persistence.Repositories.Shared;

public class GenericRepository<T>(AppDatabaseContext context) : IGenericRepository<T> where T : BaseEntity
{
    public async Task<IReadOnlyList<T>> GetAllAsync() => await context
        .Set<T>()
        .AsNoTracking()
        .ToListAsync();


    public async Task<T?> GetByIdAsync(int id) => await context
        .Set<T>()
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);


    public async Task<int> CreateAsync(T entity)
    {
        await context.AddAsync(entity);
        var id = await context.SaveChangesAsync();
        return id;
    }

    public async Task UpdateAsync(T entity)
    {
        // context.Update(entity);
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();
    }
}