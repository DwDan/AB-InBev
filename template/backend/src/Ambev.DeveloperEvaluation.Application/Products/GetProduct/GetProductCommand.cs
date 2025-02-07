using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public record GetProductCommand : IRequest<GetProductResult>
{
    public int Id { get; }

    public GetProductCommand(int id)
    {
        Id = id;
    }
}
