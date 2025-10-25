using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpelKodeordsmanager.Application.Contracts.DTOs.User;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Services;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Application.Features.User;
using SimpelKodeordsmanager.Application.Services;
using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration bindings
        services.Configure<JwtModel>(configuration.GetSection("Jwt"));

        // Add validation
        services
            .AddFluentValidationAutoValidation(cfg => { cfg.DisableDataAnnotationsValidation = true; })
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Service registrations
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();

        // User 
        services.AddScoped<IRequestHandler<UserRegisterRequestDTO, UserResponseDTO>, UserRegisterRequestHandler>();
        services.AddScoped<IRequestHandler<UserLoginRequestDTO, UserResponseDTO>, UserLoginRequestHandler>();

        return services;
    }
}