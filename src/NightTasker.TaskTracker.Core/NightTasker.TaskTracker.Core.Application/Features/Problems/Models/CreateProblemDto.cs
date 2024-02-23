namespace NightTasker.TaskTracker.Core.Application.Features.Problems.Models;

public sealed record CreateProblemDto(string? Text, Guid OrganizationId, Guid AuthorId);