using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.GetCartProduct;

public record GetCartProductCommand : IRequest<GetCartProductResult>
{
    public int Id { get; }

    public GetCartProductCommand(int id)
    {
        Id = id;
    }
}
