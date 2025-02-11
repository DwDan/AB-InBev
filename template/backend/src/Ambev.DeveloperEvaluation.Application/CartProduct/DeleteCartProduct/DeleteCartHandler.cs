using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.DeleteCartProduct;

public class DeleteCartProductHandler : IRequestHandler<DeleteCartProductCommand, DeleteCartProductResponse>
{
    private readonly ICartProductRepository _cartProductRepository;

    public DeleteCartProductHandler(
        ICartProductRepository cartProductRepository)
    {
        _cartProductRepository = cartProductRepository;
    }

    public async Task<DeleteCartProductResponse> Handle(DeleteCartProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCartProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _cartProductRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"CartProduct with ID {request.Id} not found");

        return new DeleteCartProductResponse { Success = true };
    }
}
