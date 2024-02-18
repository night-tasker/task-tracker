using NightTasker.TaskTracker.Core.Domain.DataTransferObjects.OrganizationUsers;

namespace NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Contracts;

public interface IOrganizationUsersService
{
    Task CreateOrganizationUser(
        Guid organizationId,
        CreateOrganizationUserDto createOrganizationUserDto, 
        CancellationToken cancellationToken);
}