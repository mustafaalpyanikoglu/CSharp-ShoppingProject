using Business.Features.UserCarts.Commands.CreateUserCart;
using FluentValidation;

namespace Business.Features.UserCarts.Commands.UpdateUserCart
{
    public class UpdateUserCartCommandValidator : AbstractValidator<UpdateUserCartCommand>
    {
        public UpdateUserCartCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("UserID must be greater than zero");
            RuleFor(c => c.UserId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("UserID must be greater than zero");
        }
    }
}
