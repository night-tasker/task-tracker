namespace NightTasker.TaskTracker.Core.Domain.Repositories;

public interface IUnitOfWork
{
    IUsersRepository UsersRepository { get; }
    
    IOrganizationsRepository OrganizationsRepository { get; }
    
    IOrganizationUsersRepository OrganizationUsersRepository { get; }

    Task SaveChanges(CancellationToken cancellationToken);
}