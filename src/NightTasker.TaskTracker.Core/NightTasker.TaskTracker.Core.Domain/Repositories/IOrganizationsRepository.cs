using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.TaskTracker.Core.Domain.Entities;

namespace NightTasker.TaskTracker.Core.Domain.Repositories;

public interface IOrganizationsRepository : IRepository<Organization, Guid>
{
    Task<Organization?> TryGetById(
        Guid organizationId, 
        bool trackChanges,
        CancellationToken cancellationToken);
}