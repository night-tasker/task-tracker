using MediatR;

namespace NightTasker.TaskTracker.Core.Application.Features.Problems.Commands.CreateProblem;

public sealed record CreateProblemCommand(
    string? Text, Guid OrganizationId, Guid AuthorId) : IRequest<Guid>;