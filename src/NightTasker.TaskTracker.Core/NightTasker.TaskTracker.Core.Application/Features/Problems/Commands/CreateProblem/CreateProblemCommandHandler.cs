using MediatR;
using NightTasker.TaskTracker.Core.Application.Features.Problems.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Problems.Models;
using NightTasker.TaskTracker.Core.Domain.Repositories;

namespace NightTasker.TaskTracker.Core.Application.Features.Problems.Commands.CreateProblem;

internal sealed class CreateProblemCommandHandler(
    IUnitOfWork unitOfWork,
    IProblemsService problemsService)
    : IRequestHandler<CreateProblemCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IProblemsService _problemsService = problemsService ?? throw new ArgumentNullException(nameof(problemsService));

    public async Task<Guid> Handle(CreateProblemCommand request, CancellationToken cancellationToken)
    {
        var problemDto = new CreateProblemDto(request.Text, request.OrganizationId, request.AuthorId);
        var problemId = await _problemsService.CreateProblem(problemDto, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        return problemId;
    }
}