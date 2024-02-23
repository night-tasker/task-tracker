using NightTasker.Common.Core.Abstractions;
using NightTasker.TaskTracker.Core.Domain.Core.Exceptions.Problems;
using NightTasker.TaskTracker.Core.Domain.Enums;

namespace NightTasker.TaskTracker.Core.Domain.Entities;

public class Problem : IEntityWithId<Guid>, IDateTimeOffsetModification
{
    private Problem(
        Guid id,
        Guid organizationId,
        Guid authorId,
        string text)
    {
        Id = id;
        OrganizationId = organizationId;
        AuthorId = authorId;
        Text = text;
    }
    
    public static Problem CreateInstance(
        Guid organizationId,
        Guid authorId,
        string text)
    {
        return new Problem(Guid.NewGuid(), organizationId, authorId, text);
    }
    
    public Guid Id { get; private set; }
    
    public string? Text { get; private set; }

    public Guid OrganizationId { get; private set; }

    public Organization Organization { get; private set; } = null!;

    public Guid AuthorId { get; private set; }
    
    public User Author { get; private set; } = null!;
    
    public Guid? AssigneeId { get; private set; }

    public User? Assignee { get; private set; }
    
    public ProblemStatus Status { get; private set; }
    
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    
    public DateTimeOffset? UpdatedDateTimeOffset { get; set; }

    public void OpenProblem()
    {
        Status = ProblemStatus.Open;
    }

    public void WorkOnProblem()
    {
        if (!AssigneeId.HasValue)
        {
            throw new NotAssignedProblemCanNotBeInProgress(Id);
        }
        Status = ProblemStatus.InProgress;
    }

    public void ReviewProblem()
    {
        if (Status != ProblemStatus.InProgress)
        {
            throw new NotInProgressProblemCanNotBeUnderReview(Id);
        }
        Status = ProblemStatus.UnderReview;
    }

    public void ResolveProblem()
    {
        Status = ProblemStatus.Resolved;
    }

    public void CloseProblem()
    {
        Status = ProblemStatus.Closed;
    }

    public void AssignProblem(Guid assigneeId)
    {
        AssigneeId = assigneeId;
    }
}