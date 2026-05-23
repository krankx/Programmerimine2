using FluentValidation;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class SaveKasutajaCommandValidator : AbstractValidator<SaveKasutajaCommand>
    {
        public SaveKasutajaCommandValidator()
        {
            RuleFor(x => x.Eesnimi)
                .NotEmpty().WithMessage("Eesnimi is required")
                .MaximumLength(50).WithMessage("Eesnimi cannot exceed 50 characters");

            RuleFor(x => x.Perekonnanimi)
                .NotEmpty().WithMessage("Perekonnanimi is required")
                .MaximumLength(50).WithMessage("Perekonnanimi cannot exceed 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.Parool)
                .NotEmpty().WithMessage("Parool is required")
                .MaximumLength(50).WithMessage("Parool cannot exceed 50 characters");
        }
    }
}
