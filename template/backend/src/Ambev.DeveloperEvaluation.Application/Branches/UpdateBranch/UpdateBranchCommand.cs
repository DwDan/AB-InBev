using Ambev.DeveloperEvaluation.Application.Branches.Common;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

public class UpdateBranchCommand : BranchApplication, IRequest<UpdateBranchResult>
{
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateBranchCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}