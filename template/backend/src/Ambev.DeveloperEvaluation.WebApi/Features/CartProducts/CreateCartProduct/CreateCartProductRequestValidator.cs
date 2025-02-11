using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.CreateCartProduct;

public class CreateCartProductRequestValidator : AbstractValidator<CreateCartProductRequest>
{
    public CreateCartProductRequestValidator()
    {
        RuleFor(cartProduct => cartProduct.CartId)
            .NotEmpty().WithMessage("CartId is required.");

        RuleFor(cartProduct => cartProduct.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");
    }
}