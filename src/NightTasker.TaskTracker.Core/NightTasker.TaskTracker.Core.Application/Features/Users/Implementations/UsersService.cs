using NightTasker.TaskTracker.Core.Application.Features.Users.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Users.Models;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Core.Application.Features.Users.Implementations;

internal sealed class UsersService(IUnitOfWork unitOfWork) : IUsersService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public Task CreateUser(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        var user = User.CreateInstance(createUserDto.Id, createUserDto.UserName);
        return _unitOfWork.UsersRepository.Add(user, cancellationToken);
    }
}