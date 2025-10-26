using FluentValidation;
using Microsoft.Extensions.Logging;
using SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Application.Exceptions;
using SimpelKodeordsmanager.Domain.Entities;

namespace SimpelKodeordsmanager.Application.Features.Manager;

public class ManagerReadRequestHandler(
    IValidator<ManagerReadRequestDTO> validator,
    IManagerRepository managerRepository,
    IPasswordCrypto passwordCrypto,
    ILogger<ManagerReadRequestHandler> logger
) : IRequestHandler<ManagerReadRequestDTO, IReadOnlyList<ManagerResponseDTO>>
{
    public async Task<IReadOnlyList<ManagerResponseDTO>> InvokeAsync(ManagerReadRequestDTO request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Read all passwords for: {User}", request.UserID);

        // Validate request and throw exception if invalid
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        // Check if name of password exists
        var passwords = await managerRepository.GetByUserIdAsync(request.UserID);

        if (passwords is null)
            throw new BadRequestException($"Passwords for {request.UserID} dont exists.");

        var passwordList = passwords.Select(s => new ManagerResponseDTO
        {
            UserID = s.UserId,
            Name = s.Name,
            Password = passwordCrypto.Decrypt(s.Password),
            EncryptedPassword = s.Password
        }).ToList();

        return passwordList;
    }
}