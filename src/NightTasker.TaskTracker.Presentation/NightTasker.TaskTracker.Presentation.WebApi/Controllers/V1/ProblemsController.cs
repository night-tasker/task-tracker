using MediatR;
using Microsoft.AspNetCore.Mvc;
using NightTasker.Common.Core.Identity.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Problems.Commands.CreateProblem;
using NightTasker.TaskTracker.Presentation.WebApi.Requests.Problems;

namespace NightTasker.TaskTracker.Presentation.WebApi.Controllers.V1;

[ApiController]
[Route("api/v1/problems")]
public class ProblemsController(
    ISender sender,
    IIdentityService identityService) : ControllerBase
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    private readonly IIdentityService _identityService = 
        identityService ?? throw new ArgumentNullException(nameof(identityService));
    
    [HttpPost]
    public async Task<IActionResult> CreateProblem(
        [FromBody] CreateProblemRequest problemDto, CancellationToken cancellationToken)
    {
        var userId = _identityService.CurrentUserId;
        var command = new CreateProblemCommand(problemDto.Text, problemDto.OrganizationId, userId!.Value);
        var problemId = await _sender.Send(command, cancellationToken);
        return Ok(problemId);
    }
}