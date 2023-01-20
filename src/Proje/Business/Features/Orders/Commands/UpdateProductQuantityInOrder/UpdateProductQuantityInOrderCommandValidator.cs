using FluentValidation;

namespace Business.Features.Orders.Commands.UpdateProductQuantityInOrder
{
    public class UpdateProductQuantityInOrderCommandValidator:AbstractValidator<UpdateProductQuantityInOrderCommand>
    {
        public UpdateProductQuantityInOrderCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("Id must be greater than zero");
            RuleFor(c => c.Quantity)
                .GreaterThan(-1).WithMessage("The desired amount of product must be more than zero.");
        }
    }
}
