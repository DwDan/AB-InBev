using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(product => product.Title).NotEmpty();
        RuleFor(product => product.Price).NotNull();
        RuleFor(product => product.Description).NotEmpty();
        RuleFor(product => product.Category).NotEmpty();
        RuleFor(product => product.Image).NotEmpty();
        RuleFor(product => product.Rating).NotNull();
    }
}