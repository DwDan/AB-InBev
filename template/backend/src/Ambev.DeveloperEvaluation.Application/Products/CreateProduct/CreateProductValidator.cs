using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.Title).NotEmpty();
        RuleFor(product => product.Price).NotNull();
        RuleFor(product => product.Description).NotEmpty();
        RuleFor(product => product.Category).NotEmpty();
        RuleFor(product => product.Image).NotEmpty();
        RuleFor(product => product.Rating).NotNull();
    }
}