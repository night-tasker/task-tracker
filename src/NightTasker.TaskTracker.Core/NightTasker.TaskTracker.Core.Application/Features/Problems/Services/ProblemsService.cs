using NightTasker.TaskTracker.Core.Application.Features.Problems.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Problems.Models;
using NightTasker.TaskTracker.Core.Domain.Core.Exceptions.Organizations;
using NightTasker.TaskTracker.Core.Domain.Core.Exceptions.Users;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Core.Application.Features.Problems.Services;

internal sealed class ProblemsService(IUnitOfWork unitOfWork) : IProblemsService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Guid> CreateProblem(CreateProblemDto problemDto, CancellationToken cancellationToken)
    {
        await ValidateOrganizationExists(problemDto.OrganizationId, cancellationToken);
        await ValidateUserExists(problemDto.AuthorId, cancellationToken);
        var problem = Problem.CreateInstance(
            problemDto.OrganizationId,
            problemDto.AuthorId,
            problemDto.Text);
        
        await _unitOfWork.ProblemsRepository.Add(problem, cancellationToken);
        return problem.Id;
    }

    private async Task ValidateOrganizationExists(
        Guid organizationId, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.OrganizationsRepository.CheckOrganizationExists(organizationId, cancellationToken))
        {
            throw new OrganizationNotFoundException(organizationId);
        }
    }

    private async Task ValidateUserExists(
        Guid userId, CancellationToken cancellationToken)
    {
        if(!await _unitOfWork.UsersRepository.CheckUserExists(userId, cancellationToken))
        {
            throw new UserNotFoundException(userId);
        }
    }
}