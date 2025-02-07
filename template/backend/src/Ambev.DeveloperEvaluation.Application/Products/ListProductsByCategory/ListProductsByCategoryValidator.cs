using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductsByCategory;

public class ListProductsByCategoryValidator : AbstractValidator<ListProductsByCategoryCommand>
{
    public ListProductsByCategoryValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator());
    }
}
