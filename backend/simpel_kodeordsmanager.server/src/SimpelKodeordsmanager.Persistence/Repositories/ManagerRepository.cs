using Microsoft.EntityFrameworkCore;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Persistence.DatabaseContext;
using SimpelKodeordsmanager.Persistence.Repositories.Shared;
using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Persistence.Repositories;

public class ManagerRepository(AppDatabaseContext context)
    : GenericRepository<Manager>(context), IManagerRepository
{
    private readonly AppDatabaseContext _context = context;

    public async Task<Manager?> GetByNameAsync(string name, int userId) =>
        await _context.Managers
            .FirstOrDefaultAsync(x => x.Name.Equals(name) && x.UserId.Equals(userId));

    public async Task<IReadOnlyList<Manager>?> GetByUserIdAsync(int userId) =>
        await _context.Managers
            .Where(x => x.UserId.Equals(userId))
            .ToListAsync();
}