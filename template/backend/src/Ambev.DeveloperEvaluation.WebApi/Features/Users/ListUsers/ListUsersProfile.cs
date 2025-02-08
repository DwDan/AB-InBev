using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

public class ListUsersProfile : Profile
{
    public ListUsersProfile()
    {
        CreateMap<UserApplication, UserPresentation>()
            .ReverseMap();

        CreateMap<ListUsersRequest, ListUsersCommand>();
        CreateMap<ListUsersResult, ListUsersResponse>();
    }
}
