using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.TestData;

public static class CartProductRepositoryTestData
{
    /// <summary>
    /// Configures the Faker to generate valid CartProduct entities.
    /// </summary>
    private static readonly Faker<CartProduct> handlerFaker = new Faker<CartProduct>()
        .RuleFor(cp => cp.Id, f => f.Random.Number(1, 9999))
        .RuleFor(cp => cp.CartId, f => f.Random.Number(1, 9999))
        .RuleFor(cp => cp.ProductId, f => f.Random.Number(1, 9999))
        .RuleFor(cp => cp.Quantity, f => f.Random.Number(1, 100))
        .RuleFor(cp => cp.UnityPrice, f => f.Finance.Amount(1, 500))
        .RuleFor(cp => cp.Discount, f => f.Finance.Amount(0, 50))
        .RuleFor(cp => cp.TotalPrice, (f, cp) => (cp.UnityPrice * cp.Quantity) - cp.Discount)
        .RuleFor(cp => cp.Cart, _ => null)
        .RuleFor(cp => cp.Product, _ => null);

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static CartProduct GenerateValidEntity()
    {
        return handlerFaker.Generate();
    }
}