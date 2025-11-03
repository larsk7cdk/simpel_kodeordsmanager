using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Domain.Models;
using SimpelKodeordsmanager.Infrastructure.Shared;

namespace SimpelKodeordsmanager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration bindings
        services.Configure<Crypto>(configuration.GetSection("Crypto"));
        
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IPasswordCrypto, PasswordCrypto>();

        return services;
    }
}