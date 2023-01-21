using Business.Features.Purses.Commands.UpdatePurseMoney;
using FluentValidation;

namespace Business.Features.Purses.Commands.UpdatePurse
{
    public class UpdatePurseMoneyCommandValidator:AbstractValidator<UpdatePurseMoneyCommand>
    {
        public UpdatePurseMoneyCommandValidator()
        {
            RuleFor(c => c.Money)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("The amount of money must be more than zero");
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("Id must be greater than zero");
        }
    }
}
