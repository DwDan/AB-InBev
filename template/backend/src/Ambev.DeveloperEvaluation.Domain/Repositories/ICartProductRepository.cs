using Ambev.DeveloperEvaluation.Domain.Entities;

public interface ICartProductRepository
{
    /// <summary>
    /// Creates a new cartProduct in the repository.
    /// </summary>
    /// <param name="cartProduct">The cartProduct to create.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The created cartProduct.</returns>
    Task<CartProduct> CreateAsync(CartProduct cartProduct, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing cartProduct in the repository.
    /// </summary>
    /// <param name="cartProduct">The cartProduct to update.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The updated cartProduct.</returns>
    Task<CartProduct> UpdateAsync(CartProduct cartProduct, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a cartProduct by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cartProduct.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The cartProduct if found, otherwise null.</returns>
    Task<CartProduct?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a cartProduct from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cartProduct to delete.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>True if the cartProduct was deleted, otherwise false.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
