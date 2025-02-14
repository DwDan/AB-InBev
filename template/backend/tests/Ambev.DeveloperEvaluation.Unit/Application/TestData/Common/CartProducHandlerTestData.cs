using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Common;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CartProductHandlerTestData
{
    private static readonly Faker<CartProduct> cartProductFaker = new Faker<CartProduct>()
        .RuleFor(cp => cp.Id, f => f.Random.Int(1, 1000))
        .RuleFor(cp => cp.CartId, f => f.Random.Int(1, 1000))
        .RuleFor(cp => cp.ProductId, f => f.Random.Int(1, 1000))
        .RuleFor(cp => cp.Quantity, f => f.Random.Int(1, 20))
        .RuleFor(cp => cp.UnityPrice, f => f.Finance.Amount(5, 100))
        .RuleFor(cp => cp.Discount, f => f.Random.Decimal(0, 0.2m))
        .RuleFor(cp => cp.TotalPrice, (f, cp) => cp.UnityPrice * cp.Quantity * (1 - cp.Discount));

    private static readonly Faker<CartProductApplication> cartProductApplicationFaker = new Faker<CartProductApplication>()
        .RuleFor(cp => cp.Id, f => f.Random.Int(1, 1000))
        .RuleFor(cp => cp.CartId, f => f.Random.Int(1, 1000))
        .RuleFor(cp => cp.ProductId, f => f.Random.Int(1, 1000))
        .RuleFor(cp => cp.Quantity, f => f.Random.Int(1, 20));

    public static CartProduct GenerateValidEntity()
    {
        return cartProductFaker.Generate();
    } 
    
    public static CartProductApplication GenerateValidApplication()
    {
        return cartProductApplicationFaker.Generate();
    }

    public static List<CartProduct> GenerateList(int count)
    {
        return cartProductFaker.Generate(count);
    }

    public static CartProductApplication GenerateValidEntity(CartProduct cartProduct)
    {
        return new CartProductApplication
        {
            Id = cartProduct.Id,
            CartId = cartProduct.CartId,
            ProductId = cartProduct.ProductId,
            Quantity = cartProduct.Quantity
        };
    }
}
