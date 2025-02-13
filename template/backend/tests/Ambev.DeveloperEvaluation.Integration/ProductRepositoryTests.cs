using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration;

public class ProductRepositoryTests : BaseRepositoryTests
{
    public ProductRepositoryTests(IntegrationDatabaseFixture fixture) : base(fixture) { }


    [Fact(DisplayName = "Given a new product When adding to repository Then should persist in database")]
    public async Task AddProduct_ShouldPersistInDatabase()
    {
        var repository = new ProductRepository(DbContext);

        var product = ProductRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(product);

        var result = await repository.GetByIdAsync(product.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(product.Id);
    }

    [Fact(DisplayName = "Given an existing product When updating Then should update correctly")]
    public async Task UpdateProduct_ShouldUpdateCorrectly()
    {
        var repository = new ProductRepository(DbContext);

        var product = ProductRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(product);

        product.Title = "Updated Product";
        await repository.UpdateAsync(product);

        var result = await repository.GetByIdAsync(product.Id);

        result.Should().NotBeNull();
        result.Title.Should().Be("Updated Product");
    }

    [Fact(DisplayName = "Given a product ID When retrieving Then should return correct product")]
    public async Task GetProductById_ShouldReturnCorrectProduct()
    {
        var repository = new ProductRepository(DbContext);

        var product = ProductRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(product);

        var result = await repository.GetByIdAsync(product.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(product.Id);
    }

    [Fact(DisplayName = "Given a predicate When retrieving Then should return correct product")]
    public async Task GetProductByPredicate_ShouldReturnCorrectProduct()
    {
        var repository = new ProductRepository(DbContext);

        var product = ProductRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(product);

        var result = await repository.GetByAsync(p => p.Title == product.Title);

        result.Should().NotBeNull();
        result.Title.Should().Be(product.Title);
    }

    [Fact(DisplayName = "Given a request When retrieving all product categories Then should return unique categories")]
    public async Task GetAllCategories_ShouldReturnUniqueCategories()
    {
        var repository = new ProductRepository(DbContext);

        var product1 = ProductRepositoryTestData.GenerateValidEntity();
        var product2 = ProductRepositoryTestData.GenerateValidEntity();
        product2.Category = product1.Category;
        await repository.CreateAsync(product1);
        await repository.CreateAsync(product2);

        var result = await repository.GetAllCategoriesAsync();

        result.Should().NotBeNull();
        result.Should().Contain(product1.Category);
    }

    [Fact(DisplayName = "Given a pagination request When retrieving all products Then should return paginated result")]
    public async Task GetAllProducts_ShouldReturnPaginatedResult()
    {
        var repository = new ProductRepository(DbContext);

        var product1 = ProductRepositoryTestData.GenerateValidEntity();
        var product2 = ProductRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(product1);
        await repository.CreateAsync(product2);

        var request = new ApiQueryRequestDomain { Page = 1, Size = 10 };
        var result = await repository.GetAllProductsAsync(request);

        result.Should().NotBeNull();
        result.Data.Should().NotBeEmpty();
        result.TotalItems.Should().Be(2);
    }

    [Fact(DisplayName = "Given a category and pagination request When retrieving products Then should return filtered products")]
    public async Task GetAllProductsByCategory_ShouldReturnFilteredProducts()
    {
        var repository = new ProductRepository(DbContext);

        var product1 = ProductRepositoryTestData.GenerateValidEntity();
        var product2 = ProductRepositoryTestData.GenerateValidEntity();
        product2.Category = "Electronics";
        await repository.CreateAsync(product1);
        await repository.CreateAsync(product2);

        var request = new ApiQueryRequestDomain { Page = 1, Size = 10 };
        var result = await repository.GetAllProductsByCategoryAsync(request, "Electronics");

        result.Should().NotBeNull();
        result.Data.Should().NotBeEmpty();
        result.Data.All(p => p.Category == "Electronics").Should().BeTrue();
    }

    [Fact(DisplayName = "Given a product ID When deleting Then should remove product correctly")]
    public async Task DeleteProduct_ShouldRemoveProductCorrectly()
    {
        var repository = new ProductRepository(DbContext);

        var product = ProductRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(product);

        var deleted = await repository.DeleteAsync(product.Id);
        var result = await repository.GetByIdAsync(product.Id);

        deleted.Should().BeTrue();
        result.Should().BeNull();
    }
}
