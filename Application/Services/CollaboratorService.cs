using Domain.IRepository;
using Domain.Interfaces;
using Domain.Factory;
using Domain.Models;
using Application.DTO;
using AutoMapper;
using Infrastructure;
using Application.DTO.Collaborators;
namespace Application.Services;

public class CollaboratorService
{
    private ICollaboratorRepository _collaboratorRepository;
    private ICollaboratorFactory _collaboratorFactory;
    private IAssociationTrainingModuleCollaboratorsRepository _assocTMCRepository;
    private ITrainingModuleRepository _trainingModuleRepository;
    private AbsanteeContext _context;
    private readonly IMapper _mapper;

    public CollaboratorService(ICollaboratorRepository collaboratorRepository, ICollaboratorFactory collaboratorFactory, IAssociationTrainingModuleCollaboratorsRepository assocTMCRepository, ITrainingModuleRepository trainingModuleRepository, AbsanteeContext context, IMapper mapper)
    {
        _collaboratorRepository = collaboratorRepository;
        _collaboratorFactory = collaboratorFactory;
        _assocTMCRepository = assocTMCRepository;
        _trainingModuleRepository = trainingModuleRepository;
        _context = context;
        _mapper = mapper;
    }


    // UC9 - Como gestor de RH, quero listar todos os colaboradores
    public async Task<Result<IEnumerable<Guid>>> GetAll()
    {
        try
        {
            var collabs = await _collaboratorRepository.GetAllAsync();
            var collabIds = collabs.Select(U => U.Id);

            return Result<IEnumerable<Guid>>.Success(collabIds);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Guid>>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<ICollection<Guid>>> GetActiveCollaboratorsWithNoTrainingModuleFinishedInSubject(Guid subjectId)
    {
        IEnumerable<Collaborator> activeCollaborators;
        IEnumerable<AssociationTrainingModuleCollaborator> finishedCollaborators;
        try
        {
            // Step 1: Get all active collaborators
            activeCollaborators = await _collaboratorRepository.GetActiveCollaborators();

            // Step 2: Get training modules that are finished for the subject
            var finishedTrainingModules = await _trainingModuleRepository
                .GetBySubjectIdAndFinished(subjectId, DateTime.UtcNow);

            var finishedTrainingModuleIds = finishedTrainingModules.Select(m => m.Id).ToList();

            // Step 3: Get collaborator-module links for those finished modules
            finishedCollaborators = await _assocTMCRepository
                .GetByTrainingModuleIds(finishedTrainingModuleIds);

        }
        catch (Exception e)
        {
            return Result<ICollection<Guid>>.Failure(Error.InternalServerError(e.Message));
        }

        var collaboratorsWithFinishedModules = finishedCollaborators
            .Select(c => c.CollaboratorId)
            .Distinct()
            .ToHashSet();

        // Step 4: Filter active collaborators that are NOT in the above list
        var result = activeCollaborators
            .Where(c => !collaboratorsWithFinishedModules.Contains(c.Id))
            .Select(c => c.Id)
            .ToList();

        return Result<ICollection<Guid>>.Success(result);
    }

    public async Task<Result<ICollection<Guid>>> GetCompletedTrainingsAsync(Guid subjectId, DateTime fromDate)
    {
        // Garantir que a data de entrada também seja UTC
        var fromDateUtc = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);

        IEnumerable<Collaborator> activeCollaborators;
        IEnumerable<AssociationTrainingModuleCollaborator> trainingModuleCollaborators;
        try
        {
            // Passo 1 - Confirmar a existencia do subjectId
            activeCollaborators = await _collaboratorRepository.GetActiveCollaborators();

            // Passo 2 - Procurar os Training Modules que foram terminados antes da fromDate de um dado SubjectId
            var finishedTrainingModules = await _trainingModuleRepository
                .GetBySubjectAndAfterDateFinished(subjectId, fromDateUtc);

            var finishedTrainingModuleIds = finishedTrainingModules.Select(m => m.Id).ToList();

            // Passo 3: Obter as ligações de colaboradores que completaram esses módulos
            trainingModuleCollaborators = await _assocTMCRepository
                .GetByTrainingModuleIds(finishedTrainingModuleIds);
        }
        catch (Exception e)
        {
            return Result<ICollection<Guid>>.Failure(Error.InternalServerError(e.Message));
        }

        var activeCollaboratorIds = activeCollaborators.Select(c => c.Id).ToList();

        var collaboratorsWithFinishedModules = trainingModuleCollaborators
            .Select(c => c.CollaboratorId)
            .Distinct()
            .ToHashSet();

        // Passo 4: Filtrar os colaboradores ativos que participaram e terminaram esses módulos
        var result = activeCollaborators
            .Where(c => collaboratorsWithFinishedModules.Contains(c.Id))
            .Select(c => c.Id)  // Alteração aqui, estamos agora retornando apenas o ID
            .ToList();

        return Result<ICollection<Guid>>.Success(result);
    }
    public async Task<IEnumerable<ICollaborator>> GetAllByFinishedTrainingModuleInSubjectAfterPeriod(Guid subjectId, DateTime date)
    {
        // Step 1: Get training modules that are finished for the subject
        var finishedTrainingModules = await _trainingModuleRepository
            .GetBySubjectIdAndFinished(subjectId, date);

        var trainingModulesIds = finishedTrainingModules.Select(t => t.Id).ToList();

        // Step 2: Get collaborator-module links for those finished modules
        var trainingModuleCollaborators = await _assocTMCRepository.GetByTrainingModuleIds(trainingModulesIds);

        var collabsIds = trainingModuleCollaborators.Select(t => t.CollaboratorId).ToList();

        // Step 3: Get collaborators
        var collabs = await _collaboratorRepository.GetByIdsAsync(collabsIds);

        return collabs;
    }
}