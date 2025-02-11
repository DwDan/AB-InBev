using Ambev.DeveloperEvaluation.Application.CartProducts.DeleteCartProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.DeleteCartProduct;

public class DeleteCartProductProfile : Profile
{
    public DeleteCartProductProfile()
    {
        CreateMap<int, DeleteCartProductCommand>()
            .ConstructUsing(id => new DeleteCartProductCommand(id));
    }
}
