using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts;

public record ListCartsCommand : ApiQueryRequestApplication, IRequest<ListCartsResult> { }
