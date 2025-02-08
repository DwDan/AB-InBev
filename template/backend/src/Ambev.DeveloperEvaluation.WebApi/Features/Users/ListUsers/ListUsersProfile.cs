using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
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
