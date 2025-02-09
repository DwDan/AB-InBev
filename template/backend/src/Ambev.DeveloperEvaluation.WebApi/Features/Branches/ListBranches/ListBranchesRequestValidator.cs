using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.ListBranches;

public class ListBranchesRequestValidator : AbstractValidator<ListBranchesRequest>
{
    public ListBranchesRequestValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator());
    }
}