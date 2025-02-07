using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public record ListProductsCommand : ApiQueryRequestApplication, IRequest<ListProductsResult> { }
