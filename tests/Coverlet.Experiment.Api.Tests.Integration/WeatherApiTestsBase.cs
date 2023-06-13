using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Coverlet.Experiment.Api.Tests.Integration;

public abstract class WeatherApiTestsBase : IClassFixture<WeatherWebApplicationFactory>
{
    private readonly WeatherWebApplicationFactory factory;

    protected WeatherApiTestsBase(WeatherWebApplicationFactory factory)
    {
        this.factory = factory;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.Testing.json")
            .Build();

        var connectionString = configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Could not find connection string");

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Forecasts";

            command.ExecuteNonQuery();
        }
    }

    protected static HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, string apiPath, object content)
    {
        var request = new HttpRequestMessage(httpMethod, apiPath);

        if (content != null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        }

        return request;
    }

    protected async Task<HttpResponseMessage> HttpRequest(HttpMethod method, string apiPath, object content = null)
    {
        using (var request = CreateHttpRequestMessage(method, apiPath, content))
        using (var httpClient = this.factory.CreateClient())
        {
            return await httpClient.SendAsync(request);
        }
    }

    protected virtual async Task<T> DeserializeResponse<T>(HttpResponseMessage response, bool isSuccess = true)
    {
        if (isSuccess)
        {
            response.EnsureSuccessStatusCode();
        }

        return await response.Content.ReadFromJsonAsync<T>(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }

    protected async Task<T> HttpGet<T>(string apiPath)
    {
        using (var response = await this.HttpRequest(HttpMethod.Get, apiPath))
        {
            return await this.DeserializeResponse<T>(response);
        }
    }

    protected async Task AHealthyApplication()
    {
        using (var response = await this.HttpRequest(HttpMethod.Get, "/health"))
        {
            response.EnsureSuccessStatusCode();

            var healthStatus = await response.Content.ReadAsStringAsync();

            healthStatus.Should().NotBeNull();
            healthStatus.Should().Be("Healthy");
        }
    }
}
