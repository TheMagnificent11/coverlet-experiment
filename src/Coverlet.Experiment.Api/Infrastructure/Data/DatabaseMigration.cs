using Microsoft.EntityFrameworkCore;

namespace Coverlet.Experiment.Api.Infrastructure.Data;

public static class DatabaseMigration
{
    public static async Task MigrationDatabase<T>(this IApplicationBuilder app)
        where T : DbContext
    {
        var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            ?? throw new InvalidOperationException("Could not find IServiceScopeFactory");

        using (var serviceScope = service.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<T>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
