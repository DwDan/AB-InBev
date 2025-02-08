using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

public record GetCartCommand : IRequest<GetCartResult>
{
    public int Id { get; }

    public GetCartCommand(int id)
    {
        Id = id;
    }
}
