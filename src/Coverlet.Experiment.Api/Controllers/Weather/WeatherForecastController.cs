using Coverlet.Experiment.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coverlet.Experiment.Api.Controllers.Weather;

[ApiController]
[Route("weather-forecast")]
public sealed class WeatherForecastController : ControllerBase
{
    private readonly IMediator mediator;

    public WeatherForecastController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<WeatherForecast[]> Get(CancellationToken cancellationToken = default)
    {
        var request = new WeatherForecastQuery();

        var result = await this.mediator.Send(request, cancellationToken);

        return result;
    }
}
