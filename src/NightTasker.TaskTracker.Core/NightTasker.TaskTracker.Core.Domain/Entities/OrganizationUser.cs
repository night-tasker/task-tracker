using NightTasker.Common.Core.Abstractions;
using NightTasker.TaskTracker.Core.Domain.Enums;
using NightTasker.TaskTracker.Core.Domain.Primitives;

namespace NightTasker.TaskTracker.Core.Domain.Entities;

public class OrganizationUser : AggregateRoot, IEntityWithId<Guid>, IDateTimeOffsetModification
{
    private OrganizationUser(Guid id, Guid organizationId, Guid userId, OrganizationUserRole role)
    {
        Id = id;
        OrganizationId = organizationId;
        UserId = userId;
        Role = role;
    }
    
    public static OrganizationUser CreateInstance(
        Guid id,
        Guid organizationId,
        Guid userId,
        OrganizationUserRole role)
    {
        return new OrganizationUser(id, organizationId, userId, role);
    }
    
    public Guid Id { get; private set; }
    
    public Guid OrganizationId { get; private set; }

    public Organization Organization { get; private set; } = null!;
    
    public Guid UserId { get; private set; }

    public User User { get; private set; } = null!;
    
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    
    public DateTimeOffset? UpdatedDateTimeOffset { get; set; }
    
    public OrganizationUserRole Role { get; private set; }
}