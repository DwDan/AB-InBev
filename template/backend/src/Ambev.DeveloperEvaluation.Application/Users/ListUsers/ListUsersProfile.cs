using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUsersProfile : Profile
{
    public ListUsersProfile()
    {
        CreateMap<User, UserApplication>()
           .ReverseMap();

        CreateMap<ListUsersCommand, ApiQueryRequestDomain>();

        CreateMap<ApiQueryResponseDomain<User>, ListUsersResult>();
    }
}
