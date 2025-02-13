using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration;

public class CartRepositoryTests : BaseRepositoryTests
{
    private readonly IMediator _mediator;

    public CartRepositoryTests(IntegrationDatabaseFixture fixture) : base(fixture)
    {
        _mediator = Substitute.For<IMediator>();
    }

    [Fact(DisplayName = "Given a new cart When adding to repository Then should persist in database")]
    public async Task AddCart_ShouldPersistInDatabase()
    {
        var repository = new CartRepository(DbContext, _mediator);
        var cart = await CreateCart(repository);

        var result = await repository.GetByIdAsync(cart.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(cart.Id);
    }


    [Fact(DisplayName = "Given an existing cart When updating Then should update correctly")]
    public async Task UpdateCart_ShouldUpdateCorrectly()
    {
        var repository = new CartRepository(DbContext, _mediator);
        var cart = await CreateCart(repository);

        cart.TotalPrice = 200;
        await repository.UpdateAsync(cart);

        var result = await repository.GetByIdAsync(cart.Id);

        result.Should().NotBeNull();
        result.TotalPrice.Should().Be(200);
    }

    [Fact(DisplayName = "Given a cart ID When retrieving Then should return correct cart")]
    public async Task GetCartById_ShouldReturnCorrectCart()
    {
        var repository = new CartRepository(DbContext, _mediator);
        var cart = await CreateCart(repository);

        var result = await repository.GetByIdAsync(cart.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(cart.Id);
    }

    [Fact(DisplayName = "Given a cart ID When retrieving full cart Then should return detailed cart")]
    public async Task GetFullCartById_ShouldReturnDetailedCart()
    {
        var repository = new CartRepository(DbContext, _mediator);
        var cart = await CreateCart(repository);

        var result = await repository.GetFullByIdAsync(cart.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(cart.Id);
    }

    [Fact(DisplayName = "Given a cart ID When deleting Then should remove cart correctly")]
    public async Task DeleteCart_ShouldRemoveCartCorrectly()
    {
        var repository = new CartRepository(DbContext, _mediator);
        var cart = await CreateCart(repository);

        var deleted = await repository.DeleteAsync(cart.Id);
        var result = await repository.GetByIdAsync(cart.Id);

        deleted.Should().BeTrue();
        result.Should().BeNull();
    }

    [Fact(DisplayName = "Given a user ID When retrieving carts Then should return user's carts")]
    public async Task GetCartsByUserId_ShouldReturnUsersCarts()
    {
        var repository = new CartRepository(DbContext, _mediator);
        var cart = await CreateCart(repository);

        var result = await repository.GetCartsByUserIdAsync(cart.UserId);

        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    [Fact(DisplayName = "Given a pagination request When retrieving all carts Then should return paginated result")]
    public async Task GetAllCarts_ShouldReturnPaginatedResult()
    {
        var repository = new CartRepository(DbContext, _mediator);
        var cart1 = await CreateCart(repository);
        var cart2 = await CreateCart(repository);

        var request = new ApiQueryRequestDomain { Page = 1, Size = 10 };
        var result = await repository.GetAllCartsAsync(request);

        result.Should().NotBeNull();
        result.Data.Should().NotBeEmpty();
        result.TotalItems.Should().Be(2);
    }

    [Fact(DisplayName = "Given a predicate When retrieving Then should return correct cart")]
    public async Task GetCartByPredicate_ShouldReturnCorrectCart()
    {
        var repository = new CartRepository(DbContext, _mediator);
        var cart = await CreateCart(repository);

        var result = await repository.GetByAsync(c => c.UserId == cart.UserId);

        result.Should().NotBeNull();
        result.UserId.Should().Be(cart.UserId);
    }

    private async Task<Cart> CreateCart(CartRepository repository)
    {
        var branchRepository = new BranchRepository(DbContext);
        var userRepository = new UserRepository(DbContext);

        var user = UserRepositoryTestData.GenerateValidEntity();
        user = await userRepository.CreateAsync(user);

        var branch = BranchRepositoryTestData.GenerateValidEntity();
        branch = await branchRepository.CreateAsync(branch);

        var cart = CartRepositoryTestData.GenerateValidEntity();
        cart.UserId = user.Id;
        cart.BranchId = branch.Id;

        return await repository.CreateAsync(cart);
    }
}
