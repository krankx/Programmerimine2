using FluentValidation;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class SaveVererohuMootmineCommandValidator : AbstractValidator<SaveVererohuMootmineCommand>
    {
        public SaveVererohuMootmineCommandValidator()
        {
            RuleFor(x => x.Sustoolne)
                .GreaterThan(0).WithMessage("Sustoolne must be greater than zero");

            RuleFor(x => x.Diastoolne)
                .GreaterThan(0).WithMessage("Diastoolne must be greater than zero");

            RuleFor(x => x.Pulss)
                .GreaterThan(0).WithMessage("Pulss must be greater than zero");

            RuleFor(x => x.PatsientId)
                .GreaterThan(0).WithMessage("PatsientId must be greater than zero");
        }
    }
}
