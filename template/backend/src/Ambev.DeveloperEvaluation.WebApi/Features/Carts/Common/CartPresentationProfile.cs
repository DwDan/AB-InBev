using Ambev.DeveloperEvaluation.Application.Carts.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Common
{
    public class CartPresentationProfile : Profile
    {
        public CartPresentationProfile()
        {
            CreateMap<CartProductPresentation, CartProductApplication>()
                .ReverseMap();

            CreateMap<CartPresentation, CartApplication>()
                .ReverseMap();
        }
    }
}
