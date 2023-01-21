using FluentValidation;

namespace Business.Features.OrderDetails.Commands.CreateOrder
{
    public class CreateOrderDetailCommandValidator:AbstractValidator<CreateOrderDetailCommand>
    {
        public CreateOrderDetailCommandValidator()
        {
            RuleFor(c => c.Quantity)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("The desired amount of product must be more than zero.");
            RuleFor(c => c.ProductId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("ProductId must be greater than zero");
            RuleFor(c => c.OrderId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("OrderId must be greater than zero");
        }
    }
}
