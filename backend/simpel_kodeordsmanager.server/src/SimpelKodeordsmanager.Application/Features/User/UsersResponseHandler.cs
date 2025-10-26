using Microsoft.Extensions.Logging;
using SimpelKodeordsmanager.Application.Contracts.DTOs.User;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Persistence;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Application.Contracts.Mappings;

namespace SimpelKodeordsmanager.Application.Features.User;

public class UsersResponseHandler(
    IUserRepository userRepository,
    ILogger<UsersResponseHandler> logger
) : IResponseHandler<IReadOnlyList<UserDetailsResponseDTO>>
{
    public async Task<IReadOnlyList<UserDetailsResponseDTO>> InvokeAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("List alle brugere");

        // Get all users
        var users = await userRepository.GetAllAsync();

        // Return Users
        return users.MapToResponseDto();
    }
}