using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.CreateCartProduct;

public class CreateCartProductProfile : Profile
{
    public CreateCartProductProfile()
    {
        CreateMap<CreateCartProductCommand, CartProduct>();
        CreateMap<CartProduct, CreateCartProductResult>();
    }
}
