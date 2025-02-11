using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.UpdateCartProduct;

public class UpdateCartProductProfile : Profile
{
    public UpdateCartProductProfile()
    {
        CreateMap<UpdateCartProductCommand, CartProduct>();
        CreateMap<CartProduct, UpdateCartProductResult>();
    }
}
