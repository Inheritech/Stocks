using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;

namespace Stocks.API.Extensions {
    public static class IHostExtensions {

        public static IHost MigrateDbContext<TDbContext>(this IHost host)
            where TDbContext : DbContext {
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TDbContext>>();
                var context = services.GetRequiredService<TDbContext>();

                try {
                    logger.LogInformation("Migrating database associated with context {DbContextName}.", typeof(TDbContext).Name);

                    var retries = 10;
                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(
                            retryCount: retries,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            onRetry: (exception, timeSpan, retry, ctx) => {
                                logger.LogWarning(
                                    exception,
                                    "[{DbContext}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries} while migrating database",
                                    typeof(TDbContext).Name,
                                    exception.GetType().Name,
                                    exception.Message,
                                    retry,
                                    retries
                                );
                            });

                    retry.Execute(() => context.Database.Migrate());

                    logger.LogInformation("Finished migrating database associated with context {DbContextName}", typeof(TDbContext).Name);
                } catch (Exception ex) {
                    logger.LogError(ex, "An error occured while migrating the database used on context {DbContextName}", typeof(TDbContext).Name);
                }
            }

            return host;
        }

    }
}
