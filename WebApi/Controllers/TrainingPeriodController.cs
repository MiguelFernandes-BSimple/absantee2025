﻿using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/trainingperiod")]
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
    public async Task<ActionResult<TrainingPeriodDTO>> PostTrainingPeriod(CreateTrainingPeriodDTO trainingPeriodDTO)
    {
        TrainingPeriodDTO trainingPeriodResultDTO = await _trainingPeriodService.Add(trainingPeriodDTO);

        if (trainingPeriodResultDTO == null)
            return BadRequest();

        return Created("", trainingPeriodResultDTO);

    }
}
