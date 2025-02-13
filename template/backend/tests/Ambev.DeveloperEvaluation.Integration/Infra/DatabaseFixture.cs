using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

public class DatabaseFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer;
    public string ConnectionString { get; private set; } = null!;

    public DatabaseFixture()
    {
        _postgresContainer = new PostgreSqlBuilder()
            .WithDatabase("testdb")
            .WithUsername("testuser")
            .WithPassword("testpassword")
            .WithCleanUp(true) 
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();
        ConnectionString = _postgresContainer.GetConnectionString();

        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        using var tempContext = new DefaultContext(options);
        await tempContext.Database.EnsureCreatedAsync(); 
    }

    public async Task DisposeAsync()
    {
        await _postgresContainer.StopAsync(); 
    }
}
