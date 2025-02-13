using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration;

public class BranchRepositoryTests : BaseRepositoryTests
{
    public BranchRepositoryTests(IntegrationDatabaseFixture fixture) : base(fixture){}

    [Fact(DisplayName = "Given a new branch When adding to repository Then should persist in database")]
    public async Task AddBranch_ShouldPersistInDatabase()
    {
        var repository = new BranchRepository(DbContext);
        var branch = BranchRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(branch);

        var result = await repository.GetByIdAsync(branch.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(branch.Id);
    }

    [Fact(DisplayName = "Given an existing branch When updating Then should update correctly")]
    public async Task UpdateBranch_ShouldUpdateCorrectly()
    {
        var repository = new BranchRepository(DbContext);
        var branch = BranchRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(branch);

        branch.Name = "Updated Branch";
        await repository.UpdateAsync(branch);

        var result = await repository.GetByIdAsync(branch.Id);

        result.Should().NotBeNull();
        result.Name.Should().Be("Updated Branch");
    }

    [Fact(DisplayName = "Given a branch ID When retrieving Then should return correct branch")]
    public async Task GetBranchById_ShouldReturnCorrectBranch()
    {
        var repository = new BranchRepository(DbContext);
        var branch = BranchRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(branch);

        var result = await repository.GetByIdAsync(branch.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(branch.Id);
    }

    [Fact(DisplayName = "Given a predicate When retrieving Then should return correct branch")]
    public async Task GetBranchByPredicate_ShouldReturnCorrectBranch()
    {
        var repository = new BranchRepository(DbContext);
        var branch = BranchRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(branch);

        var result = await repository.GetByAsync(b => b.Name == branch.Name);

        result.Should().NotBeNull();
        result.Name.Should().Be(branch.Name);
    }

    [Fact(DisplayName = "Given a pagination request When retrieving all branches Then should return paginated result")]
    public async Task GetAllBranches_ShouldReturnPaginatedResult()
    {
        var repository = new BranchRepository(DbContext);
        var branch1 = BranchRepositoryTestData.GenerateValidEntity();
        var branch2 = BranchRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(branch1);
        await repository.CreateAsync(branch2);

        var request = new ApiQueryRequestDomain { Page = 1, Size = 10 };
        var result = await repository.GetAllBranchesAsync(request);

        result.Should().NotBeNull();
        result.Data.Should().NotBeEmpty();
        result.TotalItems.Should().Be(2);
    }

    [Fact(DisplayName = "Given a branch ID When deleting Then should remove branch correctly")]
    public async Task DeleteBranch_ShouldRemoveBranchCorrectly()
    {
        var repository = new BranchRepository(DbContext);
        var branch = BranchRepositoryTestData.GenerateValidEntity();
        await repository.CreateAsync(branch);

        var deleted = await repository.DeleteAsync(branch.Id);
        var result = await repository.GetByIdAsync(branch.Id);

        deleted.Should().BeTrue();
        result.Should().BeNull();
    }
}
