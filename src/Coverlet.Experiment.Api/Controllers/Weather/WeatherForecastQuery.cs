using Coverlet.Experiment.Api.Infrastructure.Data;
using Coverlet.Experiment.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coverlet.Experiment.Api.Controllers.Weather;

public sealed class WeatherForecastQuery : IRequest<WeatherForecast[]>
{
    internal class WeatherForecastQueryHandler : IRequestHandler<WeatherForecastQuery, WeatherForecast[]>
    {
        private readonly WeatherDbContext dbContext;

        public WeatherForecastQueryHandler(WeatherDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<WeatherForecast[]> Handle(WeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var result = await this.dbContext
                .Forecasts
                .ToArrayAsync(cancellationToken);

            return result;
        }
    }
}
