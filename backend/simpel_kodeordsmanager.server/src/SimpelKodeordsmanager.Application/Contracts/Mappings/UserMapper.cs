using SimpelKodeordsmanager.Application.Contracts.DTOs.User;

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
}