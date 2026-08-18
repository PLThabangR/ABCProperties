
using Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{   
    //Extionsion method for dependency injection
    public static class StartUp
    {
        public static IServiceCollection AddInfrastructureServices(
         this IServiceCollection services,
         IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"), builder =>
                    {
                        //remove underscore from migrations
                        builder.MigrationsHistoryTable("Migrations","EFCore");
                    });
            });

            return services;
        }
    }
}
