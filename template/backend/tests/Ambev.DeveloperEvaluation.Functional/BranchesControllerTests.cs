using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Functional.Infra;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional;

public class BranchesControllerTests : BaseControllerTests
{
    public BranchesControllerTests(FunctionalDatabaseFixture fixture) : base(fixture) { }

    [Fact(DisplayName = "POST /api/branches - Deve criar uma nova branch com sucesso")]
    public async Task CreateBranch_ShouldReturn_CreatedStatus()
    {
        // Arrange
        var newBranch = BranchRepositoryTestData.GenerateValidEntity();

        var request = new
        {
            Name = newBranch.Name,
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/branches", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateBranchResponse>>();
        responseData.Should().NotBeNull();
        responseData.Data.Id.Should().BeGreaterThan(0);
    }
}
