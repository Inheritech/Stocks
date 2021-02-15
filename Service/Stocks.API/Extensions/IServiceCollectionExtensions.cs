using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stocks.Domain.Common;
using System;

namespace Stocks.API.Extensions {
    public static class IServiceCollectionExtensions {
        public static IServiceCollection AddSqlDatabase<TContext>(this IServiceCollection services, string connectionString, string migrationsSchema = "dbo")
            where TContext : DbContext {
            services.AddDbContext<TContext>(options => {
                options.UseSqlServer(connectionString, sqlOpts => {
                    sqlOpts.MigrationsHistoryTable("_MigrationsHistory", migrationsSchema);
                    sqlOpts.MigrationsAssembly(typeof(TContext).Assembly.GetName().Name);
                    sqlOpts.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
            }, ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection AddDomainRepositories<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext {

            services.Scan(scan => {
                scan.FromAssemblyOf<TDbContext>()
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });

            return services;
        }
    }
}
