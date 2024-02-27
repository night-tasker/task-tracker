using MediatR;
using NightTasker.TaskTracker.Core.Application.Features.Problems.Models;

namespace NightTasker.TaskTracker.Core.Application.Features.Problems.Commands.GetProblemsAssignedToUser;

public sealed record GetProblemsAssignedToUserQuery(
    Guid UserId) : IRequest<IReadOnlyCollection<ProblemDto>>;