using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

public class ListSalesRequestValidator : AbstractValidator<ListSalesRequest>
{
    public ListSalesRequestValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator());
    }
}