using NightTasker.Common.Core.Persistence;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.Repositories;

internal sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    public IUsersRepository UsersRepository { get; init; } = 
        new UsersRepository(new ApplicationDbSet<User, Guid>(dbContext));

    public IOrganizationsRepository OrganizationsRepository { get; } =
        new OrganizationsRepository(new ApplicationDbSet<Organization, Guid>(dbContext));
        
    
    public IOrganizationUsersRepository OrganizationUsersRepository { get; } = 
        new OrganizationUsersRepository(new ApplicationDbSet<OrganizationUser, Guid>(dbContext));

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}