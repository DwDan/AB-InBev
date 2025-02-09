using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public record ListSalesCommand : ApiQueryRequestApplication, IRequest<ListSalesResult> { }
