using Ambev.DeveloperEvaluation.Application.CartProducts.CreateCartProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.CreateCartProduct;

public class CreateCartProductProfile : Profile
{
    public CreateCartProductProfile()
    {        
        CreateMap<CreateCartProductRequest, CreateCartProductCommand>();
        CreateMap<CreateCartProductResult, CreateCartProductResponse>();
    }
}
