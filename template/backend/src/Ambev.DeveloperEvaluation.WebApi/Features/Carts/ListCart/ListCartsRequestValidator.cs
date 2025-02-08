using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts;

public class ListCartsRequestValidator : AbstractValidator<ListCartsRequest>
{
    public ListCartsRequestValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator());
    }
}