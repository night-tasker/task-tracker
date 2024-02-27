using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NightTasker.TaskTracker.Core.Domain.Repositories;
using NightTasker.TaskTracker.Infrastructure.Persistence.Contracts;
using NightTasker.TaskTracker.Infrastructure.Persistence.Implementations;
using NightTasker.TaskTracker.Infrastructure.Persistence.Repositories;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterPersistenceServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options)=>
        {
            options
                .UseNpgsql(configuration.GetConnectionString("Database"));
        });
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        ConfigureDapper();
        return services;
    }

    private static void ConfigureDapper()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}