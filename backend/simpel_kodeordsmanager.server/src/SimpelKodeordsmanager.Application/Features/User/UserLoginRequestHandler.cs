using FluentValidation;
using Microsoft.Extensions.Logging;
using SimpelKodeordsmanager.Application.Contracts.DTOs.User;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Application.Contracts.Mappings;
using SimpelKodeordsmanager.Application.Exceptions;

namespace SimpelKodeordsmanager.Application.Features.User;

public class UserLoginRequestHandler(
    IValidator<UserLoginRequestDTO> validator,
    IUserRepository userRepository,
    ITokenService tokenService,
    IPasswordHasher passwordHasher,
    ILogger<UserLoginRequestHandler> logger
) : IRequestHandler<UserLoginRequestDTO, UserResponseDTO>
{
    public async Task<UserResponseDTO> InvokeAsync(UserLoginRequestDTO request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Login user with: {Email}", request.Email);

        // Validate request and throw exception if invalid
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        // Check if user exists
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            throw new BadRequestException($"User with {request.Email} dont exists.");

        // Check password

        var verifyPassword = passwordHasher.Verify(request.Password, user.Password);
        if (!verifyPassword)
            throw new BadRequestException($"User password is incorrect");

        // Create JWT Token
        var generatedToken = tokenService.Generate(request.Email);

        // Return User
        return request.MapToResponseDto(generatedToken.token, generatedToken.expiresIn);
    }
}