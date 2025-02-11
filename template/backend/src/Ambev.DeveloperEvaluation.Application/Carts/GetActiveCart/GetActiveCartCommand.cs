using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetActiveCart;

public record GetActiveCartCommand : IRequest<GetActiveCartResult>
{
    public GetActiveCartCommand() { }
}
