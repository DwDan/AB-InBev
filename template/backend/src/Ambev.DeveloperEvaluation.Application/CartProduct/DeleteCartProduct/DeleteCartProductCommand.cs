using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.DeleteCartProduct;

public record DeleteCartProductCommand : IRequest<DeleteCartProductResponse>
{
    public int Id { get; }

    public DeleteCartProductCommand(int id)
    {
        Id = id;
    }
}
