using MediatR;
using NightTasker.TaskTracker.Core.Domain.DataTransferObjects.OrganizationUsers;

namespace NightTasker.TaskTracker.Core.Application.Features.Organizations.Commands.CreateOrganization;

public sealed record CreateOrganizationCommand(
    Guid Id, IReadOnlyCollection<CreateOrganizationUserDto> Users) : IRequest;