using NightTasker.Common.Core.Exceptions.Base;

namespace NightTasker.TaskTracker.Core.Domain.Core.Exceptions.Organizations;

public class OrganizationNotFoundException(Guid organizationId) : NotFoundException(
    $"Organization with id {organizationId} not found");
