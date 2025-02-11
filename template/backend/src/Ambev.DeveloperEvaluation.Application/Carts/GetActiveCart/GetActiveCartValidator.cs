using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetActiveCart;

public class GetActiveCartValidator : AbstractValidator<GetActiveCartCommand>
{
    public GetActiveCartValidator()
    {
    }
}
