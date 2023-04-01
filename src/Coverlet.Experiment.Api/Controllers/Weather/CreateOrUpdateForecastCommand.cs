using Coverlet.Experiment.Api.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coverlet.Experiment.Api.Controllers.Weather;

public sealed class CreateOrUpdateForecastCommand : IRequest
{
    public CreateOrUpdateForecastCommand(DateOnly date, int temperatureC)
    {
        this.Date = date;
        this.TemperatureC = temperatureC;
    }

    public DateOnly Date { get; }
    public int TemperatureC { get; }

    internal class Handler : IRequestHandler<CreateOrUpdateForecastCommand>
    {
        private readonly WeatherDbContext dbContext;

        public Handler(WeatherDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(CreateOrUpdateForecastCommand request, CancellationToken cancellationToken)
        {
            var existing = await this.dbContext
                .Forecasts
                .FirstOrDefaultAsync(x => x.Date == request.Date, cancellationToken);

            if (existing == null)
            {
                this.dbContext.Forecasts.Add(new Domain.WeatherForecast(request.Date, request.TemperatureC));
            }
            else
            {
                existing.UpdateTemperature(request.TemperatureC);
            }

            await this.dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
