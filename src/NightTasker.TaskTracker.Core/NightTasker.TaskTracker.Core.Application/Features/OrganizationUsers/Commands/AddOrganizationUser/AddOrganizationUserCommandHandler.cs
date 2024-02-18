using MediatR;
using NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Contracts;
using NightTasker.TaskTracker.Core.Domain.DataTransferObjects.OrganizationUsers;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Commands.AddOrganizationUser;

internal sealed class AddOrganizationUserCommandHandler(
    IUnitOfWork unitOfWork,
    IOrganizationUsersService organizationUsersService)
    : IRequestHandler<AddOrganizationUserCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IOrganizationUsersService _organizationUsersService = organizationUsersService
        ?? throw new ArgumentNullException(nameof(organizationUsersService));

    public async Task Handle(AddOrganizationUserCommand request, CancellationToken cancellationToken)
    {
        var createOrganizationUserDto = new CreateOrganizationUserDto(
            request.OrganizationUserId, request.UserId, request.Role);
        await _organizationUsersService.CreateOrganizationUser(
            request.OrganizationId, createOrganizationUserDto, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
    }
}