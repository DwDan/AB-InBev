using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.GetCartProduct;

public class GetCartProductProfile : Profile
{
    public GetCartProductProfile()
    {
        CreateMap<CartProduct, GetCartProductResult>();
    }
}
