using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Common;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Users;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ListUsersHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated users will have valid:
    /// - Page (using random number)
    /// - Size (using random number)
    /// - Order (using random column and direction)
    /// </summary>
    private static readonly Faker<ListUsersCommand> listUsersHandlerFaker = new Faker<ListUsersCommand>()
        .RuleFor(u => u.Page, f => f.Random.Number(1, 99))
        .RuleFor(u => u.Size, f => f.Random.Number(1, 99))
        .RuleFor(u => u.Order, f => f.GenerateOrder<User>());

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static ListUsersCommand GenerateValidCommand()
    {
        return listUsersHandlerFaker.Generate();
    }

    public static ApiQueryRequestDomain GenerateValidCommand(ListUsersCommand command)
    {
        return new ApiQueryRequestDomain()
        {
            Order = command.Order,
            Page = command.Page,
            Size = command.Size,
        };
    }

    public static ApiQueryResponseDomain<User> GenerateValidResponse()
    {
        return new ApiQueryResponseDomain<User>
        {
            Data = new List<User>() { UserHandlerTestData.GenerateValidEntity() },
            TotalItems = 1,
            CurrentPage = 1,
            TotalPages = 1
        };
    }

    public static ListUsersResult GenerateValidResponse(ApiQueryResponseDomain<User> domainResponse)
    {
        return new ListUsersResult
        {
            Data = new List<UserApplication>() { UserHandlerTestData.GenerateValidEntity(domainResponse.Data[0]) },
            TotalItems = domainResponse.TotalItems,
            CurrentPage = domainResponse.CurrentPage,
            TotalPages = domainResponse.TotalPages
        };
    }
}
