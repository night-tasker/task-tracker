using NightTasker.Common.Core.Abstractions;
using NightTasker.TaskTracker.Core.Domain.Core.Primitives;

namespace NightTasker.TaskTracker.Core.Domain.Entities;

public class User : AggregateRoot, IEntityWithId<Guid>, IDateTimeOffsetModification
{
    private User(Guid id, string? userName)
    {
        Id = id;
        UserName = userName;
    }
    
    public static User CreateInstance(
        Guid id,
        string userName)
    {
        return new User(id, userName);
    }
    
    public Guid Id { get; private set; }

    public string? UserName { get; private set; }
    
    public IReadOnlyCollection<OrganizationUser> OrganizationUsers { get; } = null!;
    
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    
    public DateTimeOffset? UpdatedDateTimeOffset { get; set; }
}