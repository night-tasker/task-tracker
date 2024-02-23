using NightTasker.Common.Core.Abstractions;
using NightTasker.TaskTracker.Core.Domain.Core.Primitives;
using NightTasker.TaskTracker.Core.Domain.Enums;

namespace NightTasker.TaskTracker.Core.Domain.Entities;

public class OrganizationUser : AggregateRoot, IEntity, IDateTimeOffsetModification
{
    private OrganizationUser(Guid organizationId, Guid userId, OrganizationUserRole role)
    {
        OrganizationId = organizationId;
        UserId = userId;
        Role = role;
    }
    
    public static OrganizationUser CreateInstance(
        Guid organizationId,
        Guid userId,
        OrganizationUserRole role)
    {
        return new OrganizationUser(organizationId, userId, role);
    }
    
    public Guid OrganizationId { get; private set; }

    public Organization Organization { get; private set; } = null!;
    
    public Guid UserId { get; private set; }

    public User User { get; private set; } = null!;
    
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    
    public DateTimeOffset? UpdatedDateTimeOffset { get; set; }
    
    public OrganizationUserRole Role { get; private set; }
}