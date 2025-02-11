using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Sale;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CartProductRepository : ICartProductRepository
{
    private readonly DefaultContext _context;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartProductRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CartProductRepository(DefaultContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new cartProduct in the database.
    /// </summary>
    /// <param name="cartProduct">The cartProduct to create.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The created cartProduct.</returns>
    public async Task<CartProduct> CreateAsync(CartProduct cartProduct, CancellationToken cancellationToken = default)
    {
        await _context.CartProducts.AddAsync(cartProduct, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cartProduct;
    }

    /// <summary>
    /// Retrieves a cartProduct by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cartProduct.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The cartProduct if found, otherwise null.</returns>
    public async Task<CartProduct?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.CartProducts.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates an existing cartProduct in the database.
    /// </summary>
    /// <param name="cartProduct">The cartProduct to update.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The updated cartProduct.</returns>
    public async Task<CartProduct> UpdateAsync(CartProduct cartProduct, CancellationToken cancellationToken = default)
    {
        _context.CartProducts.Update(cartProduct);
        await _context.SaveChangesAsync(cancellationToken);
        return cartProduct;
    }

    /// <summary>
    /// Deletes a cartProduct from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cartProduct to delete.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>True if the cartProduct was deleted, otherwise false.</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var cartProduct = await GetByIdAsync(id, cancellationToken);
        if (cartProduct == null)
            return false;

        _context.CartProducts.Remove(cartProduct);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new SaleItemCancelledEvent(cartProduct.CartId, id), cancellationToken);

        return true;
    }
}