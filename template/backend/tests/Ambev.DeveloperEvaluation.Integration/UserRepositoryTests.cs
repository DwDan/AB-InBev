using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration;

public class UserRepositoryTests : BaseRepositoryTests
{
    public UserRepositoryTests(IntegrationDatabaseFixture fixture) : base(fixture) { }

    [Fact(DisplayName = "Given a new user When adding to repository Then should persist in database")]
    public async Task AddUser_ShouldPersistInDatabase()
    {
        var userRepository = new UserRepository(DbContext);
        var user = UserRepositoryTestData.GenerateValidEntity();
        await userRepository.CreateAsync(user);

        var result = await userRepository.GetByIdAsync(user.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(user.Id);
    }

    [Fact(DisplayName = "Given an existing user When updating Then should update correctly")]
    public async Task UpdateUser_ShouldUpdateCorrectly()
    {
        var userRepository = new UserRepository(DbContext);
        var user = UserRepositoryTestData.GenerateValidEntity();
        await userRepository.CreateAsync(user);

        user.Name.Firstname = "Updated Name";
        await userRepository.UpdateAsync(user);

        var result = await userRepository.GetByIdAsync(user.Id);

        result.Should().NotBeNull();
        result.Name.Firstname.Should().Be("Updated Name");
    }

    [Fact(DisplayName = "Given a user ID When retrieving Then should return correct user")]
    public async Task GetUserById_ShouldReturnCorrectUser()
    {
        var userRepository = new UserRepository(DbContext);
        var user = UserRepositoryTestData.GenerateValidEntity();
        await userRepository.CreateAsync(user);

        var result = await userRepository.GetByIdAsync(user.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(user.Id);
    }

    [Fact(DisplayName = "Given an email When retrieving Then should return correct user")]
    public async Task GetUserByEmail_ShouldReturnCorrectUser()
    {
        var userRepository = new UserRepository(DbContext);
        var user = UserRepositoryTestData.GenerateValidEntity();
        await userRepository.CreateAsync(user);

        var result = await userRepository.GetByEmailAsync(user.Email);

        result.Should().NotBeNull();
        result.Email.Should().Be(user.Email);
    }

    [Fact(DisplayName = "Given a predicate When retrieving Then should return correct user")]
    public async Task GetUserByPredicate_ShouldReturnCorrectUser()
    {
        var userRepository = new UserRepository(DbContext);
        var user = UserRepositoryTestData.GenerateValidEntity();
        await userRepository.CreateAsync(user);

        var result = await userRepository.GetByAsync(u => u.Email == user.Email);

        result.Should().NotBeNull();
        result.Email.Should().Be(user.Email);
    }

    [Fact(DisplayName = "Given a pagination request When retrieving all users Then should return paginated result")]
    public async Task GetAllUsers_ShouldReturnPaginatedResult()
    {
        var userRepository = new UserRepository(DbContext);
        var user1 = UserRepositoryTestData.GenerateValidEntity();
        var user2 = UserRepositoryTestData.GenerateValidEntity();
        await userRepository.CreateAsync(user1);
        await userRepository.CreateAsync(user2);

        var request = new ApiQueryRequestDomain { Page = 1, Size = 10 };
        var result = await userRepository.GetAllUsersAsync(request);

        result.Should().NotBeNull();
        result.Data.Should().NotBeEmpty();
        result.TotalItems.Should().Be(2);
    }

    [Fact(DisplayName = "Given a user ID When deleting Then should remove user correctly")]
    public async Task DeleteUser_ShouldRemoveUserCorrectly()
    {
        var userRepository = new UserRepository(DbContext);
        var user = UserRepositoryTestData.GenerateValidEntity();
        await userRepository.CreateAsync(user);

        var deleted = await userRepository.DeleteAsync(user.Id);
        var result = await userRepository.GetByIdAsync(user.Id);

        deleted.Should().BeTrue();
        result.Should().BeNull();
    }
}
