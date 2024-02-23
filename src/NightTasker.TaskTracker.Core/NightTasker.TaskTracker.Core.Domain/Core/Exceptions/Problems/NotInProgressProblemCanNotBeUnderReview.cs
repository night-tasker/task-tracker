namespace NightTasker.TaskTracker.Core.Domain.Core.Exceptions.Problems;

public class NotInProgressProblemCanNotBeUnderReview(Guid problemId) : DomainException(
    $"Problem with id {problemId} is not in progress and can not be under review");