using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUsersValidator : AbstractValidator<ListUsersCommand>
{
    public ListUsersValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator());
    }
}
