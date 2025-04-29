using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HolidayPlanController : Controller
{
    private readonly HolidayPlanService _holidayPlanService;

    public HolidayPlanController(HolidayPlanService holidayPlanService)
    {
        _holidayPlanService = holidayPlanService;
    }

    // UC4: Como gestor de projetos, quero criar projeto
    [HttpPost]
    public async Task<ActionResult<HolidayPlanDTO>> AddHolidayPlan(CreateHolidayPlanDTO createHolidayPlanDTO)
    {
        var result = await _holidayPlanService.AddHolidayPlan(createHolidayPlanDTO);

        if (result == null)
            return BadRequest();

        return Ok(result);
    }

    // UC4: Como gestor de projetos, quero criar projeto
    [HttpPost("holidayperiod")] 
    public async Task<ActionResult<HolidayPeriodDTO>> AddHolidayPeriod(CreateHolidayPeriodDTO createHolidayPeriodDTO)
    {
        var result = await _holidayPlanService.AddHolidayPeriod(createHolidayPeriodDTO);

        if (result == null)
            return BadRequest();

        return Ok(result);
    }

}
