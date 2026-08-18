
using Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace Infrastructure
{   
    //Extionsion method for dependency injection
    public static class StartUp
    {
        public static IServiceCollection AddInfrastructureServices(
         this IServiceCollection services,
         IConfiguration config)
        {

            // Get connection string and validate it exists
            var connectionString = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found in configuration.");
            }


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, builder =>
                    {
                        //remove underscore from migrations
                        builder.MigrationsHistoryTable("Migrations","EFCore");
                    });
            });

            return services;
        }
    }
}
