using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Common;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CartHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Cart entities.
    /// </summary>
    private static readonly Faker<Cart> cartHandlerFaker = new Faker<Cart>()
        .RuleFor(c => c.Id, f => f.Random.Int(1, 1000))
        .RuleFor(c => c.UserId, f => f.Random.Int(1, 1000))
        .RuleFor(c => c.Date, f => f.Date.Recent())
        .RuleFor(c => c.BranchId, f => f.Random.Bool() ? f.Random.Int(1, 100) : (int?)null)
        .RuleFor(c => c.Inactive, f => f.Random.Bool())
        .RuleFor(c => c.IsCancelled, f => f.Random.Bool())
        .RuleFor(c => c.IsFinished, f => f.Random.Bool())
        .RuleFor(c => c.Price, f => f.Finance.Amount(50, 500))
        .RuleFor(c => c.TotalPrice, (f, c) => c.Price * (1 - f.Random.Decimal(0, 0.2m)))
        .RuleFor(c => c.Products, f => new List<CartProduct>() { CartProductHandlerTestData.GenerateValidEntity() })
        .RuleFor(c => c.User, f => new User { Id = f.Random.Int(1, 1000) });    
    
    private static readonly Faker<CartApplication> cartApplicationHandlerFaker = new Faker<CartApplication>()
        .RuleFor(c => c.Id, f => f.Random.Int(1, 1000))
        .RuleFor(c => c.UserId, f => f.Random.Int(1, 1000))
        .RuleFor(c => c.Date, f => f.Date.Recent())
        .RuleFor(c => c.BranchId, f => f.Random.Bool() ? f.Random.Int(1, 100) : (int?)null)
        .RuleFor(c => c.IsCancelled, f => f.Random.Bool())
        .RuleFor(c => c.IsFinished, f => f.Random.Bool())
        .RuleFor(c => c.Products, f => new List<CartProductApplication>() { CartProductHandlerTestData.GenerateValidApplication() });

    /// <summary>
    /// Generates a valid Cart entity with randomized data.
    /// The generated cart will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Cart entity with randomly generated data.</returns>
    public static Cart GenerateValidEntity()
    {
        return cartHandlerFaker.Generate();
    }    
    
    public static CartApplication GenerateValidApplication()
    {
        return cartApplicationHandlerFaker.Generate();
    }

    /// <summary>
    /// Generates a valid CartApplication entity from a Cart entity.
    /// </summary>
    /// <returns>A valid CartApplication entity.</returns>
    public static CartApplication GenerateValidEntity(Cart cart)
    {
        return new CartApplication
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Date = cart.Date,
            IsFinished = cart.IsFinished,
            IsCancelled = cart.IsCancelled,
            BranchId = cart.BranchId,
            Products = cart.Products.Select(p => new CartProductApplication
            {
                Id = p.Id,
                CartId = p.CartId,
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }).ToList()
        };
    }
}