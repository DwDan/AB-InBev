using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
{
    public CreateCartCommandValidator()
    {
        RuleFor(cart => cart.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(cart => cart.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(cart => cart.Products)
            .NotEmpty().WithMessage("At least one product is required.")
            .Must(products => products.All(product => product.Quantity > 0))
            .WithMessage("Each product must have a quantity greater than zero.");

        RuleForEach(cart => cart.Products)
            .ChildRules(products =>
            {
                products.RuleFor(product => product.ProductId)
                    .GreaterThan(0).WithMessage("ProductId must be a positive integer.");

                products.RuleFor(product => product.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
            });
    }
}