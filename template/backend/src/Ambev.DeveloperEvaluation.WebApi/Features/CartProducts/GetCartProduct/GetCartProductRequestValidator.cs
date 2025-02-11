using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.GetCartProduct;

public class GetCartProductRequestValidator : AbstractValidator<GetCartProductRequest>
{
    public GetCartProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("CartProduct ID is required");
    }
}