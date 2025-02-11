using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

public interface ICartRepository
{
    /// <summary>
    /// Creates a new cart in the repository.
    /// </summary>
    /// <param name="cart">The cart to create.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The created cart.</returns>
    Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing cart in the repository.
    /// </summary>
    /// <param name="cart">The cart to update.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The updated cart.</returns>
    Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a cart by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The cart if found, otherwise null.</returns>
    Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a cart from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>True if the cart was deleted, otherwise false.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all carts by user ID.
    /// </summary>
    /// <param name="userId">The user ID to filter carts by.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A list of carts belonging to the user.</returns>
    Task<List<Cart>> GetCartsByUserIdAsync(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all carts with pagination and sorting.
    /// </summary>
    /// <param name="request">The pagination and sorting request parameters.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A paginated response containing the list of carts.</returns>
    Task<ApiQueryResponseDomain<Cart>> GetAllCartsAsync(ApiQueryRequestDomain request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all carts by user ID.
    /// </summary>
    /// <param name="userId">The predicate to filter carts by.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A list of carts belonging to the user.</returns>
    Task<Cart?> GetByAsync(Expression<Func<Cart, bool>> predicate, CancellationToken cancellationToken = default);
}
