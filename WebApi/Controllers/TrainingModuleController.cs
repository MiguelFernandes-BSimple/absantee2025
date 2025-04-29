using Application.DTO.AssociationTrainingModuleCollaborator;
using Application.DTO.TrainingModule;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrainingModuleController : ControllerBase
{
    private readonly TrainingModuleService _trainingModuleService;
    private readonly AssociationTrainingModuleCollaboratorService _assocTMCService;

    public TrainingModuleController(TrainingModuleService trainingModuleService, AssociationTrainingModuleCollaboratorService assocTMCService)
    {
        _trainingModuleService = trainingModuleService;
        _assocTMCService = assocTMCService;
    }

    // US 30: Como Gestor de Formação, quero definir um Módulo de Formação (Training Module): 
    //        - assunto, 
    //        - lista de vários períodos (com horas)
    // POST api/TrainingModule
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

    // US 31: Como Gestor de Formação, quero inscrever um colaborador num módulo de formação
    // POST api/TrainingModules/{id}/collaborators
    [HttpPost("{trainingModuleId}/collaborators")]
    public async Task<ActionResult> AddCollaboratorToModule(Guid trainingModuleId, [FromBody] CreateAssociationTrainingModuleCollaboratorDTO assocDTO)
    {
        try
        {
            AssociationTrainingModuleCollaboratorDTO createdAssoc = await _assocTMCService.Add(trainingModuleId, assocDTO);

            return Created("", createdAssoc);
        }
        catch
        {
            return BadRequest(trainingModuleId);
        }
    }
}
