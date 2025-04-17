
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Models;

namespace WebApi.Controllers;


[ApiController]
[Route("api/colaborator")]
public class ColaboratorController : Controller
{
    private readonly CollaboratorService _colaboratorService;

    private readonly HolidayPlanService _holidayPlanService;

    List <string> _errorMessages = new List<string>();


    public ColaboratorController(CollaboratorService collaboratorService,HolidayPlanService holidayPlanService )
    {
        _colaboratorService = collaboratorService;
        _holidayPlanService = holidayPlanService;
        
    }

    [HttpGet("holidayperiods/longer-than/{days}")]
    public ActionResult<IEnumerable<object?>> GetWithHolidayLongerThan(int days)
    {
        var result = _colaboratorService.FindAllWithHolidayPeriodsLongerThan(days);
        return Ok(result);
    }
    
/*
    [HttpPost("holidayperiods/between-dates")]
    public ActionResult<IEnumerable<object?>> GetWithHolidayBetweenDates(DateOnly dataInicio , DateOnly DataFim )
    {
        var result = _colaboratorService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(periodDate);
        return Ok(result);
    }

    [HttpPost("project")]
    public ActionResult<IEnumerable<object?>> GetByProject()
    {
        var result = _colaboratorService.FindAllByProject(project);
        return Ok(result);
    }

    [HttpPost("project/period")]
    public ActionResult<IEnumerable<object?>> GetByProjectAndPeriod([FromBody] ProjectAndPeriod model)
    {
        var result = _colaboratorService.FindAllByProjectAndBetweenPeriod(model.Project, model.Period);
        return Ok(result);
    }
    
*/
}