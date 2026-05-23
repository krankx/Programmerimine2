using FluentValidation;

namespace KooliProjekt.Application.Features.Patsiendid
{
    public class SavePatsientCommandValidator : AbstractValidator<SavePatsientCommand>
    {
        public SavePatsientCommandValidator()
        {
            RuleFor(x => x.Eesnimi)
                .NotEmpty().WithMessage("Eesnimi is required")
                .MaximumLength(50).WithMessage("Eesnimi cannot exceed 50 characters");

            RuleFor(x => x.Perekonnanimi)
                .NotEmpty().WithMessage("Perekonnanimi is required")
                .MaximumLength(50).WithMessage("Perekonnanimi cannot exceed 50 characters");

            RuleFor(x => x.Isikukood)
                .NotEmpty().WithMessage("Isikukood is required")
                .Length(11).WithMessage("Isikukood must be exactly 11 characters");

            RuleFor(x => x.KasutajaId)
                .GreaterThan(0).WithMessage("KasutajaId must be greater than zero");
        }
    }
}
