using FluentValidation;
using Microsoft.Extensions.Logging;
using SimpelKodeordsmanager.Application.Contracts.DTOs.User;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Application.Contracts.Mappings;
using SimpelKodeordsmanager.Application.Exceptions;
using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Application.Features.User;

public class UserRegisterRequestHandler(
    IValidator<UserRegisterRequestDTO> validator,
    IUserRepository userRepository,
    IUserRoleRepository userRoleRepository,
    ITokenService tokenService,
    IPasswordHasher passwordHasher,
    ILogger<UserRegisterRequestHandler> logger
) : IRequestHandler<UserRegisterRequestDTO, UserResponseDTO>
{
    public async Task<UserResponseDTO> InvokeAsync(UserRegisterRequestDTO request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Register new user with: {Email}", request.Email);

        // Validate request and throw exception if invalid
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        // Check if user exists
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user is not null)
            throw new BadRequestException($"User with {request.Email} already exists.");

        // Hash password
        var hashPassword = passwordHasher.Hash(request.Password);

        // Save user
        var createUser = new Domain.Models.User
        {
            Email = request.Email,
            Name = request.Name,
            Password = hashPassword
        };
        await userRepository.CreateAsync(createUser);

        // Add role to user
        var userRole = new UserRole
        {
            UserId = createUser.Id,
            RoleId = request.IsAdmin ? Role.AdminId : Role.MemberId
        };
        await userRoleRepository.CreateAsync(userRole);

        // // Create JWT Token
        var generatedToken = await tokenService.GenerateAsync(createUser.Id, request.Email);

        // Return user
        return request.MapToResponseDto(generatedToken.token, generatedToken.expiresIn);
    }
}