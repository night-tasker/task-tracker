using Microsoft.Extensions.DependencyInjection;

namespace NightTasker.TaskTracker.Core.Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        });
        
        return services;
    }
}