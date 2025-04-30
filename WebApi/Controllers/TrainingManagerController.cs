

using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class TrainingManagerController : ControllerBase{

    private readonly CollaboratorService _collabService;

    public TrainingManagerController(CollaboratorService collaboratorService)
    {
        _collabService = collaboratorService;
    }

    [HttpGet("completed")]
    public async Task<IActionResult> GetAllCollaborators([FromQuery] Guid subjectId, [FromQuery] DateTime fromDate)
    {
        var collaborators = await _collabService.GetCompletedTrainingsAsync(subjectId, fromDate);

        return Ok(collaborators);
    }
}