using Application.DTO.TrainingModule;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrainingModuleController : ControllerBase
{
    private readonly TrainingModuleService _trainingModuleService;

    public TrainingModuleController(TrainingModuleService trainingModuleService)
    {
        _trainingModuleService = trainingModuleService;
    }

    // US 30: Como Gestor de Formação, quero definir um Módulo de Formação (Training Module): 
    //        - assunto, 
    //        - lista de vários períodos (com horas)
    // Post: api/TrainingModule
    [HttpPost]
    public async Task<ActionResult> AddTrainingModule(AddTrainingModuleDTO tmDTO)
    {
        try
        {
            TrainingModuleDTO addedTS = await _trainingModuleService.Add(tmDTO);

            return Created("", addedTS);
        }
        catch
        {
            return BadRequest(tmDTO.Periods);
        }
    }
}
