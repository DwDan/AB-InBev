using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.GetCartProduct;

public class GetCartProductValidator : AbstractValidator<GetCartProductCommand>
{
    public GetCartProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("CartProduct ID is required");
    }
}
