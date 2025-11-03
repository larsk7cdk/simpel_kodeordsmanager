using FluentValidation;

namespace SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;

public class ManagerReadRequestDTOValidator : AbstractValidator<ManagerReadRequestDTO>
{
    public ManagerReadRequestDTOValidator()
    {
    }
}