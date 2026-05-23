using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class SaveSoogikorraRidaCommandValidator : AbstractValidator<SaveSoogikorraRidaCommand>
    {
        public SaveSoogikorraRidaCommandValidator(ApplicationDbContext context)
        {
            RuleFor(x => x.Kogus)
                .GreaterThan(0).WithMessage("Kogus must be greater than zero");

            RuleFor(x => x.SoogikordId)
                .GreaterThan(0).WithMessage("SoogikordId must be greater than zero");

            RuleFor(x => x.ToiduaineId)
                .GreaterThan(0).WithMessage("ToiduaineId must be greater than zero");
        }
    }
}
