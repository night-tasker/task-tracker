using MassTransit;
using MediatR;
using NightTasker.Common.Messaging.Events.Implementations.Organizations;
using NightTasker.TaskTracker.Core.Application.Features.Organizations.Commands.CreateOrganization;
using NightTasker.TaskTracker.Core.Domain.DataTransferObjects.OrganizationUsers;
using NightTasker.TaskTracker.Core.Domain.Enums;

namespace NightTasker.TaskTracker.Infrastructure.Messaging.Consumers.Events.Organizations;

public class OrganizationCreatedConsumer(ISender sender) : IConsumer<OrganizationCreated>
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    public Task Consume(ConsumeContext<OrganizationCreated> context)
    {
        var users = context.Message.Users
            .Select(x => new CreateOrganizationUserDto(x.UserId, (OrganizationUserRole)x.Role))
            .ToArray();
        var command = new CreateOrganizationCommand(context.Message.Id, users);
        return _sender.Send(command, context.CancellationToken);
    }
}