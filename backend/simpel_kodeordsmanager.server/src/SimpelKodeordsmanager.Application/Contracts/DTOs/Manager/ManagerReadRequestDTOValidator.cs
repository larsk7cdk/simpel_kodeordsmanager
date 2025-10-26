using FluentValidation;

namespace SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;

public class ManagerReadRequestDTOValidator : AbstractValidator<ManagerReadRequestDTO>
{
    public ManagerReadRequestDTOValidator()
    {
        RuleFor(x => x.UserID)
            .NotEmpty().WithMessage("Bruger ID må ikke være tom.");
    }
}