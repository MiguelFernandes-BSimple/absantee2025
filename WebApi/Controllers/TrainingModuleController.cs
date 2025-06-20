using Application.DTO.TrainingModule;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/trainingmodules")]
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
    // POST api/trainingmodules
    [HttpPost]
    public async Task<ActionResult<TrainingModuleDTO>> AddTrainingModule(AddTrainingModuleDTO tmDTO)
    {
        var addedTS = await _trainingModuleService.Add(tmDTO);

        return addedTS.ToActionResult();
    }
}
