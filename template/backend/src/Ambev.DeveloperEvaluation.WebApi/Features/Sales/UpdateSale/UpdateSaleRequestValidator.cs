using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(sale => sale.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(sale => sale.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(sale => sale.Products)
            .NotEmpty().WithMessage("At least one product is required.")
            .Must(products => products.All(product => product.Quantity > 0))
            .WithMessage("Each product must have a quantity greater than zero.");

        RuleForEach(sale => sale.Products)
            .ChildRules(products =>
            {
                products.RuleFor(product => product.ProductId)
                    .GreaterThan(0).WithMessage("ProductId must be a positive integer.");

                products.RuleFor(product => product.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
            });
    }
}