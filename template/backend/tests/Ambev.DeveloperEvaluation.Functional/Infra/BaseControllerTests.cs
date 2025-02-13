using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Infra
{
    public class BaseControllerTests : IClassFixture<FunctionalDatabaseFixture>
    {
        protected readonly HttpClient _client;

        public BaseControllerTests(FunctionalDatabaseFixture fixture)
        {
            var factory = new CustomWebApplicationFactory(fixture.ConnectionString);
            _client = factory.CreateClient();

            var token = fixture.TokenGenerator.GenerateToken(fixture.TestUser);
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}