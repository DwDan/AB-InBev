using Ambev.DeveloperEvaluation.Application.CartProducts.UpdateCartProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.UpdateCartProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.UpdateCart;

public class UpdateCartProductProfile : Profile
{
    public UpdateCartProductProfile()
    {
        CreateMap<UpdateCartProductRequest, UpdateCartProductCommand>();
        CreateMap<UpdateCartProductResult, UpdateCartProductResponse>();
    }
}
