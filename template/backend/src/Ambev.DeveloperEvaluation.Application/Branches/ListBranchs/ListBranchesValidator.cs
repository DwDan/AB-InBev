using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.ListBranches;

public class ListBranchesValidator : AbstractValidator<ListBranchesCommand>
{
    public ListBranchesValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator());
    }
}
