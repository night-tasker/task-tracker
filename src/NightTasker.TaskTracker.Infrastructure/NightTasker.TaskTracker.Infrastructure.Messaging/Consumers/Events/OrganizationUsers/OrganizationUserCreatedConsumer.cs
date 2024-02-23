using MassTransit;
using MediatR;
using NightTasker.Common.Messaging.Events.Implementations.OrganizationUsers;
using NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Commands.AddOrganizationUser;
using NightTasker.TaskTracker.Core.Domain.Enums;

namespace NightTasker.TaskTracker.Infrastructure.Messaging.Consumers.Events.OrganizationUsers;

public class OrganizationUserCreatedConsumer(ISender sender) : IConsumer<OrganizationUserCreated>
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    public Task Consume(ConsumeContext<OrganizationUserCreated> context)
    {
        var command = new AddOrganizationUserCommand(
            context.Message.OrganizationId, context.Message.UserId, (OrganizationUserRole) context.Message.Role);
        return _sender.Send(command, context.CancellationToken);
    }
}