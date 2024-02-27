using NightTasker.TaskTracker.Core.Domain.Entities;

namespace NightTasker.TaskTracker.Core.Domain.Repositories;

public interface IProblemsReadRepository
{
    Task<IReadOnlyCollection<Problem>> GetProblemsAssignedOnUser(
        Guid userId, CancellationToken cancellationToken);
}