using FluentValidation;

namespace Business.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator:AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(c => c.Price).NotEmpty().GreaterThan(0).WithMessage("The price of the product must be greater than zero");
            RuleFor(c => c.Quantity).NotEmpty().GreaterThan(-1).WithMessage("Product quantity cannot be minimum negative");
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2).WithMessage("The name of the product must be a minimum of 2 characters");
        }
    }
}
