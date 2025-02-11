using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.UpdateCartProduct;

public class UpdateCartProductRequestValidator : AbstractValidator<UpdateCartProductRequest>
{
    public UpdateCartProductRequestValidator()
    {
        RuleFor(cartProduct => cartProduct.CartId)
            .NotEmpty().WithMessage("CartId is required.");

        RuleFor(cartProduct => cartProduct.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");
    }
}