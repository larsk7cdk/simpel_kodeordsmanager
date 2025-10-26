using Microsoft.EntityFrameworkCore;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Domain.Models;
using SimpelKodeordsmanager.Persistence.DatabaseContext;
using SimpelKodeordsmanager.Persistence.Repositories.Shared;

namespace SimpelKodeordsmanager.Persistence.Repositories;

public class UserRoleRepository(AppDatabaseContext context)
    : GenericRepository<UserRole>(context), IUserRoleRepository
{
    private readonly AppDatabaseContext _context = context;

    public async Task<List<string>> GetRoleNamesByUserId(int userId) =>
        await _context.UserRole
            .Where(x => x.UserId.Equals(userId))
            .Select(x => x.Role.Name)
            .ToListAsync();
}