using FluentValidation;

namespace Business.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(c=> c.Price).NotEmpty().GreaterThan(0).WithMessage("The price of the product must be greater than zero");
            RuleFor(c => c.Quantity).NotEmpty().GreaterThan(0).WithMessage("The quantity of the product must be more than zero");
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2).WithMessage("The name of the product must be a minimum of 2 characters");
        }
    }
}
