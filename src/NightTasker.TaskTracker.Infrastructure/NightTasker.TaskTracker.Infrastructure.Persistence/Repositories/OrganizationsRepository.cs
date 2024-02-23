using Microsoft.EntityFrameworkCore;
using NightTasker.Common.Core.Persistence;
using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.Repositories;

internal sealed class OrganizationsRepository(ApplicationDbSet<Organization, Guid> dbSet)
    : BaseRepository<Organization, Guid>(dbSet), IOrganizationsRepository
{
    public Task<Organization?> TryGetById(
        Guid organizationId, 
        bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = Entities;
        
        if (!trackChanges)
        {
            query = query.AsNoTracking();
        }
        
        return query
            .SingleOrDefaultAsync(x => x.Id == organizationId, cancellationToken);
    }
    
    public Task<bool> CheckOrganizationExists(
        Guid organizationId, 
        CancellationToken cancellationToken)
    {
        return Entities
            .AnyAsync(x => x.Id == organizationId, cancellationToken);
    }
}