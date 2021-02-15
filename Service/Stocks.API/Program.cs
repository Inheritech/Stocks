using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Stocks.API.Extensions;
using Stocks.Infrastructure;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Stocks.API {
    public class Program {

        public static string EnvironmentName { get; } = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT)") ?? Environments.Production;

        public static IConfiguration BaseConfiguration { get; } = GetBaseConfiguration();

        public static async Task<int> Main(string[] args) {

            Log.Logger = CreateDefaultSerilogLogger(BaseConfiguration);

            try {
                Log.Information("Configuring Stocks API host...");
                var host = CreateHostBuilder(args).Build();

                Log.Information("Applying migrations...");
                host.MigrateDbContext<StocksContext>();

                Log.Information("Starting Stocks API host...");
                await host.RunAsync();

                return 0;
            } catch (Exception ex) {
                Log.Fatal(ex, "Service terminated unexpectedly.");
                return 1;
            } finally {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.CaptureStartupErrors(false);
                    webBuilder.UseStartup<Startup>();
                });

        private static IConfiguration GetBaseConfiguration()
            => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        private static Serilog.ILogger CreateDefaultSerilogLogger(IConfiguration configuration)
            => new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationContext", "Stocks.API")
                .WriteTo.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss}] ({Level:u3}) {Message:lj} {NewLine}{Exception}"
                )
                .CreateLogger();

    }
}
