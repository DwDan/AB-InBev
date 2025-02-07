using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsValidator : AbstractValidator<ListProductsCommand>
{
    public ListProductsValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator());
    }
}
