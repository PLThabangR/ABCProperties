using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    //This class house our extension methods for to be used for dependency injection
    // Contains extension methods to register Application layer services with the DI container
    public static class StartUp
    {
        //1....this IServiceCollection services - Extends the IServiceCollection type

      //2....AddApplicationServices - Naming convention: Add[FeatureName] Services

//3....Returns: IServiceCollection for method chaining
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Application assembly (where your commands, queries, and handlers live)

           // This is used to scan for MediatR handlers (IRequestHandler implementations)

           var assembly = Assembly.GetExecutingAssembly();
            //Adds MediatR to the DI container
            return services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
            });
        }

    }
}
