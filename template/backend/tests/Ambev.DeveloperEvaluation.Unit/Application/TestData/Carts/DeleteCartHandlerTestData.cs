using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Carts;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DeleteCartHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Cart entities.
    /// The generated carts will have valid:
    /// - Id (using random numbers)
    /// </summary>
    private static readonly Faker<DeleteCartCommand> deleteCartHandlerFaker = new Faker<DeleteCartCommand>()
        .CustomInstantiator(f => new DeleteCartCommand(f.Random.Int(1, 99)));

    /// <summary>
    /// Generates a valid Cart entity with randomized data.
    /// The generated cart will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Cart entity with randomly generated data.</returns>
    public static DeleteCartCommand GenerateValidCommand()
    {
        return deleteCartHandlerFaker.Generate();
    }
}
