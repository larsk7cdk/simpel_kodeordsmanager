using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration bindings
        services.Configure<JwtModel>(configuration.GetSection("Jwt"));

        // Service registrations

        return services;
    }
}