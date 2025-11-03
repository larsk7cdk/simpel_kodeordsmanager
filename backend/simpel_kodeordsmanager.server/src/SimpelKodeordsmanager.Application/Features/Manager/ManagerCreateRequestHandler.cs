using FluentValidation;
using Microsoft.Extensions.Logging;
using SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Application.Exceptions;

namespace SimpelKodeordsmanager.Application.Features.Manager;

public class ManagerCreateRequestHandler(
    IValidator<ManagerCreateRequestDTO> validator,
    ICurrentUserService currentUserService,
    IManagerRepository managerRepository,
    IPasswordCrypto passwordCrypto,
    ILogger<ManagerCreateRequestHandler> logger
) : IRequestHandler<ManagerCreateRequestDTO>
{
    public async Task InvokeAsync(ManagerCreateRequestDTO request, CancellationToken cancellationToken = default)
    {
        var userId = currentUserService.GetUserId()!;

        logger.LogInformation("Create new password for UserID: {UserID}, with {Name}", userId, request.Name);

        // Validate request and throw exception if invalid
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        // Check if name of password exists
        var password = await managerRepository.GetByNameAsync(request.Name, userId);

        if (password != null)
            throw new BadRequestException($"Password with {request.Name} already exists.");

        // Encrypt password
        var encryptedPassword = passwordCrypto.Encrypt(request.Password);

        // Save user
        var createPassword = new Domain.Entities.ManagerEntity
        {
            Name = request.Name,
            Password = encryptedPassword,
            UserId = userId,
        };

        await managerRepository.CreateAsync(createPassword);
    }
}