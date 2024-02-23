using Microsoft.Extensions.DependencyInjection;
using NightTasker.TaskTracker.Core.Application.Features.Organizations.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Organizations.Implementations;
using NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Implementations;
using NightTasker.TaskTracker.Core.Application.Features.Problems.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Problems.Services;
using NightTasker.TaskTracker.Core.Application.Features.Users.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Users.Implementations;

namespace NightTasker.TaskTracker.Core.Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        });

        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IOrganizationsService, OrganizationsService>();
        services.AddScoped<IOrganizationUsersService, OrganizationUsersService>();
        services.AddScoped<IProblemsService, ProblemsService>();
        
        return services;
    }
}