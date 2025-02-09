using Ambev.DeveloperEvaluation.Application.Branches.Common;
using Ambev.DeveloperEvaluation.Application.Branches.ListBranches;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.ListBranches;

public class ListBranchesProfile : Profile
{
    public ListBranchesProfile()
    {
        CreateMap<BranchApplication, BranchPresentation>()
            .ReverseMap();

        CreateMap<ListBranchesRequest, ListBranchesCommand>();
        CreateMap<ListBranchesResult, ListBranchesResponse>();
    }
}
