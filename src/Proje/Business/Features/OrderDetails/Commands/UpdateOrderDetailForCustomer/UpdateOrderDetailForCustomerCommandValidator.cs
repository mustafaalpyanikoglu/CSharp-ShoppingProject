using FluentValidation;

namespace Business.Features.OrderDetails.Commands.UpdateOrderDetailForCustomer
{
    public class UpdateOrderDetailForCustomerCommandValidator:AbstractValidator<UpdateOrderDetailForCustomerCommand>
    {
        public UpdateOrderDetailForCustomerCommandValidator()
        {
            RuleFor(c => c.Quantity)
                .NotNull()
                .GreaterThan(-1).WithMessage("the amount to be updated must be at least zero");
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("OrderId must be greater than zero");
        }
    }
}
