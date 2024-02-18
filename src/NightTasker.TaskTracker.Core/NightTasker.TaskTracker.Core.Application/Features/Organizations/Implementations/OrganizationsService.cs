using NightTasker.TaskTracker.Core.Application.Features.Organizations.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Organizations.Models;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Core.Application.Features.Organizations.Implementations;

internal sealed class OrganizationsService(IUnitOfWork unitOfWork) : IOrganizationsService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task CreateOrganization(
        CreateOrganizationDto createOrganizationDto, CancellationToken cancellationToken)
    {
        var organization = Organization.CreateInstance(createOrganizationDto.Id);
        if (createOrganizationDto.Users is not null && createOrganizationDto.Users.Count > 0)
        {
            await organization.AddUsers(
                createOrganizationDto.Users, unitOfWork.OrganizationUsersRepository, cancellationToken);
        }
        
        await _unitOfWork.OrganizationsRepository.Add(organization, cancellationToken);
    }
}