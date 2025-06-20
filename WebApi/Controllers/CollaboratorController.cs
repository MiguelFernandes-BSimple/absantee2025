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


    [HttpPut]
    public async Task<ActionResult<CollabUpdatedDTO>> updateCollaborator([FromBody] CollabDetailsDTO newCollabData)
    {
        var collabData = new CollabData(newCollabData.CollabId, newCollabData.UserId, newCollabData.Names, newCollabData.Surnames, newCollabData.Email, newCollabData.UserPeriod, newCollabData.CollaboratorPeriod);

        var result = await _collabService.EditCollaborator(collabData);
        if (result == null) return BadRequest("Invalid Arguments");
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Guid>>> Get()
    {
        var collaborators = await _collabService.GetAll();

        return collaborators.ToActionResult();
    }

    [HttpGet("details")]
    public async Task<ActionResult<IEnumerable<CollabDetailsDTO>>> GetAllInfo()
    {
        var collaborators = await _collabService.GetAllInfo();

        return collaborators.ToActionResult();
    }

    [HttpGet("{collaboratorId}")]
    public async Task<ActionResult<CollaboratorDTO>> GetById(Guid collaboratorId)
    {
        var collaborator = await _collabService.GetById(collaboratorId);

        return collaborator.ToActionResult();
    }

    [HttpGet("{collaboratorId}/details")]
    public async Task<ActionResult<CollabDetailsDTO>> GetDetailsById(Guid collaboratorId)
    {
        var collaborator = await _collabService.GetDetailsById(collaboratorId);

        return collaborator.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Guid>>> FindBy([FromQuery] string? name, [FromQuery] string? surname)
    {
        if (name == null && surname == null)
            return BadRequest("Please insert at least a name or surname");

        IEnumerable<Guid> collabIds;

        if (surname == null)
        {
            collabIds = await _collabService.GetByNames(name);
        }
        else if (name == null)
        {
            collabIds = await _collabService.GetBySurnames(surname);
        }
        else
        {
            collabIds = await _collabService.GetByNamesAndSurnames(name, surname);
        }

        if (collabIds.Any())
            return Ok(collabIds);

        else return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CollaboratorCreatedDto>> Create([FromBody] CreateCollaboratorDto collabDto)
    {
        var createCollabDto = new CollabCreateDataDTO(collabDto.Names, collabDto.Surnames, collabDto.Email, collabDto.deactivationDate, collabDto.PeriodDateTime);

        var collabCreated = await _collabService.Create(createCollabDto);

        return collabCreated.ToActionResult();
    }

    // endpoint utilizado para testes
    [HttpGet("count")]
    public async Task<IActionResult> GetCount()
    {
        var count = await _collabService.GetCount();

        if (count > 0)
            return Ok(count);

        return NotFound("No collaborators found");
    }
    //US14 
    [HttpGet("holidayPlan/holidayPeriods/ByPeriod")]
    public async Task<ActionResult<IEnumerable<CollaboratorDTO>>> GetCollaboratorsByPeriod(
        [FromQuery] DateOnly initDate,
        [FromQuery] DateOnly finalDate)
    {
        var periodDate = new PeriodDate(initDate, finalDate);
        var result = await _collabService.FindAllWithHolidayPeriodsBetweenDates(periodDate);
        return result.ToActionResult();
    }

    //US15
    [HttpGet("longer-than")]
    public async Task<ActionResult<IEnumerable<CollaboratorDTO>>> GetWithHolidayPeriodsLongerThan(int days)
    {
        var result = await _collabService.FindAllWithHolidayPeriodsLongerThan(days);

        return result.ToActionResult();
    }
}