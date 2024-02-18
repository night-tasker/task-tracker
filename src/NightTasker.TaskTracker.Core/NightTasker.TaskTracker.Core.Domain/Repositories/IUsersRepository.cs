using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.TaskTracker.Core.Domain.Entities;

namespace NightTasker.TaskTracker.Core.Domain.Repositories;

public interface IUsersRepository : IRepository<User, Guid>
{
    Task<bool> CheckUserExists(Guid userId, CancellationToken cancellationToken);
}