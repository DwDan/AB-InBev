using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the <see cref="OrderValidator{T}"/> class.
/// </summary>
[Trait("Category", "Unit")]
public class OrderValidatorTests
{
    private readonly OrderValidator<Product> _validator;

    public OrderValidatorTests()
    {
        _validator = new OrderValidator<Product>();
    }

    [Fact(DisplayName = "Given a valid order format When validated Then should pass validation")]
    public void Given_ValidOrderFormat_When_Validated_Then_ShouldNotHaveErrors()
    {
        var order = "Price desc, Title asc";

        var result = _validator.TestValidate(order);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Given an empty order When validated Then should pass validation")]
    public void Given_EmptyOrder_When_Validated_Then_ShouldNotHaveErrors()
    {
        var order = string.Empty;

        var result = _validator.TestValidate(order);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Given an invalid order format When validated Then should have error")]
    public void Given_InvalidOrderFormat_When_Validated_Then_ShouldHaveError()
    {
        var order = "InvalidProperty desc";

        var result = _validator.TestValidate(order);

        result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage($"Format order '{order}' is not valid. Ensure correct column names and format (e.g., 'price desc, title asc').");
    }
}
