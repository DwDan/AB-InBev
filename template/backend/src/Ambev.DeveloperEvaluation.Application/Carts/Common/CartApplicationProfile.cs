using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.Common
{
    public class CartApplicationProfile : Profile
    {
        public CartApplicationProfile()
        {
            CreateMap<GetCartProductApplication, CartProduct>()
                .ReverseMap();

            CreateMap<CartProductApplication, CartProduct>()
                .ReverseMap();

            CreateMap<Cart, CartApplication>()
               .ReverseMap();
        }
    }
}
