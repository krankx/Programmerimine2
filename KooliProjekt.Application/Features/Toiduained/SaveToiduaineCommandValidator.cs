using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class SaveToiduaineCommandValidator : AbstractValidator<SaveToiduaineCommand>
    {
        public SaveToiduaineCommandValidator(ApplicationDbContext context)
        {
            RuleFor(x => x.Nimetus)
                .NotEmpty().WithMessage("Nimetus is required")
                .MaximumLength(100).WithMessage("Nimetus cannot exceed 100 characters");

            RuleFor(x => x.Energia)
                .GreaterThanOrEqualTo(0).WithMessage("Energia cannot be negative");
        }
    }
}
