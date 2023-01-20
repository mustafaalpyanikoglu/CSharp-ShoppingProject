using Business.Features.Purses.Commands.DeletePurse;
using FluentValidation;

namespace Business.Features.Purses.Commands.UpdatePurse
{
    public class UpdatePurseCommandValidator:AbstractValidator<UpdatePurseCommand>
    {
        public UpdatePurseCommandValidator()
        {
            RuleFor(c => c.Money)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("The amount of money must be more than zero");
            RuleFor(c => c.UserId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("UserID must be greater than zero");
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("Id must be greater than zero");
        }
    }
}
