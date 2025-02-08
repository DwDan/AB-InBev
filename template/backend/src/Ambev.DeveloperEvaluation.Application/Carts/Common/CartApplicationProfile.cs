using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.Common
{
    public class CartApplicationProfile : Profile
    {
        public CartApplicationProfile()
        {
            CreateMap<CartProductApplication, CartProduct>()
                .ReverseMap();

            CreateMap<Cart, CartApplication>()
               .ReverseMap();
        }
    }
}
