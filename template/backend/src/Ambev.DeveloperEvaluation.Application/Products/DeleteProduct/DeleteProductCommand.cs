using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public record DeleteProductCommand : IRequest<DeleteProductResponse>
{
    public int Id { get; }

    public DeleteProductCommand(int id)
    {
        Id = id;
    }
}
