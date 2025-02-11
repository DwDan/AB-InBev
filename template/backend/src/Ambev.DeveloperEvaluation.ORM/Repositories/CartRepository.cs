using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Sale;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CartRepository : ICartRepository
{
    private readonly DefaultContext _context;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CartRepository(DefaultContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new cart in the database.
    /// </summary>
    /// <param name="cart">The cart to create.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The created cart.</returns>
    public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new SaleCreatedEvent(cart.Id), cancellationToken);

        return cart;
    }

    /// <summary>
    /// Retrieves a cart by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The cart if found, otherwise null.</returns>
    public async Task<Cart?> GetFullByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(cart => cart.User)
            .Include(cart => cart.Products)
            .ThenInclude(cartProduct => cartProduct.Product)
            .FirstOrDefaultAsync(cart=> cart.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a cart by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The cart if found, otherwise null.</returns>
    public async Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Carts.FirstOrDefaultAsync(cart=> cart.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves all carts in the database with pagination and sorting.
    /// </summary>
    /// <param name="request">The pagination and sorting request parameters.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A paginated response containing the list of carts.</returns>
    public async Task<ApiQueryResponseDomain<Cart>> GetAllCartsAsync(ApiQueryRequestDomain request, CancellationToken cancellationToken = default)
    {
        var query = _context.Carts
            .Include(cart => cart.User)
            .Include(cart => cart.Products) 
            .ThenInclude(cartProduct => cartProduct.Product)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Order))
            query = query.OrderBy(request.Order.Trim());

        int totalItems = await query.CountAsync(cancellationToken);

        var data = await query
            .Skip((request.Page - 1) * request.Size)
            .Take(request.Size)
            .ToListAsync(cancellationToken);

        return new ApiQueryResponseDomain<Cart>
        {
            Data = data,
            TotalItems = totalItems,
            CurrentPage = request.Page,
            TotalPages = (int)Math.Ceiling(totalItems / (double)request.Size)
        };
    }

    /// <summary>
    /// Updates an existing cart in the database.
    /// </summary>
    /// <param name="cart">The cart to update.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The updated cart.</returns>
    public async Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new SaleModifiedEvent(cart.Id), cancellationToken);

        return cart;
    }

    /// <summary>
    /// Deletes a cart from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>True if the cart was deleted, otherwise false.</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);
        if (cart == null)
            return false;

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new SaleCancelledEvent(id), cancellationToken);

        return true;
    }

    /// <summary>
    /// Retrieves all carts by user ID.
    /// </summary>
    /// <param name="userId">The user ID to filter carts by.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A list of carts belonging to the user.</returns>
    public async Task<List<Cart>> GetCartsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Carts.Where(c => c.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves all carts by user ID.
    /// </summary>
    /// <param name="predicate">The predicate to filter carts by.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A list of carts belonging to the user.</returns>
    public async Task<Cart?> GetByAsync(Expression<Func<Cart, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(cart => cart.User)
            .Include(cart => cart.Products)
            .ThenInclude(cartProduct => cartProduct.Product)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }
}
