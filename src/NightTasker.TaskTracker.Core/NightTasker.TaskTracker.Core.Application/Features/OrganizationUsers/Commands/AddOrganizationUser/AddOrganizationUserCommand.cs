using MediatR;
using NightTasker.TaskTracker.Core.Domain.Enums;

namespace NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Commands.AddOrganizationUser;

public sealed record AddOrganizationUserCommand(
    Guid OrganizationUserId, Guid OrganizationId, Guid UserId, OrganizationUserRole Role) : IRequest;