using InstrumentPlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace InstrumentPlatform.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IApplicationBuilder"/> related to application startup tasks.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Applies any pending database migrations at application startup.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> instance used to configure the application.</param>
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationBuilder>>();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                db.Database.Migrate();
                logger.LogInformation("Database is up to date.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Database migration failed: {ex.Message}");
            }
        }
    }
}
