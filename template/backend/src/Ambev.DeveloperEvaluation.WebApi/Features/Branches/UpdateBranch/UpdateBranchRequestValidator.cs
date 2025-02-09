using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;

public class UpdateBranchRequestValidator : AbstractValidator<UpdateBranchRequest>
{
    public UpdateBranchRequestValidator()
    {
        RuleFor(branch => branch.Id).NotEmpty();
        RuleFor(branch => branch.Name).NotEmpty();
    }
}