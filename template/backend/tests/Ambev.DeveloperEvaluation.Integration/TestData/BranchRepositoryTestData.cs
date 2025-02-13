using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.TestData;

public static class BranchRepositoryTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Branch entities.
    /// </summary>
    private static readonly Faker<Branch> handlerFaker = new Faker<Branch>()
        .RuleFor(u => u.Name, f => f.Name.FirstName());

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static Branch GenerateValidEntity()
    {
        return handlerFaker.Generate();
    }
}