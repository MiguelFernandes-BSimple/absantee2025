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
    private readonly CollaboratorService _collaboratorService;

    public TrainingModuleController(TrainingModuleService trainingModuleService, AssociationTrainingModuleCollaboratorService assocTMCService, CollaboratorService collaboratorService)
    {
        _trainingModuleService = trainingModuleService;
        _assocTMCService = assocTMCService;
        _collaboratorService = collaboratorService;
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

    // UC32: Como Gestor de Formação, quero listar todos os colaboradores ativos que não têm qualquer formação terminada em determinado tema
    // GET api/TrainingSubject/{id}/collaborators/active/no-training-done
    [HttpGet("not-completed/subjects/{id}/collaborators/active/")]
    public async Task<ActionResult> GetActiveCollaboratorsWithNoTrainingDoneForSubject(Guid id)
    {
        try
        {
            IEnumerable<Guid> collabs =
                await _collaboratorService.GetActiveCollaboratorsWithNoTrainingModuleFinishedInSubject(id);

            return Ok(collabs);
        }
        catch
        {
            return BadRequest(id);
        }
    }

    [HttpGet("completed")]
    public async Task<IActionResult> GetAllCollaborators([FromQuery] Guid subjectId, [FromQuery] DateTime fromDate)
    {
        var collaborators = await _collaboratorService.GetCompletedTrainingsAsync(subjectId, fromDate);

        return Ok(collaborators);
    }
}
