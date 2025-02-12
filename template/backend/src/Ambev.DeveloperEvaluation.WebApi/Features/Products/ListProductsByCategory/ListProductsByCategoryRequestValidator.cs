using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProductsByCategory;

public class ListProductsByCategoryRequestValidator : AbstractValidator<ListProductsByCategoryRequest>
{
    public ListProductsByCategoryRequestValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator<Product>());
        RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page must be greater than 0");
        RuleFor(x => x.Size).GreaterThan(0).WithMessage("Page size must be greater than 0");
    }
}