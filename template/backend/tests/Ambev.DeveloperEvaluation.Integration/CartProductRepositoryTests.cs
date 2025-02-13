using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration;

public class CartProductRepositoryTests : BaseRepositoryTests
{
    private readonly IMediator _mediator;

    public CartProductRepositoryTests(DatabaseFixture fixture) : base(fixture)
    {
        _mediator = Substitute.For<IMediator>();
    }

    [Fact(DisplayName = "Given a new cartProduct When adding to repository Then should persist in database")]
    public async Task AddCartProduct_ShouldPersistInDatabase()
    {
        var cartProductRepository = new CartProductRepository(DbContext, _mediator);
        var cartProduct = await CreateCartProduct(cartProductRepository);

        var result = await cartProductRepository.GetByIdAsync(cartProduct.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(cartProduct.Id);
    }


    [Fact(DisplayName = "Given an existing cartProduct When updating Then should update correctly")]
    public async Task UpdateCartProduct_ShouldUpdateCorrectly()
    {
        var cartProductRepository = new CartProductRepository(DbContext, _mediator);
        var cartProduct = await CreateCartProduct(cartProductRepository);

        cartProduct.Quantity = 5;
        await cartProductRepository.UpdateAsync(cartProduct);

        var result = await cartProductRepository.GetByIdAsync(cartProduct.Id);

        result.Should().NotBeNull();
        result.Quantity.Should().Be(5);
    }

    [Fact(DisplayName = "Given a cartProduct ID When retrieving Then should return correct cartProduct")]
    public async Task GetCartProductById_ShouldReturnCorrectCartProduct()
    {
        var cartProductRepository = new CartProductRepository(DbContext, _mediator);
        var cartProduct = await CreateCartProduct(cartProductRepository);

        var result = await cartProductRepository.GetByIdAsync(cartProduct.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(cartProduct.Id);
    }

    [Fact(DisplayName = "Given a cartProduct ID When deleting Then should remove cartProduct correctly")]
    public async Task DeleteCartProduct_ShouldRemoveCartProductCorrectly()
    {
        var cartProductRepository = new CartProductRepository(DbContext, _mediator);
        var cartProduct = await CreateCartProduct(cartProductRepository);

        var deleted = await cartProductRepository.DeleteAsync(cartProduct.Id);
        var result = await cartProductRepository.GetByIdAsync(cartProduct.Id);

        deleted.Should().BeTrue();
        result.Should().BeNull();
    }

    private async Task<CartProduct> CreateCartProduct(CartProductRepository cartProductRepository)
    {
        var branchRepository = new BranchRepository(DbContext);
        var userRepository = new UserRepository(DbContext);
        var productRepository = new ProductRepository(DbContext);
        var cartRepository = new CartRepository(DbContext, _mediator);

        var user = UserRepositoryTestData.GenerateValidEntity();
        user = await userRepository.CreateAsync(user);

        var branch = BranchRepositoryTestData.GenerateValidEntity();
        branch = await branchRepository.CreateAsync(branch);

        var cart = CartRepositoryTestData.GenerateValidEntity();
        cart.UserId = user.Id;
        cart.BranchId = branch.Id;
        cart = await cartRepository.CreateAsync(cart);

        var product = ProductRepositoryTestData.GenerateValidEntity();
        product = await productRepository.CreateAsync(product);

        var cartProduct = CartProductRepositoryTestData.GenerateValidEntity();
        cartProduct.CartId = cart.Id;
        cartProduct.ProductId = product.Id;
        return await cartProductRepository.CreateAsync(cartProduct);
    }
}
