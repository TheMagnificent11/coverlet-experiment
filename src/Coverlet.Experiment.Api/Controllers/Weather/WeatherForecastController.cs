using Coverlet.Experiment.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coverlet.Experiment.Api.Controllers.Weather;

[ApiController]
[Route("weather-forecasts")]
public sealed class WeatherForecastController : ControllerBase
{
    private readonly IMediator mediator;

    public WeatherForecastController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(WeatherForecast[]))]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        var query = new WeatherForecastQuery();

        var result = await this.mediator.Send(query, cancellationToken);

        return this.Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Put(
        [FromBody] WeatherForecastRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateOrUpdateForecastCommand(request.Date, request.TemperatureC);

        await this.mediator.Send(command, cancellationToken);

        return this.Ok();
    }
}
