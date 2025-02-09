using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Branch entity operations
/// </summary>
public interface IBranchRepository
{
    /// <summary>
    /// Creates a new branch in the repository
    /// </summary>
    /// <param name="branch">The branch to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created branch</returns>
    Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing branch in the repository
    /// </summary>
    /// <param name="branch">The branch to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated branch</returns>
    Task<Branch> UpdateAsync(Branch branch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a branch by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch if found, null otherwise</returns>
    Task<Branch?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a branch by predicate
    /// </summary>
    /// <param name="predicate">The predicate to filter branches</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch if found, null otherwise</returns>
    Task<Branch?> GetByAsync(Expression<Func<Branch, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all branches with pagination and sorting
    /// </summary>
    /// <param name="request">The pagination and sorting request parameters</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
    /// <returns>A paginated response containing the list of branches</returns>
    Task<ApiQueryResponseDomain<Branch>> GetAllBranchesAsync(ApiQueryRequestDomain request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a branch from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the branch was deleted, false if not found</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
