using Ambev.DeveloperEvaluation.Application.CartProducts.GetCartProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.GetCartProduct;

public class GetCartProductProfile : Profile
{
    public GetCartProductProfile()
    {
        CreateMap<GetCartProductRequest, GetCartProductCommand>();
        CreateMap<GetCartProductResult, GetCartProductResponse>();
    }
}
