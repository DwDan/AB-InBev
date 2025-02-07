using Ambev.DeveloperEvaluation.WebApi.Features.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProductsByCategory;

public class ListProductsByCategoryRequest : ApiQueryRequestPresentation
{
    public string Category { get; set; } = string.Empty;
}
