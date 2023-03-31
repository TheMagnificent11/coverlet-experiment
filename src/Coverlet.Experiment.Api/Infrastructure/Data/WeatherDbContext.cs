using Coverlet.Experiment.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coverlet.Experiment.Api.Infrastructure.Data;

public sealed class WeatherDbContext : DbContext
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
        : base(options)
    {
    }

    public DbSet<WeatherForecast> Forecasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ForecastConfiguration());
    }
}
