using FluentValidation;
using Microsoft.Extensions.Logging;
using SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Application.Exceptions;
using SimpelKodeordsmanager.Domain.Entities;

namespace SimpelKodeordsmanager.Application.Features.Manager;

public class ManagerCreateRequestHandler(
    IValidator<ManagerCreateRequestDTO> validator,
    IManagerRepository managerRepository,
    IPasswordCrypto passwordCrypto,
    ILogger<ManagerCreateRequestHandler> logger
) : IRequestHandler<ManagerCreateRequestDTO>
{
    public async Task InvokeAsync(ManagerCreateRequestDTO request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Create new password for: {Name}", request.Name);

        // Validate request and throw exception if invalid
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        // Check if name of password exists
        var password = await managerRepository.GetByNameAsync(request.Name, request.UserID);

        if (password != null)
            throw new BadRequestException($"Password with {request.Name} already exists.");

        // Encrypt password
        var encryptedPassword = passwordCrypto.Encrypt(request.Password);

        // Save user
        var createPassword = new ManagerEntity
        {
            Name = request.Name,
            Password = encryptedPassword,
            UserId = request.UserID,
        };

        await managerRepository.CreateAsync(createPassword);
    }
}