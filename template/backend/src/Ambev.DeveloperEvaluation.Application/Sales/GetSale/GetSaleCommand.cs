using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public record GetSaleCommand : IRequest<GetSaleResult>
{
    public int Id { get; }

    public GetSaleCommand(int id)
    {
        Id = id;
    }
}
