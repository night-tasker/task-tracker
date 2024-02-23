using NightTasker.Common.Core.Abstractions;
using NightTasker.TaskTracker.Core.Domain.Core.Primitives;
using NightTasker.TaskTracker.Core.Domain.DataTransferObjects.OrganizationUsers;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Core.Domain.Entities;

public class Organization : AggregateRoot, IEntityWithId<Guid>, IDateTimeOffsetModification
{
    private Organization(Guid id)
    {
        Id = id;
    }

    public static Organization CreateInstance(
        Guid id)
    {
        return new Organization(id);
    }
    
    public Guid Id { get; private set; }
        
    public IReadOnlyCollection<OrganizationUser> OrganizationUsers { get; } = null!;

    public IReadOnlyCollection<Problem> Problems { get; set; }
    
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    
    public DateTimeOffset? UpdatedDateTimeOffset { get; set; }


    public Task AddUsers(
        IReadOnlyCollection<CreateOrganizationUserDto> users,
        IOrganizationUsersRepository organizationUsersRepository,
        CancellationToken cancellationToken)
    {
        var organizationUsers = users
            .Select(x => OrganizationUser.CreateInstance(Id, x.UserId, x.Role))
            .ToArray();
        return organizationUsersRepository.AddRange(organizationUsers, cancellationToken);
    }
}