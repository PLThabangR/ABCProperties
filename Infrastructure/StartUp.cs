using Application.feature.Agents;
using Infrastructure.Contexts;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    // Extension method for registering Infrastructure services
    public static class StartUp
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration config)
        {
            // Get the connection string from appsettings.json
            var connectionString = config.GetConnectionString("DefaultConnection");

            // Make sure the connection string exists
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found in configuration.");
            }

            // Register ApplicationDbContext with Dependency Injection
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    // Store EF Core migrations history in a custom table/schema
                    sqlOptions.MigrationsHistoryTable("Migrations", "EFCore");

                    // Retry the SQL Server operation if a temporary connection
                    // failure occurs
                    sqlOptions.EnableRetryOnFailure();
                });
            });

            // Register AgentService with Dependency Injection
            services.AddScoped<IAgentService, AgentService>();

            return services;
        }
    }
}