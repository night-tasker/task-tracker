using NightTasker.TaskTracker.Core.Domain.Enums;

namespace NightTasker.TaskTracker.Core.Application.Features.Problems.Models;

public sealed record ProblemDto(
    Guid Id, string? Text, Guid OrganizationId, Guid AuthorId, Guid? AssigneeId, ProblemStatus Status);