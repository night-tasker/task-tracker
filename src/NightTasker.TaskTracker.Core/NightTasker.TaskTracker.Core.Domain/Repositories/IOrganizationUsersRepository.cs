using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.TaskTracker.Core.Domain.Entities;

namespace NightTasker.TaskTracker.Core.Domain.Repositories;

public interface IOrganizationUsersRepository : IRepository<OrganizationUser, Guid>
{
    
}