using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;

public record DeleteBranchCommand : IRequest<DeleteBranchResponse>
{
    public int Id { get; }

    public DeleteBranchCommand(int id)
    {
        Id = id;
    }
}
