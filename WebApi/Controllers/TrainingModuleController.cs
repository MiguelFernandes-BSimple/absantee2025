using Application.DTO.AssociationTrainingModuleCollaborator;
using Application.DTO.TrainingModule;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/trainingmodules")]
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
    // POST api/trainingmodules
    [HttpPost]
    public async Task<ActionResult<TrainingModuleDTO>> AddTrainingModule(AddTrainingModuleDTO tmDTO)
    {
        var addedTS = await _trainingModuleService.Add(tmDTO);

        return addedTS.ToActionResult();
    }

    // US 31: Como Gestor de Formação, quero inscrever um colaborador num módulo de formação
    // POST api/trainingmodules/{id}/collaborators
    [HttpPost("{trainingModuleId}/collaborators")]
    public async Task<ActionResult<AssociationTrainingModuleCollaboratorDTO>> AddCollaboratorToModule(Guid trainingModuleId, [FromBody] CreateAssociationTrainingModuleCollaboratorDTO assocDTO)
    {
        var createdAssoc = await _assocTMCService.Add(trainingModuleId, assocDTO);

        return createdAssoc.ToActionResult();
    }

    // UC32: Como Gestor de Formação, quero listar todos os colaboradores ativos que não têm qualquer formação terminada em determinado tema
    // GET api/trainingmodules/{id}/collaborators/active/no-training-done
    [HttpGet("not-completed/subjects/{id}/collaborators/active/")]
    public async Task<ActionResult<ICollection<Guid>>> GetActiveCollaboratorsWithNoTrainingDoneForSubject(Guid id)
    {
        var collabs = await _collaboratorService.GetActiveCollaboratorsWithNoTrainingModuleFinishedInSubject(id);

        return collabs.ToActionResult();
    }

    // UC33: Como Gestor de Formação, quero listar todos os colaboradores que têm 
    //       formação terminada em determinado tema depois de uma determinada data
    // Get 
    [HttpGet("completed/subjects/{id}/collaborators")]
    public async Task<ActionResult<ICollection<Guid>>> GetAllCollaboratorsWithTrainingDoneInSubjectAfterDate(Guid id, [FromQuery] DateTime fromDate)
    {
        var collabs = await _collaboratorService.GetCompletedTrainingsAsync(id, fromDate);

        return collabs.ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainingModuleDTO>>> GetAllInfo()
    {
        var trainingModules = await _trainingModuleService.GetAllInfo();

        return trainingModules.ToActionResult();
    }
}
