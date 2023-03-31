using Coverlet.Experiment.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coverlet.Experiment;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("Default");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ApplicationException("Could not find database connection string");
        }

        builder.Services
            .AddDbContext<WeatherDbContext>(options => options.UseSqlite(connectionString))
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

        builder.Services.AddControllers();

        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}
