using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListProductsProfile : Profile
{
    public ListProductsProfile()
    {
        CreateMap<RatingApplication, RatingPresentation>()
            .ReverseMap();

        CreateMap<ProductApplication, ProductPresentation>()
            .ReverseMap();

        CreateMap<ListProductsRequest, ListProductsCommand>();
        CreateMap<ListProductsResult, ListProductsResponse>();
    }
}
