using FluentValidation;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class SaveSoogikordCommandValidator : AbstractValidator<SaveSoogikordCommand>
    {
        public SaveSoogikordCommandValidator()
        {
            RuleFor(x => x.PatsientId)
                .GreaterThan(0).WithMessage("PatsientId must be greater than zero");
        }
    }
}
