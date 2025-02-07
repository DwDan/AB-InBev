using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsProfile : Profile
{
    public ListProductsProfile()
    {
        CreateMap<RatingApplication, Rating>()
            .ReverseMap();

        CreateMap<Product, ProductApplication>()
           .ReverseMap();

        CreateMap<ListProductsCommand, ApiQueryRequestDomain>();

        CreateMap<ApiQueryResponseDomain<Product>, ListProductsResult>();
    }
}
