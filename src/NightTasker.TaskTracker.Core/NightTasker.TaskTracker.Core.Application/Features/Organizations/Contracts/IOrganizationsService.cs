using NightTasker.TaskTracker.Core.Application.Features.Organizations.Models;

namespace NightTasker.TaskTracker.Core.Application.Features.Organizations.Contracts;

public interface IOrganizationsService
{
    Task CreateOrganization(CreateOrganizationDto createOrganizationDto, CancellationToken cancellationToken);
}