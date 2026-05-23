using FluentValidation;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class SaveKaaluMootmineCommandValidator : AbstractValidator<SaveKaaluMootmineCommand>
    {
        public SaveKaaluMootmineCommandValidator()
        {
            RuleFor(x => x.Kaal)
                .GreaterThan(0).WithMessage("Kaal must be greater than zero");

            RuleFor(x => x.PatsientId)
                .GreaterThan(0).WithMessage("PatsientId must be greater than zero");
        }
    }
}
