using Application;
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
    private readonly HolidayPlanService _holidayPlanService;

    public CollaboratorController(CollaboratorService collabService, HolidayPlanService holidayPlanService)
    {
        _collabService = collabService;
        _holidayPlanService = holidayPlanService;
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
    public async Task<IActionResult> Create([FromBody] CreateCollaboratorDto collabDto)
    {
        // verificações feitas no dto
        var collabCreated = await _collabService.Create(collabDto);

        if (collabCreated == null) return BadRequest();

        return Created("", collabCreated);
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
    //UC13
    [HttpGet("{collaboratorId}/holidayPlan/holidayPeriods/ByPeriod")]
    public async Task<ActionResult<IEnumerable<HolidayPeriodDTO>>> GetHolidayPeriodsOfCollaboratorByPeriod(Guid collaboratorId, [FromQuery] PeriodDate periodDate)
    {
        var result = await _collabService.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(collaboratorId, periodDate);

        return Ok(result);
    }

    //US14 
    [HttpGet("holidayPlan/holidayPeriods/ByPeriod")]
    public async Task<ActionResult<IEnumerable<CollaboratorDTO>>> GetCollaboratorsByPeriod(
        [FromQuery] DateOnly initDate,
        [FromQuery] DateOnly finalDate)
    {
        var periodDate = new PeriodDate(initDate, finalDate);
        var result = await _collabService.FindAllWithHolidayPeriodsBetweenDates(periodDate);
        return Ok(result);
    }

    //US15
    [HttpGet("longer-than")]
    public async Task<ActionResult<IEnumerable<CollaboratorDTO>>> GetWithHolidayPeriodsLongerThan(int days)
    {
        var result = await _collabService.FindAllWithHolidayPeriodsLongerThan(days);

        return Ok(result);
    }


    // UC17 Get: api/collaborators/foo/holidayperiods/includes-date?date=bar
    [HttpGet("{id}/holidayperiods/includes-date")]
    public async Task<ActionResult<HolidayPeriod?>> GetHolidayPeriodContainingDay(Guid id, string date)
    {
        var dateOnly = DateOnly.Parse(date);
        var result = await _holidayPlanService.FindHolidayPeriodForCollaboratorThatContainsDay(id, dateOnly);

        if (result != null)
            return Ok(result);

        return NotFound();
    }

    // UC18 Get: api/collaborators/foo/holidayperiods/longer-than?days=bar
    [HttpGet("{id}/holidayperiods/longer-than")]
    public async Task<ActionResult<IEnumerable<HolidayPeriod>>> GetHolidayPeriodLongerThan(Guid id, string days)
    {
        var amount = int.Parse(days);
        var result = await _holidayPlanService.FindAllHolidayPeriodsForCollaboratorLongerThan(id, amount);

        return Ok(result);
    }

    // UC19 Get: api/collaborators/holidayperiods/include-weekends?
    [HttpGet("{id}/holidayperiods/includes-weekends")]
    public async Task<ActionResult<IEnumerable<HolidayPeriod>>> GetHolidayPeriodsBetweenThatIncludeWeeknds(Guid id, PeriodDate periodDate)
    {
        var result = await _holidayPlanService.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekendsAsync(id, periodDate);
        return Ok(result);
    }

    // uc20 
    [HttpGet("holidayperiods/overlaps")]
    public async Task<ActionResult<IEnumerable<HolidayPeriodDTO>>> GetOverlapingPeriodsBetween(
        [FromQuery] Guid collabId1,
        [FromQuery] Guid collabId2,
        [FromQuery] PeriodDate periodDate)
    {
        var periods = await _holidayPlanService.FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDatesAsync(collabId1, collabId2, periodDate);

        if (periods == null) return BadRequest();

        return Ok(periods);
    }

    // Collaborator Projects
    [HttpGet("{id}/associations")]
    public async Task<ActionResult<IEnumerable<AssociationProjectCollaboratorDTO>>> GetCollaboratorProjects(Guid id)
    {
        var result = await _collabService.GetCollaboratorProjects(id);

        if (result == null) return BadRequest();

        return Ok(result);
    }
}