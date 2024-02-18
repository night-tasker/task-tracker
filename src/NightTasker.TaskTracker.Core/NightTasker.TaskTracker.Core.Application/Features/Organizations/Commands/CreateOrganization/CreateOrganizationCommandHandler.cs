using MediatR;
using NightTasker.TaskTracker.Core.Application.Features.Organizations.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Organizations.Models;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Core.Application.Features.Organizations.Commands.CreateOrganization;

internal sealed class CreateOrganizationCommandHandler(
    IOrganizationsService organizationsService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateOrganizationCommand>
{
    private readonly IOrganizationsService _organizationsService = 
        organizationsService ?? throw new ArgumentNullException(nameof(organizationsService));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    
    
    public async Task Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var createOrganizationDto = new CreateOrganizationDto(request.Id, request.Users);
        await _organizationsService.CreateOrganization(createOrganizationDto, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
    }
}