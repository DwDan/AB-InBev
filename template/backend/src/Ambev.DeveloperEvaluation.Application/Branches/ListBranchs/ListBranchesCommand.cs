using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.ListBranches;

public record ListBranchesCommand : ApiQueryRequestApplication, IRequest<ListBranchesResult> { }
