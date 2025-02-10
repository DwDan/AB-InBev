using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

public interface IProductRepository
{
    /// <summary>
    /// Creates a new product in the repository.
    /// </summary>
    /// <param name="product">The product to create.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The created product.</returns>
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product in the repository.
    /// </summary>
    /// <param name="product">The product to update.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The updated product.</returns>
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The product if found, otherwise null.</returns>
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>True if the product was deleted, otherwise false.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by its title.
    /// </summary>
    /// <param name="title">The title of the product to search for.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The product if found, otherwise null.</returns>
    Task<Product?> GetByAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all unique product categories in the repository.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A list of unique product categories.</returns>
    Task<List<string>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all products with pagination and sorting.
    /// </summary>
    /// <param name="request">The pagination and sorting request parameters.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A paginated response containing the list of products.</returns>
    Task<ApiQueryResponseDomain<Product>> GetAllProductsAsync(ApiQueryRequestDomain request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all products from a specific category with pagination, sorting, and filtering.
    /// </summary>
    /// <param name="request">The pagination and sorting request parameters.</param>
    /// <param name="category">The category to filter products by.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A paginated response containing the filtered list of products.</returns>
    Task<ApiQueryResponseDomain<Product>> GetAllProductsByCategoryAsync(ApiQueryRequestDomain request, string category, CancellationToken cancellationToken = default);
}
