using NightTasker.TaskTracker.Core.Application.Exceptions.NotFound;
using NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Contracts;
using NightTasker.TaskTracker.Core.Domain.DataTransferObjects.OrganizationUsers;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Implementations;

internal sealed class OrganizationUsersService(IUnitOfWork unitOfWork) : IOrganizationUsersService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task CreateOrganizationUser(
        Guid organizationId,
        CreateOrganizationUserDto createOrganizationUserDto, 
        CancellationToken cancellationToken)
    {
        var organization = await _unitOfWork.OrganizationsRepository.TryGetById(
            organizationId, true, cancellationToken);
        if(organization is null)
        {
            throw new OrganizationNotFoundException(organizationId);
        }
        
        await ValidateUserExists(createOrganizationUserDto.UserId, cancellationToken);
        
        await organization.AddUsers(
            new[] { createOrganizationUserDto }, _unitOfWork.OrganizationUsersRepository, cancellationToken);
    }

    private async Task ValidateUserExists(Guid userId, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.UsersRepository.CheckUserExists(userId, cancellationToken))
        {
            throw new UserNotFoundException(userId);
        }
    }
}