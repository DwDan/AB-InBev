using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListCategories;

public class ListCategoriesRequestValidator : AbstractValidator<ListCategoriesRequest>
{
    public ListCategoriesRequestValidator() { }
}