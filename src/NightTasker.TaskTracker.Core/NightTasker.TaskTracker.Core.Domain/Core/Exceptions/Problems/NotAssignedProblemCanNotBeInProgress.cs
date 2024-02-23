using NightTasker.Common.Core.Exceptions.Base;

namespace NightTasker.TaskTracker.Core.Domain.Core.Exceptions.Problems;

public sealed class NotAssignedProblemCanNotBeInProgress(Guid id)
    : DomainException($"Problem with id {id} is not assigned and can not be in progress");