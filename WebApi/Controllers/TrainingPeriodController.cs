using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrainingPeriodController : ControllerBase
{
    private readonly TrainingPeriodService _trainingPeriodService;

    public TrainingPeriodController(TrainingPeriodService trainingPeriodService)
    {
        _trainingPeriodService = trainingPeriodService;
    }

    // UC2 
    [HttpPost]
    public async Task<ActionResult<TrainingPeriodDTO>> PostTrainingPeriod(TrainingPeriodDTO trainingPeriodDTO)
    {
        TrainingPeriodDTO trainingPeriodResultDTO = await _trainingPeriodService.Add(trainingPeriodDTO);

        if (trainingPeriodResultDTO == null)
            return BadRequest();

        return Ok(trainingPeriodResultDTO);

    }
}
