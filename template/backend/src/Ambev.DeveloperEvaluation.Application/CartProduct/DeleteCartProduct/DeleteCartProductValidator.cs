using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.DeleteCartProduct;

public class DeleteCartProductValidator : AbstractValidator<DeleteCartProductCommand>
{
    public DeleteCartProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("CartProduct ID is required");
    }
}
