using FluentValidation;

namespace SimpelKodeordsmanager.Application.Contracts.DTOs.User;

public class UserRegisterRequestDTOValidator : AbstractValidator<UserRegisterRequestDTO>
{
    public UserRegisterRequestDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Navn må ikke være tom.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email må ikke være tom.")
            .EmailAddress().WithMessage("Email skal være en gyldig email-adresse.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Kodeord må ikke være tom.")
            .MinimumLength(4).WithMessage("Kodeord skal være mindst 4 tegn langt.");
        // .Matches("[A-Z]").WithMessage("Kodeord skal indeholde mindst ét stort bogstav.")
        // .Matches("[a-z]").WithMessage("Kodeord skal indeholde mindst ét lille bogstav.")
        // .Matches("[0-9]").WithMessage("Kodeord skal indeholde mindst ét tal.")
        // .Matches("[^a-zA-Z0-9]").WithMessage("Kodeord skal indeholde mindst ét specialtegn.");
    }
}