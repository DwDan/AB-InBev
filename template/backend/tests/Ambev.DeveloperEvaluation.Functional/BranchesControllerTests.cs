using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Functional.Infra;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.ListBranches;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional;

public class BranchesControllerTests : BaseControllerTests
{
    public BranchesControllerTests(FunctionalDatabaseFixture fixture) : base(fixture) { }

    [Fact(DisplayName = "POST /api/branches - Deve criar uma nova branch com sucesso")]
    public async Task CreateBranch_ShouldReturn_CreatedStatus()
    {
        var newBranch = BranchRepositoryTestData.GenerateValidEntity();
        var request = new { Name = newBranch.Name };

        var response = await _client.PostAsJsonAsync("/api/branches", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateBranchResponse>>();
        responseData.Should().NotBeNull();
        responseData.Data.Id.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "GET /api/branches - Deve retornar a lista de branches")]
    public async Task ListBranches_ShouldReturn_BranchesList()
    {
        var newBranch = BranchRepositoryTestData.GenerateValidEntity();
        var createResponse = await _client.PostAsJsonAsync("/api/branches", new { Name = newBranch.Name });

        var response = await _client.GetAsync("/api/branches");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<ListBranchesResponse>>();
        responseData.Should().NotBeNull();
        responseData.Data.Data.Should().HaveCountGreaterThan(0);
    }

    [Fact(DisplayName = "GET /api/branches/{id} - Deve retornar a branch correta")]
    public async Task GetBranch_ShouldReturn_CorrectBranch()
    {
        var newBranch = BranchRepositoryTestData.GenerateValidEntity();
        var createResponse = await _client.PostAsJsonAsync("/api/branches", new { Name = newBranch.Name });

        var createData = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateBranchResponse>>();
        var createdBranchId = createData.Data.Id;

        var response = await _client.GetAsync($"/api/branches/{createdBranchId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetBranchResponse>>();
        responseData.Should().NotBeNull();
        responseData.Data.Id.Should().Be(createdBranchId);
        responseData.Data.Name.Should().Be(newBranch.Name);
    }

    [Fact(DisplayName = "PUT /api/branches/{id} - Deve atualizar a branch corretamente")]
    public async Task UpdateBranch_ShouldUpdate_BranchSuccessfully()
    {
        var newBranch = BranchRepositoryTestData.GenerateValidEntity();
        var createResponse = await _client.PostAsJsonAsync("/api/branches", new { Name = newBranch.Name });

        var createData = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateBranchResponse>>();
        var createdBranchId = createData.Data.Id;

        var updatedRequest = new { Name = "Updated Branch Name" };
        var updateResponse = await _client.PutAsJsonAsync($"/api/branches/{createdBranchId}", updatedRequest);

        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var updateData = await updateResponse.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateBranchResponse>>();
        updateData.Should().NotBeNull();
        updateData.Data.Id.Should().Be(createdBranchId);
        updateData.Data.Name.Should().Be("Updated Branch Name");
    }

    [Fact(DisplayName = "DELETE /api/branches/{id} - Deve remover a branch corretamente")]
    public async Task DeleteBranch_ShouldDelete_BranchSuccessfully()
    {
        var newBranch = BranchRepositoryTestData.GenerateValidEntity();
        var createResponse = await _client.PostAsJsonAsync("/api/branches", new { Name = newBranch.Name });

        var createData = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateBranchResponse>>();
        var createdBranchId = createData.Data.Id;

        var deleteResponse = await _client.DeleteAsync($"/api/branches/{createdBranchId}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var getResponse = await _client.GetAsync($"/api/branches/{createdBranchId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
