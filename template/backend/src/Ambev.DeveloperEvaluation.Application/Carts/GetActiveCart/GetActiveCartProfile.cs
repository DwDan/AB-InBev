using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetActiveCart;

public class GetActiveCartProfile : Profile
{
    public GetActiveCartProfile()
    {
        CreateMap<Cart, GetActiveCartResult>();
    }
}
