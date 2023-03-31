using Coverlet.Experiment.Domain;
using FluentAssertions;
using Xunit;

namespace Coverlet.Experiment.Domin.Tests.Unit;

public static class WeatherForecastTests
{
    [Theory]
    [InlineData(-5, TemperatureSummary.Freezing)]
    [InlineData(0, TemperatureSummary.Freezing)]
    [InlineData(5, TemperatureSummary.Bracing)]
    [InlineData(10, TemperatureSummary.Chilly)]
    [InlineData(15, TemperatureSummary.Cool)]
    [InlineData(20, TemperatureSummary.Mild)]
    [InlineData(25, TemperatureSummary.Warm)]
    [InlineData(30, TemperatureSummary.Balmy)]
    [InlineData(35, TemperatureSummary.Hot)]
    [InlineData(40, TemperatureSummary.Sweltering)]
    [InlineData(45, TemperatureSummary.Scorching)]
    public static void Summary(int temperatureC, TemperatureSummary expectedSummary)
    {
        var forecast = new WeatherForecast(DateOnly.FromDateTime(DateTime.Today), temperatureC);
        forecast.Summary.Should().Be(expectedSummary);
    }
}
