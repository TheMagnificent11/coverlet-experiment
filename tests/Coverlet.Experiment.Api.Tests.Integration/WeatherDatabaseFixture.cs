using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Coverlet.Experiment.Api.Tests.Integration;

public sealed class WeatherDatabaseFixture : IAsyncLifetime
{
    private readonly string connectionString;

    public WeatherDatabaseFixture()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.Testing.json")
            .Build();

        var connectionString = configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Could not find connection string");

        this.connectionString = connectionString;
    }

    public async Task InitializeAsync()
    {
        try
        {
            using (var connection = new SqliteConnection(this.connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Forecasts";

                await command.ExecuteNonQueryAsync();
            }
        }
        catch (SqliteException)
        {
            // Database doesn't exist yet, probably because migrations haven't been run to create it
            return;
        }
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
