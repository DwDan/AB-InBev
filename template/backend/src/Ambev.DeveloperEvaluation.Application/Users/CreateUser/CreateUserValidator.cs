using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Validator for CreateUserCommand that defines validation rules for user creation command.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Username: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None
    /// - Name: Firstname and Lastname required, max 50 characters
    /// - Address: City and Street required, max 50/150 characters; Number must be positive; Zipcode required
    /// - Geolocation: Latitude and Longitude required, max 50 characters
    /// </remarks>
    public CreateUserCommandValidator()
    {
        RuleFor(user => user.Email)
            .SetValidator(new EmailValidator())
            .WithMessage("The provided email is not valid.");

        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator())
            .WithMessage("The password does not meet the security requirements.");

        RuleFor(user => user.Phone)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Phone number must follow the international format (+X XXXXXXXXXX).");

        RuleFor(user => user.Status)
            .NotEqual(UserStatus.Unknown)
            .WithMessage("User status cannot be 'Unknown'.");

        RuleFor(user => user.Role)
            .NotEqual(UserRole.None)
            .WithMessage("User role cannot be 'None'.");

        // Name Validation
        RuleFor(user => user.Name.Firstname)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must be at most 50 characters.");

        RuleFor(user => user.Name.Lastname)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must be at most 50 characters.");

        // Address Validation
        RuleFor(user => user.Address.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City must be at most 50 characters.");

        RuleFor(user => user.Address.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(150).WithMessage("Street must be at most 150 characters.");

        RuleFor(user => user.Address.Number)
            .GreaterThan(0).WithMessage("Address number must be a positive value.");

        RuleFor(user => user.Address.Zipcode)
            .NotEmpty().WithMessage("Zipcode is required.")
            .MaximumLength(20).WithMessage("Zipcode must be at most 20 characters.");

        // Geolocation Validation
        RuleFor(user => user.Address.Geolocation.Latitude)
            .NotEmpty().WithMessage("Latitude is required.")
            .MaximumLength(50).WithMessage("Latitude must be at most 50 characters.");

        RuleFor(user => user.Address.Geolocation.Longitude)
            .NotEmpty().WithMessage("Longitude is required.")
            .MaximumLength(50).WithMessage("Longitude must be at most 50 characters.");
    }
}