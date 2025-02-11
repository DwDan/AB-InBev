using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.UpdateCartProduct;

public class UpdateCartProductCommandValidator : AbstractValidator<UpdateCartProductCommand>
{
    public UpdateCartProductCommandValidator()
    {
        RuleFor(cartProduct => cartProduct.CartId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(cartProduct => cartProduct.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");
    }
}