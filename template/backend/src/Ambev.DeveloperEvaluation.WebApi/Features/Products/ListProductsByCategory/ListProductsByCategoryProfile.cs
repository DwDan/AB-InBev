using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Application.Products.ListProductsByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProductsByCategory;

public class ListProductsByCategoryProfile : Profile
{
    public ListProductsByCategoryProfile()
    {
        CreateMap<RatingApplication, RatingPresentation>()
            .ReverseMap();

        CreateMap<ProductApplication, ProductPresentation>()
            .ReverseMap();

        CreateMap<ListProductsByCategoryRequest, ListProductsByCategoryCommand>();
        CreateMap<ListProductsByCategoryResult, ListProductsByCategoryResponse>();
    }
}
