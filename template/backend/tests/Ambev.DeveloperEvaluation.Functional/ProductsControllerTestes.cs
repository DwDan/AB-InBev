using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Functional.Infra;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional;

public class ProductsControllerTests : BaseControllerTests
{
    public ProductsControllerTests(FunctionalDatabaseFixture fixture) : base(fixture) { }

    [Fact(DisplayName = "POST /api/products - Should create a new product successfully")]
    public async Task CreateProduct_ShouldReturn_CreatedStatus()
    {
        var newProduct = ProductRepositoryTestData.GenerateValidEntity();
        var request = new
        {
            Title = newProduct.Title,
            Price = newProduct.Price,
            Description = newProduct.Description,
            Category = newProduct.Category,
            Image = newProduct.Image
        };

        var response = await _client.PostAsJsonAsync("/api/products", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();
        responseData.Should().NotBeNull();
        responseData.Data.Id.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "GET /api/products - Should return a list of products")]
    public async Task ListProducts_ShouldReturn_ProductsList()
    {
        var newProduct = ProductRepositoryTestData.GenerateValidEntity();
        await _client.PostAsJsonAsync("/api/products", newProduct);

        var response = await _client.GetAsync("/api/products");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<ListProductsResponse>>();
        responseData.Should().NotBeNull();
        responseData.Data.Data.Should().HaveCountGreaterThan(0);
    }

    [Fact(DisplayName = "GET /api/products/{id} - Should return the correct product")]
    public async Task GetProduct_ShouldReturn_CorrectProduct()
    {
        var newProduct = ProductRepositoryTestData.GenerateValidEntity();
        var createResponse = await _client.PostAsJsonAsync("/api/products", newProduct);

        var createData = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();
        var createdProductId = createData.Data.Id;

        var response = await _client.GetAsync($"/api/products/{createdProductId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetProductResponse>>();
        responseData.Should().NotBeNull();
        responseData.Data.Id.Should().Be(createdProductId);
        responseData.Data.Title.Should().Be(newProduct.Title);
    }

    [Fact(DisplayName = "PUT /api/products/{id} - Should update the product successfully")]
    public async Task UpdateProduct_ShouldUpdate_ProductSuccessfully()
    {
        var newProduct = ProductRepositoryTestData.GenerateValidEntity();
        var createResponse = await _client.PostAsJsonAsync("/api/products", newProduct);

        var createData = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();
        var createdProductId = createData.Data.Id;

        newProduct.Title = "Updated Product Title";
        newProduct.Price = 199.99M;
        var updateResponse = await _client.PutAsJsonAsync($"/api/products/{createdProductId}", newProduct);

        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var updateData = await updateResponse.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateProductResponse>>();
        updateData.Should().NotBeNull();
        updateData.Data.Id.Should().Be(createdProductId);
        updateData.Data.Title.Should().Be("Updated Product Title");
        updateData.Data.Price.Should().Be(199.99M);
    }

    [Fact(DisplayName = "DELETE /api/products/{id} - Should delete the product successfully")]
    public async Task DeleteProduct_ShouldDelete_ProductSuccessfully()
    {
        var newProduct = ProductRepositoryTestData.GenerateValidEntity();
        var createResponse = await _client.PostAsJsonAsync("/api/products", newProduct);

        var createData = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();
        var createdProductId = createData.Data.Id;

        var deleteResponse = await _client.DeleteAsync($"/api/products/{createdProductId}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var getResponse = await _client.GetAsync($"/api/products/{createdProductId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact(DisplayName = "GET /api/products/categories - Should return a list of product categories")]
    public async Task ListCategories_ShouldReturn_CategoriesList()
    {
        await _client.PostAsJsonAsync("/api/products", ProductRepositoryTestData.GenerateValidEntity());
        await _client.PostAsJsonAsync("/api/products", ProductRepositoryTestData.GenerateValidEntity());

        var response = await _client.GetAsync("/api/products/categories");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<ListCategoriesResponse>>();
        responseData.Should().NotBeNull();
        responseData.Data.Categories.Should().HaveCountGreaterThan(0);
    }
}
