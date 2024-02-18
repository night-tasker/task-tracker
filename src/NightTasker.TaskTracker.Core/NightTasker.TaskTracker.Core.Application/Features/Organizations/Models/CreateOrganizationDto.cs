using NightTasker.TaskTracker.Core.Domain.DataTransferObjects.OrganizationUsers;

namespace NightTasker.TaskTracker.Core.Application.Features.Organizations.Models;

public sealed record CreateOrganizationDto(Guid Id, IReadOnlyCollection<CreateOrganizationUserDto>? Users);