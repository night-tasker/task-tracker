using NightTasker.Common.Core.Persistence;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Repositories;
using NightTasker.TaskTracker.Infrastructure.Persistence.Contracts;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.Repositories;

internal sealed class UnitOfWork(
    ApplicationDbContext dbContext,
    ISqlConnectionFactory sqlConnectionFactory) : IUnitOfWork
{
    public IUsersRepository UsersRepository { get; init; } = 
        new UsersRepository(new ApplicationDbSet<User, Guid>(dbContext));

    public IOrganizationsRepository OrganizationsRepository { get; } =
        new OrganizationsRepository(new ApplicationDbSet<Organization, Guid>(dbContext));
    
    public IOrganizationUsersRepository OrganizationUsersRepository { get; } = 
        new OrganizationUsersRepository(new ApplicationDbSet<OrganizationUser, Guid>(dbContext));

    public IProblemsRepository ProblemsRepository { get; } =
        new ProblemsRepository(new ApplicationDbSet<Problem, Guid>(dbContext));

    public IProblemsReadRepository ProblemsReadRepository { get; } =
        new ProblemsReadRepository(sqlConnectionFactory);

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}