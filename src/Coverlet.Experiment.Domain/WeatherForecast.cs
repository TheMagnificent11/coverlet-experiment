namespace Coverlet.Experiment.Domain;

public class WeatherForecast
{
    public WeatherForecast(DateOnly date, int temperatureC)
        : this()
    {
        this.Date = date;
        this.TemperatureC = temperatureC;
    }

    // EF constructor
    private WeatherForecast()
    {
    }

    public DateOnly Date { get; protected set; }

    public int TemperatureC { get; protected set; }

    public int TemperatureF => 32 + this.TemperatureC * 9 / 5;

    public string Summary
    {
        get
        {
            var summary = this.TemperatureC switch
            {
                int n when n <= 0 => TemperatureSummary.Freezing,
                int n when n > 0 && n <= 5 => TemperatureSummary.Bracing,
                int n when n > 5 && n <= 10 => TemperatureSummary.Chilly,
                int n when n > 10 && n <= 15 => TemperatureSummary.Cool,
                int n when n > 15 && n <= 20 => TemperatureSummary.Mild,
                int n when n > 20 && n <= 25 => TemperatureSummary.Warm,
                int n when n > 25 && n <= 30 => TemperatureSummary.Balmy,
                int n when n > 30 && n <= 35 => TemperatureSummary.Hot,
                int n when n > 35 && n <= 40 => TemperatureSummary.Sweltering,
                int n when n > 40 => TemperatureSummary.Scorching,
                _ => TemperatureSummary.Unknown,
            };

            return summary.ToString();
        }
    }

    public void UpdateTemperature(int newTemperatureC)
    {
        if (this.TemperatureC == newTemperatureC)
        {
            return;
        }

        this.TemperatureC = newTemperatureC;
    }
}
