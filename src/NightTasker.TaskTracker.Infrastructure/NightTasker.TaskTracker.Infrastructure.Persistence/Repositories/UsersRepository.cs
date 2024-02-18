using Microsoft.EntityFrameworkCore;
using NightTasker.Common.Core.Persistence;
using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.Repositories;

internal sealed class UsersRepository(ApplicationDbSet<User, Guid> dbSet) : BaseRepository<User, Guid>(dbSet), IUsersRepository
{
    public Task<bool> CheckUserExists(Guid userId, CancellationToken cancellationToken)
    {
        return Entities.AnyAsync(x => x.Id == userId, cancellationToken);
    }
}