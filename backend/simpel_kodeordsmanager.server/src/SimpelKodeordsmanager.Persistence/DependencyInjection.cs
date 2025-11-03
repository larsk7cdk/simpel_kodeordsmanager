using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Persistence.DatabaseContext;
using SimpelKodeordsmanager.Persistence.Repositories;
using SimpelKodeordsmanager.Persistence.Repositories.Shared;

namespace SimpelKodeordsmanager.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AppDatabaseConnectionString"));
        });

        // Service registrations
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IManagerRepository, ManagerRepository>();

        return services;
    }
}