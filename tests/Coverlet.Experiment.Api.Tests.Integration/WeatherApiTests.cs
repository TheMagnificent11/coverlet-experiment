using Coverlet.Experiment.Api.Controllers.Weather;
using Coverlet.Experiment.Domain;
using FluentAssertions;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;

namespace Coverlet.Experiment.Api.Tests.Integration;

public sealed class WeatherApiTests : WeatherApiTestsBase
{
    private const string ApiPath = "/weather-forecasts";

    public WeatherApiTests(WeatherWebApplicationFactory factory)
        : base(factory)
    {
    }

    private WeatherForecast[] Forecasts { get; set; }

    [BddfyFact]
    public void ShouldGetCorrectForecastsAfterAddingForecasts()
    {
        var forecasts = Enumerable
            .Range(1, 3)
            .Select(x => DateOnly.FromDateTime(DateTime.Today.AddDays(x)))
            .Select(x => new WeatherForecast(x, Random.Shared.Next(-20, 55)))
            .ToArray();

        this.Given(x => x.AHealthyApplication())
                .And(x => x.AForecastIsAddedOrUpdated(forecasts[0].Date, forecasts[0].TemperatureC))
                .And(x => x.AForecastIsAddedOrUpdated(forecasts[1].Date, forecasts[1].TemperatureC))
                .And(x => x.AForecastIsAddedOrUpdated(forecasts[2].Date, forecasts[2].TemperatureC))
            .When(x => x.TheForecastIsLookedUp())
            .Then(x => x.TheForecastShouldBe(forecasts));
    }

    [BddfyFact]
    public void ShouldCorrectUpdateAForecastWhenTheDateIsTheSame()
    {
        var date = DateOnly.FromDateTime(DateTime.Today);
        var finalTempC = 25;

        this.Given(x => x.AHealthyApplication())
                .And(x => x.AForecastIsAddedOrUpdated(date, finalTempC + 3))
                .And(x => x.AForecastIsAddedOrUpdated(date, finalTempC))
            .When(x => x.TheForecastIsLookedUp())
            .Then(x => x.TheForecastShouldBe(new[] { new WeatherForecast(date, finalTempC) }));
    }

    private async Task AForecastIsAddedOrUpdated(DateOnly date, int temperatureC)
    {
        var request = new WeatherForecastRequest { Date = date, TemperatureC = temperatureC };

        using (var response = await this.HttpRequest(HttpMethod.Put, ApiPath, request))
        {
            response.EnsureSuccessStatusCode();
        }
    }

    private async Task TheForecastIsLookedUp()
    {
        this.Forecasts = await this.HttpGet<WeatherForecast[]>(ApiPath);
    }

    private void TheForecastShouldBe(WeatherForecast[] expectedForecasts)
    {
        this.Forecasts.Should().NotBeEmpty();
        this.Forecasts.Should().HaveCount(expectedForecasts.Length);

        for (var i = 0; i < expectedForecasts.Length; i++)
        {
            var actual = this.Forecasts[i];
            var expected = expectedForecasts[i];

            actual.Should().NotBeNull();
            actual.Date.Should().Be(expected.Date);
            actual.TemperatureC.Should().Be(expected.TemperatureC);
            actual.Summary.Should().Be(expected.Summary);
        }
    }
}
