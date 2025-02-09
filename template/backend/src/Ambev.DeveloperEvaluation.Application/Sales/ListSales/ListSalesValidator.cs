using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesValidator : AbstractValidator<ListSalesCommand>
{
    public ListSalesValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator());
    }
}
