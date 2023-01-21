using FluentValidation;

namespace Business.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(c => c.UserCartId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("UserCartId must be greater than zero");
            RuleFor(c => c.OrderNumber)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6).MaximumLength(6).WithMessage("order number must be six characters");
        }
    }
}
