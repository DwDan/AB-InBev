using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.TestData;

public static class ProductRepositoryTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// </summary>
    private static readonly Faker<Product> handlerFaker = new Faker<Product>()
        .RuleFor(p => p.Id, f => f.Random.Number(1, 9999))
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => f.Finance.Amount(1, 1000))
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
        .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
        .RuleFor(p => p.Rating, f => new Rating
        {
            Rate = f.Random.Decimal(0, 5),
            Count = f.Random.Number(0, 1000)
        });

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static Product GenerateValidEntity()
    {
        return handlerFaker.Generate();
    }
}
