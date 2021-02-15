using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Stocks.Domain.Common;
using System;
using System.IO;
using System.Reflection;

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

        public static IServiceCollection AddSwaggerGen(this IServiceCollection services) {
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Stocks HTTP API",
                    Version = "v1",
                    Description = "Stocks Service HTTP API"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath)) {
                    options.IncludeXmlComments(xmlPath);
                }
            });

            return services;
        }
    }
}
