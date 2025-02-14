using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Common;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Carts;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ListCartsHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Cart entities.
    /// The generated carts will have valid:
    /// - Page (using random number)
    /// - Size (using random number)
    /// - Order (using random column and direction)
    /// </summary>
    private static readonly Faker<ListCartsCommand> listCartsHandlerFaker = new Faker<ListCartsCommand>()
        .RuleFor(u => u.Page, f => f.Random.Number(1, 99))
        .RuleFor(u => u.Size, f => f.Random.Number(1, 99))
        .RuleFor(u => u.Order, f => f.GenerateOrder<Cart>());

    /// <summary>
    /// Generates a valid Cart entity with randomized data.
    /// The generated cart will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Cart entity with randomly generated data.</returns>
    public static ListCartsCommand GenerateValidCommand()
    {
        return listCartsHandlerFaker.Generate();
    }

    public static ApiQueryRequestDomain GenerateValidCommand(ListCartsCommand command)
    {
        return new ApiQueryRequestDomain()
        {
            Order = command.Order,
            Page = command.Page,
            Size = command.Size,
        };
    }

    public static ApiQueryResponseDomain<Cart> GenerateValidResponse()
    {
        return new ApiQueryResponseDomain<Cart>
        {
            Data = new List<Cart>() { CartHandlerTestData.GenerateValidEntity() },
            TotalItems = 1,
            CurrentPage = 1,
            TotalPages = 1
        };
    }

    public static ListCartsResult GenerateValidResponse(ApiQueryResponseDomain<Cart> domainResponse)
    {
        return new ListCartsResult
        {
            Data = new List<CartApplication>() { CartHandlerTestData.GenerateValidEntity(domainResponse.Data[0]) },
            TotalItems = domainResponse.TotalItems,
            CurrentPage = domainResponse.CurrentPage,
            TotalPages = domainResponse.TotalPages
        };
    }
}
