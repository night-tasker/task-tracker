using NightTasker.TaskTracker.Core.Application.Features.Users.Models;

namespace NightTasker.TaskTracker.Core.Application.Features.Users.Contracts;

public interface IUsersService
{
    Task CreateUser(CreateUserDto createUserDto, CancellationToken cancellationToken);
}