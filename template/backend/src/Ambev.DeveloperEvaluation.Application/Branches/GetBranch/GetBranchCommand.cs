using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

public record GetBranchCommand : IRequest<GetBranchResult>
{
    public int Id { get; }

    public GetBranchCommand(int id)
    {
        Id = id;
    }
}
