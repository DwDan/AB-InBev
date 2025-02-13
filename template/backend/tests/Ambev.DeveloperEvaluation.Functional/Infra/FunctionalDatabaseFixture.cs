using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;
using Xunit;

public class FunctionalDatabaseFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer;
    public string ConnectionString { get; private set; } = null!;
    public User TestUser { get; private set; } = null!;
    public JwtTokenGenerator TokenGenerator { get; private set; } = null!; 

    public FunctionalDatabaseFixture()
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

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        TokenGenerator = new JwtTokenGenerator(config);

        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        using var tempContext = new DefaultContext(options);
        await tempContext.Database.MigrateAsync(); 

        var passwordHasher = new BCryptPasswordHasher();
        TestUser = UserRepositoryTestData.GenerateValidEntity();
        TestUser.Password = passwordHasher.HashPassword(TestUser.Password);

        tempContext.Users.Add(TestUser);
        await tempContext.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgresContainer.StopAsync(); 
    }
}
