using Microsoft.EntityFrameworkCore;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Domain.Entities;
using SimpelKodeordsmanager.Persistence.DatabaseContext;
using SimpelKodeordsmanager.Persistence.Repositories.Shared;

namespace SimpelKodeordsmanager.Persistence.Repositories;

public class UserRepository(AppDatabaseContext context)
    : GenericRepository<UserEntity>(context), IUserRepository
{
    private readonly AppDatabaseContext _context = context;

    public async Task<UserEntity?> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
}