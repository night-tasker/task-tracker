namespace NightTasker.TaskTracker.Core.Domain.Repositories;

public interface IUnitOfWork
{
    IUsersRepository UsersRepository { get; }
    
    IOrganizationsRepository OrganizationsRepository { get; }
    
    IOrganizationUsersRepository OrganizationUsersRepository { get; }
    
    IProblemsRepository ProblemsRepository { get; }
    
    IProblemsReadRepository ProblemsReadRepository { get; }

    Task SaveChanges(CancellationToken cancellationToken);
}