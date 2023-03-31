namespace Coverlet.Experiment.Api.Controllers.Weather;

public class WeatherForecastRequest
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
}
