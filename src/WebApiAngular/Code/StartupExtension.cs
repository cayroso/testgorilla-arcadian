using Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace WebApiAngular.Code
{
    public static class StartupExtension
    {
        public static async Task<IServiceProvider> CheckAndUpdateDatabase(this IServiceProvider rootServiceProvider)
        {
            using var scope = rootServiceProvider.CreateScope();

            var serviceProvider = scope.ServiceProvider;
            var env = serviceProvider.GetRequiredService<IHostEnvironment>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            try
            {
                var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }

                await AppDbContextInitializer.Initialize(dbContext);

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
                throw;
            }

            return serviceProvider;
        }
    }
}
