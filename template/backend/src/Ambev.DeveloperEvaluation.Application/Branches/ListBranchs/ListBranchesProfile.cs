using Ambev.DeveloperEvaluation.Application.Branches.Common;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branches.ListBranches;

public class ListBranchesProfile : Profile
{
    public ListBranchesProfile()
    {
        CreateMap<Branch, BranchApplication>()
           .ReverseMap();

        CreateMap<ListBranchesCommand, ApiQueryRequestDomain>();

        CreateMap<ApiQueryResponseDomain<Branch>, ListBranchesResult>();
    }
}
