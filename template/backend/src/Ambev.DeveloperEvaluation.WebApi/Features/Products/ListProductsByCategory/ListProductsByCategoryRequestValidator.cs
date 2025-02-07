using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProductsByCategory;

public class ListProductsByCategoryRequestValidator : AbstractValidator<ListProductsByCategoryRequest>
{
    public ListProductsByCategoryRequestValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator());
    }
}