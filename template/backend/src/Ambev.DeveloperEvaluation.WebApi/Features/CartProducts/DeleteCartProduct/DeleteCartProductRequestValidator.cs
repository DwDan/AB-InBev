using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.DeleteCartProduct;

public class DeleteCartProductRequestValidator : AbstractValidator<DeleteCartProductRequest>
{
    public DeleteCartProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("CartProduct ID is required");
    }
}
