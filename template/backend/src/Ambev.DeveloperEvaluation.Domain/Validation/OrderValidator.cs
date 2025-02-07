using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class OrderValidator : AbstractValidator<string>
{
    public OrderValidator()
    {
        When(order => !string.IsNullOrWhiteSpace(order), () =>
        {
            RuleFor(order => order!)
                .Must(IsValidOrderBy)
                .WithMessage("Invalid order format. Ensure correct column names and format (e.g., 'price desc, title asc').");
        });
    }

    private bool IsValidOrderBy(string order)
    {
        var validProperties = typeof(Product).GetProperties().Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var orderParams = order.Split(',');

        foreach (var param in orderParams)
        {
            var trimmedParam = param.Trim();
            var isDescending = trimmedParam.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);
            var propertyName = isDescending ? trimmedParam[..^5] : trimmedParam;

            if (!validProperties.Contains(propertyName))
            {
                return false;
            }
        }

        return true;
    }
}