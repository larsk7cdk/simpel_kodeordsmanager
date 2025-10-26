using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;
using SimpelKodeordsmanager.Application.Contracts.DTOs.User;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Application.Features.Manager;
using SimpelKodeordsmanager.Application.Features.User;
using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration bindings
        services.Configure<Jwt>(configuration.GetSection("Jwt"));

        // Add validation
        services
            .AddFluentValidationAutoValidation(cfg => { cfg.DisableDataAnnotationsValidation = true; })
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // User 
        services.AddScoped<IRequestHandler<UserRegisterRequestDTO, UserResponseDTO>, UserRegisterRequestHandler>();
        services.AddScoped<IRequestHandler<UserLoginRequestDTO, UserResponseDTO>, UserLoginRequestHandler>();
        services.AddScoped<IResponseHandler<IReadOnlyList<UserDetailsResponseDTO>>, UsersResponseHandler>();

        // Manager for passwords
        services.AddScoped<IRequestHandler<ManagerCreateRequestDTO>, ManagerCreateRequestHandler>();
        services.AddScoped<IRequestHandler<ManagerReadRequestDTO, IReadOnlyList<ManagerResponseDTO>>, ManagerReadRequestHandler>();

        return services;
    }
}