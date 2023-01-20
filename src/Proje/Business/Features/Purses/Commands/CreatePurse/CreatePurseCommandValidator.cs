using FluentValidation;

namespace Business.Features.Purses.Commands.CreatePurse
{
    public class CreatePurseCommandValidator:AbstractValidator<CreatePurseCommand>
    {
        public CreatePurseCommandValidator()
        {
            RuleFor(c => c.Money)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("The amount of money must be more than zero");
            RuleFor(c => c.UserId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("UserID must be greater than zero");
        }
    }
}
