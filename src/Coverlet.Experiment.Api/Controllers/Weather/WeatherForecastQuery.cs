using Coverlet.Experiment.Domain;
using MediatR;

namespace Coverlet.Experiment.Api.Controllers.Weather;

public class WeatherForecastQuery : IRequest<WeatherForecast[]>
{
    internal class WeatherForecastQueryHandler : IRequestHandler<WeatherForecastQuery, WeatherForecast[]>
    {
        public Task<WeatherForecast[]> Handle(WeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var result = Enumerable
                .Range(1, 5)
                .Select(x => DateOnly.FromDateTime(DateTime.Today.AddDays(x)))
                .Select(x => new WeatherForecast(x, Random.Shared.Next(-20, 55)))
                .ToArray();

            return Task.FromResult(result);
        }
    }
}
