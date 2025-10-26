using SimpelKodeordsmanager.Application.Contracts.DTOs.User;
using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.Application.Contracts.Mappings;

public static class UserMapper
{
    public static UserResponseDTO MapToResponseDto(this UserRegisterRequestDTO dto, string token, int expiresIn) =>
        new()
        {
            Email = dto.Email,
            Token = token,
            ExpiresIn = expiresIn,
        };

    public static UserResponseDTO MapToResponseDto(this UserLoginRequestDTO dto, string token, int expiresIn) =>
        new()
        {
            Email = dto.Email,
            Token = token,
            ExpiresIn = expiresIn,
        };
    
    public static IReadOnlyList<UserDetailsResponseDTO> MapToResponseDto(this IReadOnlyList<User> entity)
    {
        var users = entity.Select(s => new UserDetailsResponseDTO
        {
            Id = s.Id,
            Email = s.Email,
            Name = s.Name
        });

        return users.ToList();
    }
}