using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductsByCategory;

public class ListProductsByCategoryProfile : Profile
{
    public ListProductsByCategoryProfile()
    {
        CreateMap<RatingApplication, Rating>()
            .ReverseMap();

        CreateMap<Product, ProductApplication>()
           .ReverseMap();

        CreateMap<ListProductsByCategoryCommand, ApiQueryRequestDomain>();

        CreateMap<ApiQueryResponseDomain<Product>, ListProductsByCategoryResult>();
    }
}
