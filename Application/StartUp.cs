using Application.Pipeplines;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    // This class contains extension methods used to register
    // Application services in the Dependency Injection container.
    public static class StartUp
    {
        // This method allows us to write:
        //
        // builder.Services.AddApplicationServices();
        //
        // instead of registering everything one by one.
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            // Get the Application assembly.
            //
            // The assembly contains our:
            // - Commands
            // - Queries
            // - Handlers
            // - Validators
            //
            // We need the assembly so that MediatR and FluentValidation
            // know where to look for these classes.
            var assembly = Assembly.GetExecutingAssembly();

            // Register MediatR.
            //
            // MediatR will scan this assembly and automatically find
            // our command/query handlers.
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
            });

            // Register all FluentValidation validators
            // that are inside this assembly.
            services.AddValidatorsFromAssembly(assembly);

            // Register our validation pipeline.
            //
            // This allows validation to happen automatically
            // before a request reaches its handler.
            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationPipelineBehavior<,>)
            );

            // Return services so that we can continue chaining
            // other service registrations.
            return services;
        }
    }
}