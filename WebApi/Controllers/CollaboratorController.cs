using Application.DTO;
using Application.DTO.Collaborators;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/collaborators")]
[ApiController]
public class CollaboratorController : ControllerBase
{
    private readonly CollaboratorService _collabService;

    public CollaboratorController(CollaboratorService collabService)
    {
        _collabService = collabService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Guid>>> Get()
    {
        var collaborators = await _collabService.GetAll();

        return collaborators.ToActionResult();
    }

}