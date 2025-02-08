using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts;

public class ListCartsValidator : AbstractValidator<ListCartsCommand>
{
    public ListCartsValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator());
    }
}
