using Testcontainers.PostgreSql;

namespace EndToEndTests.Fixture;

public class DatabaseFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container =
        new PostgreSqlBuilder()
            .WithPortBinding(5432, 5432)
            .Build();

    public string ConnectionString => _container.GetConnectionString();
    
    public Task InitializeAsync()
    {
        return _container.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _container.DisposeAsync().AsTask();
    }
}