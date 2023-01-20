using FluentValidation;

namespace Business.Features.Orders.Commands.ConfirmOrder
{
    public class ConfirmOrderCommandValidator:AbstractValidator<ConfirmOrderCommand>
    {
        public ConfirmOrderCommandValidator()
        {
            RuleFor(c => c.OrderId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("Id must be greater than zero");
        }
    }
}
