using FluentValidation;

namespace Business.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator:AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(c => c.Quantity)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("The desired amount of product must be more than zero.");
            RuleFor(c => c.UserCartId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("UserCartId must be greater than zero");
            RuleFor(c => c.ProductId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("ProductId must be greater than zero");
        }
    }
}
