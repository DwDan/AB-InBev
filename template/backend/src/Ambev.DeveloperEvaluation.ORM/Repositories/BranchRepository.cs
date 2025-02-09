﻿using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IBranchRepository using Entity Framework Core
/// </summary>
public class BranchRepository : IBranchRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of BranchRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public BranchRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new branch in the database
    /// </summary>
    /// <param name="branch">The branch to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created branch</returns>
    public async Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        await _context.Branches.AddAsync(branch, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return branch;
    }

    /// <summary>
    /// Updates an existing branch in the database
    /// </summary>
    /// <param name="branch">The branch to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated branch</returns>
    public async Task<Branch> UpdateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        _context.Branches.Update(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return branch;
    }

    /// <summary>
    /// Retrieves a branch by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch if found, null otherwise</returns>
    public async Task<Branch?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Branches.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a branch by a given predicate
    /// </summary>
    /// <param name="predicate">The predicate to filter branches</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch if found, null otherwise</returns>
    public async Task<Branch?> GetByAsync(Expression<Func<Branch, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Branches.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// Retrieves all branches in the database with pagination and sorting.
    /// </summary>
    /// <param name="request">The pagination and sorting request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated response containing the list of branches</returns>
    public async Task<ApiQueryResponseDomain<Branch>> GetAllBranchesAsync(ApiQueryRequestDomain request, CancellationToken cancellationToken = default)
    {
        var query = _context.Branches
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Order))
            query = query.OrderBy(request.Order.Trim());

        int totalItems = await query.CountAsync(cancellationToken);

        var data = await query
            .Skip((request.Page - 1) * request.Size)
            .Take(request.Size)
            .ToListAsync(cancellationToken);

        return new ApiQueryResponseDomain<Branch>
        {
            Data = data,
            TotalItems = totalItems,
            CurrentPage = request.Page,
            TotalPages = (int)Math.Ceiling(totalItems / (double)request.Size)
        };
    }

    /// <summary>
    /// Deletes a branch from the database by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the branch was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var branch = await GetByIdAsync(id, cancellationToken);
        if (branch == null)
            return false;

        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
