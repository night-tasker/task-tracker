using MediatR;
using NightTasker.TaskTracker.Core.Application.Features.Users.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Users.Models;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Core.Application.Features.Users.Commands.CreateUser;

internal sealed class AddOrganizationUserCommandHandler(
    IUsersService usersService,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand>
{
    private readonly IUsersService _usersService = 
        usersService ?? throw new ArgumentNullException(nameof(usersService));
    private readonly IUnitOfWork _unitOfWork = 
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _usersService.CreateUser(new CreateUserDto(request.Id, request.UserName), cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
    }
}