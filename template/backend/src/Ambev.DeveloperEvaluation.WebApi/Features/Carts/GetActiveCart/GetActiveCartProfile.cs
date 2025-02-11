using Ambev.DeveloperEvaluation.Application.Carts.GetActiveCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetActiveCart;

public class GetCartProfile : Profile
{
    public GetCartProfile()
    {
        CreateMap<GetActiveCartRequest, GetActiveCartCommand>();
        CreateMap<GetActiveCartResult, GetActiveCartResponse>();
    }
}
