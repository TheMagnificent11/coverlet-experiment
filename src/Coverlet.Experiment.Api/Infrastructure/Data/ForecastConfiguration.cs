using Coverlet.Experiment.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coverlet.Experiment.Api.Infrastructure.Data;

public sealed class ForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.HasKey(x => x.Date);
        builder.Ignore(x => x.TemperatureF);
        builder.Ignore(x => x.Summary);
    }
}
