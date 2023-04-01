using System.Text;
using System.Text.Json;
using FluentAssertions;
using Xunit;

namespace Coverlet.Experiment.Api.Tests.Integration;

[Collection(nameof(WeatherCollection))]
public abstract class WeatherApiTestsBase : IClassFixture<WeatherWebApplicationFactory>
{
    private readonly WeatherWebApplicationFactory factory;

    protected WeatherApiTestsBase(WeatherWebApplicationFactory factory)
    {
        this.factory = factory;
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

        var json = await response.Content.ReadAsStringAsync();
        if (json == null)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(json);
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
        var result = await this.HttpGet<string>("/health");

        result.Should().NotBeNull();
        result.Should().Be("Healthy");
    }
}
