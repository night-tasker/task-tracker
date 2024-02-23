using NightTasker.Common.Core.Persistence;
using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.Repositories;

internal sealed class ProblemsRepository(ApplicationDbSet<Problem, Guid> dbSet)
    : BaseRepository<Problem, Guid>(dbSet), IProblemsRepository;