using Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.DeleteBranch;

public class DeleteBranchProfile : Profile
{
    public DeleteBranchProfile()
    {
        CreateMap<int, DeleteBranchCommand>()
            .ConstructUsing(id => new DeleteBranchCommand(id));
    }
}
