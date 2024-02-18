using MediatR;

namespace NightTasker.TaskTracker.Core.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(Guid Id, string UserName) : IRequest;