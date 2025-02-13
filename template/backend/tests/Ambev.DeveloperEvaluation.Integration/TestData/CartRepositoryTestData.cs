using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.TestData;

public static class CartRepositoryTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Cart entities.
    /// </summary>
    private static readonly Faker<Cart> handlerFaker = new Faker<Cart>()
        .RuleFor(c => c.Id, f => f.Random.Number(1, 9999))
        .RuleFor(c => c.UserId, f => f.Random.Number(1, 9999))
        .RuleFor(c => c.Date, f => f.Date.Past(1).ToUniversalTime())
        .RuleFor(c => c.BranchId, f => f.Random.Bool() ? f.Random.Number(1, 9999) : (int?)null)
        .RuleFor(c => c.Inactive, f => f.Random.Bool())
        .RuleFor(c => c.IsCancelled, f => f.Random.Bool())
        .RuleFor(c => c.IsFinished, f => f.Random.Bool())
        .RuleFor(c => c.Price, f => f.Finance.Amount(10, 500))
        .RuleFor(c => c.TotalPrice, (f, c) => c.Price * f.Random.Decimal(1, 1.2M))
        .RuleFor(c => c.Products, _ => new List<CartProduct>())
        .RuleFor(c => c.User, _ => default)
        .RuleFor(c => c.Branch, _ => default);

    /// <summary>
    /// Generates a valid Cart entity with randomized data.
    /// </summary>
    /// <returns>A valid Cart entity with randomly generated data.</returns>
    public static Cart GenerateValidEntity()
    {
        return handlerFaker.Generate();
    }
}
