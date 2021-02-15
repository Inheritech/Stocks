using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stocks.Infrastructure;
using System;

namespace Stocks.API {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            string connectionString = Configuration.GetConnectionString("StocksDb");

            services.AddDbContext<StocksContext>(options => {
                options.UseSqlServer(connectionString, sqlOpts => {
                    sqlOpts.MigrationsHistoryTable("_MigrationsHistory", StocksContext.DefaultSchema);
                    sqlOpts.MigrationsAssembly(typeof(StocksContext).Assembly.GetName().Name);
                    sqlOpts.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
            }, ServiceLifetime.Scoped);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
