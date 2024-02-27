using MediatR;
using NightTasker.TaskTracker.Core.Application.Features.Problems.Models;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Core.Application.Features.Problems.Commands.GetProblemsAssignedToUser;

internal sealed class GetProblemsAssignedToUserQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetProblemsAssignedToUserQuery, IReadOnlyCollection<ProblemDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<IReadOnlyCollection<ProblemDto>> Handle(GetProblemsAssignedToUserQuery request, CancellationToken cancellationToken)
    {
        var problems = await _unitOfWork.ProblemsReadRepository.GetProblemsAssignedOnUser(request.UserId, cancellationToken);
        var problemsDto = problems.Select(ProblemToDto).ToArray();
        return problemsDto;
    }
    
    private static ProblemDto ProblemToDto(Problem problem)
    {
        return new ProblemDto(
            problem.Id,
            problem.Text,
            problem.OrganizationId,
            problem.AuthorId,
            problem.AssigneeId,
            problem.Status
        );
    }
}