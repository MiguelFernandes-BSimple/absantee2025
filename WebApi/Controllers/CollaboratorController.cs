using Application.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CollaboratorController : ControllerBase
{
    private readonly CollaboratorService _collaboratorService;

    List<string> _errorMessages = new List<string>();

    public CollaboratorController(CollaboratorService collaboratorService)
    {
        _collaboratorService = collaboratorService;
    }

    // GET: api/Collaborator
    [HttpGet("collaborators")]
    public async Task<ActionResult<IEnumerable<ICollaborator>>> GetCollaborators()
    {
        IEnumerable<ICollaborator> collabs = await _collaboratorService.FindAll();

        return Ok(collabs);
    }
}