using MassTransit;
using MediatR;
using NightTasker.Common.Messaging.Events.Contracts.Users;
using NightTasker.TaskTracker.Core.Application.Features.Users.Commands.CreateUser;

namespace NightTasker.TaskTracker.Infrastructure.Messaging.Consumers.Events.Users;

public sealed class UserRegisteredConsumer(ISender sender) : IConsumer<IUserRegistered>
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    public Task Consume(ConsumeContext<IUserRegistered> context)
    {
        var command = new CreateUserCommand(context.Message.Id, context.Message.UserName);
        return _sender.Send(command, context.CancellationToken);
    }
}