using System.Linq.Dynamic.Core;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new product in the database.
    /// </summary>
    /// <param name="product">The product to create.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The created product.</returns>
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The product if found, otherwise null.</returns>
    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a product by its title.
    /// </summary>
    /// <param name="title">The title of the product to search for.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The product if found, otherwise null.</returns>
    public async Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(o => o.Title == title, cancellationToken);
    }

    /// <summary>
    /// Deletes a product from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>True if the product was deleted, otherwise false.</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Updates an existing product in the database.
    /// </summary>
    /// <param name="product">The product to update.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The updated product.</returns>
    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    /// <summary>
    /// Retrieves all product categories in the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A list of unique categories.</returns>
    public async Task<List<string>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products.Select(x => x.Category)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves all products in the database with pagination and sorting.
    /// </summary>
    /// <param name="request">The pagination and sorting request parameters.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A paginated response containing the list of products.</returns>
    public async Task<ApiQueryResponseDomain<Product>> GetAllProductsAsync(ApiQueryRequestDomain request, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Order))
            query = query.OrderBy(request.Order.Trim());

        int totalItems = await query.CountAsync(cancellationToken);

        var data = await query
            .Skip((request.Page - 1) * request.Size)
            .Take(request.Size)
            .ToListAsync(cancellationToken);

        return new ApiQueryResponseDomain<Product>
        {
            Data = data,
            TotalItems = totalItems,
            CurrentPage = request.Page,
            TotalPages = (int)Math.Ceiling(totalItems / (double)request.Size)
        };
    }

    /// <summary>
    /// Retrieves all products from a specific category with pagination, sorting, and filtering.
    /// </summary>
    /// <param name="request">The pagination and sorting request parameters.</param>
    /// <param name="category">The category to filter products by.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A paginated response containing the filtered list of products.</returns>
    public async Task<ApiQueryResponseDomain<Product>> GetAllProductsByCategoryAsync(ApiQueryRequestDomain request, string category, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable()
            .Where(p => p.Category == category);

        if (!string.IsNullOrWhiteSpace(request.Order))
            query = query.OrderBy(request.Order.Trim());

        int totalItems = await query.CountAsync(cancellationToken);

        var data = await query
            .Skip((request.Page - 1) * request.Size)
            .Take(request.Size)
            .ToListAsync(cancellationToken);

        return new ApiQueryResponseDomain<Product>
        {
            Data = data,
            TotalItems = totalItems,
            CurrentPage = request.Page,
            TotalPages = (int)Math.Ceiling(totalItems / (double)request.Size)
        };
    }
}