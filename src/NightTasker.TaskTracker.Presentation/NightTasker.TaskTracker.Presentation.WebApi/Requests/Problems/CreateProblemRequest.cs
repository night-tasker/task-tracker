using FluentValidation;

namespace NightTasker.TaskTracker.Presentation.WebApi.Requests.Problems;

public sealed record CreateProblemRequest(string? Text, Guid OrganizationId);

internal sealed class CreateProblemRequestValidator : AbstractValidator<CreateProblemRequest>
{
    public CreateProblemRequestValidator()
    {
        RuleFor(x => x.OrganizationId)
            .NotEqual(Guid.Empty);
    }
}
 