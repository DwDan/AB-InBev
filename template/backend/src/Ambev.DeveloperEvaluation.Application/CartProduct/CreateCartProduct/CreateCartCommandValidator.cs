using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.CreateCartProduct;

public class CreateCartProductCommandValidator : AbstractValidator<CreateCartProductCommand>
{
    public CreateCartProductCommandValidator()
    {
        RuleFor(cartProduct => cartProduct.CartId)
            .NotEmpty().WithMessage("CartId is required.");

        RuleFor(cartProduct => cartProduct.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");
    }
}