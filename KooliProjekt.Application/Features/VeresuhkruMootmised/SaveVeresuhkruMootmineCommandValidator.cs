using FluentValidation;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class SaveVeresuhkruMootmineCommandValidator : AbstractValidator<SaveVeresuhkruMootmineCommand>
    {
        public SaveVeresuhkruMootmineCommandValidator()
        {
            RuleFor(x => x.Veresuhkur)
                .GreaterThan(0).WithMessage("Veresuhkur must be greater than zero");

            RuleFor(x => x.PatsientId)
                .GreaterThan(0).WithMessage("PatsientId must be greater than zero");
        }
    }
}
