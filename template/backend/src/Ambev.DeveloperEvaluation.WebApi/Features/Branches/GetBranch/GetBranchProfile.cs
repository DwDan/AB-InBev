using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;

public class GetBranchProfile : Profile
{
    public GetBranchProfile()
    {
        CreateMap<GetBranchRequest, GetBranchCommand>();
        CreateMap<GetBranchResult, GetBranchResponse>();
    }
}
