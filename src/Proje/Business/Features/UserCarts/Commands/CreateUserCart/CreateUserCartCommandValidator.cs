using FluentValidation;

namespace Business.Features.UserCarts.Commands.CreateUserCart
{
    public class CreateUserCartCommandValidator: AbstractValidator<CreateUserCartCommand>
    {
        public CreateUserCartCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("UserID must be greater than zero");
        }
    }
}
