using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.UpdateCartProduct;

public class UpdateCartProductCommand : CartProductApplication, IRequest<UpdateCartProductResult>
{
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateCartProductCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}