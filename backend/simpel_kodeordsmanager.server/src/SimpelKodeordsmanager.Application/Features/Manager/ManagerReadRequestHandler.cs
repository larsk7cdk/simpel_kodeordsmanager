using FluentValidation;
using Microsoft.Extensions.Logging;
using SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Application.Exceptions;

namespace SimpelKodeordsmanager.Application.Features.Manager;

public class ManagerReadRequestHandler(
    IValidator<ManagerReadRequestDTO> validator,
    ICurrentUserService currentUserService,
    IManagerRepository managerRepository,
    IPasswordCrypto passwordCrypto,
    ILogger<ManagerReadRequestHandler> logger
) : IRequestHandler<ManagerReadRequestDTO, IReadOnlyList<ManagerResponseDTO>>
{
    public async Task<IReadOnlyList<ManagerResponseDTO>> InvokeAsync(ManagerReadRequestDTO request, CancellationToken cancellationToken = default)
    {
        var userId = currentUserService.GetUserId()!;
        logger.LogInformation("Read all passwords for UserID: {User}", userId);

        // Validate request and throw exception if invalid
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        // Check if name of password exists
        var passwords = await managerRepository.GetByUserIdAsync(userId);

        if (passwords is null)
            throw new BadRequestException($"Passwords for {userId} dont exists.");

        var passwordList = passwords.Select(s => new ManagerResponseDTO
        {
            UserID = userId,
            Name = s.Name,
            Username = s.Username,
            Password = passwordCrypto.Decrypt(s.Password),
            EncryptedPassword = s.Password
        }).ToList();

        return passwordList;
    }
}