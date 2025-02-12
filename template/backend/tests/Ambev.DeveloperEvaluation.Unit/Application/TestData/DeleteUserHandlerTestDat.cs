using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DeleteUserHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated users will have valid:
    /// - Id (using random numbers)
    /// </summary>
    private static readonly Faker<DeleteUserCommand> deleteUserHandlerFaker = new Faker<DeleteUserCommand>()
        .CustomInstantiator(f => new DeleteUserCommand(f.Random.Int(1, 99)));

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static DeleteUserCommand GenerateValidCommand()
    {
        return deleteUserHandlerFaker.Generate();
    }
}
