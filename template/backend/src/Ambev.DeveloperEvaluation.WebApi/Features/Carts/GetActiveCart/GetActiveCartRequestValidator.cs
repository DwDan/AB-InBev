using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetActiveCart;

public class GetActiveCartRequestValidator : AbstractValidator<GetActiveCartRequest>
{
    public GetActiveCartRequestValidator() { }
}