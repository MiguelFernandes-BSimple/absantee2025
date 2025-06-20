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

    public async Task<Result<IEnumerable<CollabDetailsDTO>>> GetAllInfo()
    {
        try
        {

            var collabs = await _collaboratorRepository.GetAllAsync();
            var userIds = collabs.Select(c => c.UserId).ToList();
            var users = await _userRepository.GetByIdsAsync(userIds);

            var resultList = new List<CollabDetailsDTO>();

            foreach (var collab in collabs)
            {
                var user = users.FirstOrDefault(u => u.Id == collab.UserId);

                if (user != null)
                {
                    resultList.Add(new CollabDetailsDTO
                    {
                        CollabId = collab.Id,
                        UserId = user.Id,
                        Names = user.Names,
                        Surnames = user.Surnames,
                        Email = user.Email,
                        UserPeriod = user.PeriodDateTime,
                        CollaboratorPeriod = collab.PeriodDateTime
                    });
                }
            }

            return Result<IEnumerable<CollabDetailsDTO>>.Success(resultList);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<CollabDetailsDTO>>.Failure(Error.InternalServerError(e.Message));
        }
    }
    public async Task<Result<CollaboratorDTO>> GetById(Guid id)
    {
        try
        {
            var collab = await _collaboratorRepository.GetByIdAsync(id);
            if (collab == null)
                return Result<CollaboratorDTO>.Failure(Error.NotFound("User not found"));
            var result = _mapper.Map<CollaboratorDTO>(collab);

            return Result<CollaboratorDTO>.Success(result);

        }
        catch (Exception e)
        {
            return Result<CollaboratorDTO>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<CollabDetailsDTO>> GetDetailsById(Guid id)
    {
        try
        {

            var collab = await _collaboratorRepository.GetByIdAsync(id);
            if (collab == null)
                return Result<CollabDetailsDTO>.Failure(Error.NotFound("Collab not found"));

            var user = await _userRepository.GetByIdAsync(collab.UserId);
            if (user == null)
                return Result<CollabDetailsDTO>.Failure(Error.NotFound("User not found"));

            var result = new CollabDetailsDTO()
            {
                CollabId = collab.Id,
                UserId = user.Id,
                Names = user.Names,
                Surnames = user.Surnames,
                Email = user.Email,
                UserPeriod = user.PeriodDateTime,
                CollaboratorPeriod = collab.PeriodDateTime
            };

            return Result<CollabDetailsDTO>.Success(result);
        }
        catch (Exception e)
        {
            return Result<CollabDetailsDTO>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<long> GetCount()
    {
        return await _collaboratorRepository.GetCount();
    }

    // uc10 - Como gestor de RG, quero listar todos os colaboradores que têm determinado nome ou sobrenome
    public async Task<IEnumerable<Guid>> GetByNames(string names)
    {
        var users = await _userRepository.GetByNamesAsync(names);
        var userIds = users.Select(u => u.Id);
        var collabs = await _collaboratorRepository.GetByUsersIdsAsync(userIds);
        var collabIds = collabs.Select(c => c.Id);
        return collabIds;
    }

    public async Task<IEnumerable<Guid>> GetBySurnames(string surnames)
    {
        var users = await _userRepository.GetBySurnamesAsync(surnames);
        var userIds = users.Select(u => u.Id);
        var collabs = await _collaboratorRepository.GetByUsersIdsAsync(userIds);
        return collabs.Select(c => c.Id);
    }

    public async Task<IEnumerable<Guid>> GetByNamesAndSurnames(string names, string surnames)
    {
        var users = await _userRepository.GetByNamesAndSurnamesAsync(names, surnames);
        var userIds = users.Select(u => u.Id);
        var collabs = await _collaboratorRepository.GetByUsersIdsAsync(userIds);
        return collabs.Select(c => c.Id);
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

    public async Task<IEnumerable<Guid>> GetAllIds()
    {
        var collabs = await _collaboratorRepository.GetAllAsync();
        return collabs.Select(c => c.Id);
    }

}
