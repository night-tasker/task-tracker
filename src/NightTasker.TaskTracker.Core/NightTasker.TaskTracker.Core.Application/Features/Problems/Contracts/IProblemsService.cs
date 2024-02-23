using NightTasker.TaskTracker.Core.Application.Features.Problems.Models;

namespace NightTasker.TaskTracker.Core.Application.Features.Problems.Contracts;

public interface IProblemsService
{
    Task<Guid> CreateProblem(CreateProblemDto problemDto, CancellationToken cancellationToken);
}