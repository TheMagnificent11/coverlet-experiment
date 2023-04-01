using System.Text.Json;
using Coverlet.Experiment.Domain;
using FluentAssertions;
using Xunit;

namespace Coverlet.Experiment.Api.Tests.Integration;

[Collection(nameof(WeatherCollection))]
public class WeatherForecastApiTests : IClassFixture<WeatherWebApplicationFactory>
{
    private readonly WeatherWebApplicationFactory factory;

    public WeatherForecastApiTests(WeatherWebApplicationFactory factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task GetForecasts()
    {
        using (var request = new HttpRequestMessage(HttpMethod.Get, "/weather-forecast"))
        using (var httpClient = this.factory.CreateClient())
        using (var response = await httpClient.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            json.Should().NotBeNull();

            var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(json);
            forecasts.Should().NotBeNull();
            forecasts.Should().HaveCount(5);
        }
    }
}
