using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchCommandValidator()
    {
        RuleFor(branch => branch.Id).NotEmpty();
        RuleFor(branch => branch.Name).NotEmpty();
    }
}