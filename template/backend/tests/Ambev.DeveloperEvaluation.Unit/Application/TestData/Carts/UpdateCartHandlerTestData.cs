using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Common;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Carts;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UpdateCartHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Cart entities.
    /// The generated carts will have valid:
    /// - Id (using random numbers)
    /// - Cartname (using internet cartnames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Customer or Admin)
    /// </summary>
    private static readonly Faker<UpdateCartCommand> updateCartHandlerFaker = new Faker<UpdateCartCommand>()
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
    public static UpdateCartCommand GenerateValidCommand()
    {
        return updateCartHandlerFaker.Generate();
    }

    public static Cart GenerateValidCommand(UpdateCartCommand cart)
    {
        return new Cart
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Date = cart.Date,
            IsFinished = cart.IsFinished,
            IsCancelled = cart.IsCancelled,
            BranchId = cart.BranchId,
            Products = cart.Products.Select(p => new CartProduct
            {
                Id = p.Id,
                CartId = p.CartId,
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                Product = new Product() 
                {
                    Id = p.ProductId,
                }
            }).ToList()
        };
    }

    public static UpdateCartResult GenerateValidResult(Cart cart)
    {
        return new UpdateCartResult
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Date = cart.Date,
            BranchId = cart.BranchId,
            Products = cart.Products.Select(p => new UpdateCartProductApplication
            {
                Id = p.Id,
                CartId = p.CartId,
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                Discount = p.Discount,
                TotalPrice = p.TotalPrice,
                UnityPrice = p.UnityPrice,
                Product = new ProductApplication()
                {
                    Id = p.ProductId,
                }
            }).ToList()
        };
    }
}
