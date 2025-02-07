using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductsByCategory;

public record ListProductsByCategoryCommand : ApiQueryRequestApplication, IRequest<ListProductsByCategoryResult> 
{
    public string Category { get; set; } = string.Empty;
}