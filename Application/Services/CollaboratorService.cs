using Domain.IRepository;
using Domain.Interfaces;
using Domain.Factory;
using Domain.Models;
using Application.DTO;
using AutoMapper;
using Infrastructure;
namespace Application.Services;

public class CollaboratorService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;
    private ICollaboratorRepository _collaboratorRepository;
    private IUserRepository _userRepository;
    private ICollaboratorFactory _collaboratorFactory;
    private IUserFactory _userFactory;
    private IAssociationTrainingModuleCollaboratorsRepository _assocTMCRepository;
    private ITrainingModuleRepository _trainingModuleRepository;
    private AbsanteeContext _context;
    private readonly IMapper _mapper;

    public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository, ICollaboratorRepository collaboratorRepository, IUserRepository userRepository, ICollaboratorFactory checkCollaboratorFactory, IUserFactory userFactory, AbsanteeContext context)
    {
        _associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        _holidayPlanRepository = holidayPlanRepository;
        _collaboratorRepository = collaboratorRepository;
        _userRepository = userRepository;
        _collaboratorFactory = checkCollaboratorFactory;
        _userFactory = userFactory;
        _context = context;
    }

    public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository, ICollaboratorRepository collaboratorRepository, IUserRepository userRepository, ICollaboratorFactory collaboratorFactory, IUserFactory userFactory, IAssociationTrainingModuleCollaboratorsRepository trainingModuleCollaboratorsRepository, ITrainingModuleRepository trainingModuleRepository, AbsanteeContext context, IMapper mapper) : this(associationProjectCollaboratorRepository, holidayPlanRepository, collaboratorRepository, userRepository, collaboratorFactory, userFactory, context)
    {
        _assocTMCRepository = trainingModuleCollaboratorsRepository;
        _trainingModuleRepository = trainingModuleRepository;
        _mapper = mapper;
    }

    //uc9
    public async Task<Result<IEnumerable<Guid>>> GetAll()
    {
        var collabs = await _collaboratorRepository.GetAllAsync();
        var collabIds = collabs.Select(U => U.Id);

        return Result<IEnumerable<Guid>>.Success(collabIds);
    }

    public async Task<Result<CollaboratorDTO>> GetById(Guid id)
    {
        var collab = await _collaboratorRepository.GetByIdAsync(id);
        if (collab == null)
            return Result<CollaboratorDTO>.Failure(Error.NotFound("User not found"));
        var result = _mapper.Map<CollaboratorDTO>(collab);

        return Result<CollaboratorDTO>.Success(result);
    }

    public async Task<long> GetCount()
    {
        return await _collaboratorRepository.GetCount();
    }

    // uc10
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

    public async Task<CollaboratorCreatedDto> Create(CreateCollaboratorDto createCollabDto)
    {

        var user = await _userFactory.Create(createCollabDto.Names, createCollabDto.Surnames, createCollabDto.Email, createCollabDto.deactivationDate);

        if (user == null) return null;

        var createdUser = _userRepository.Add(user);
        if (createdUser == null) return null;

        var collab = await _collaboratorFactory.Create(createdUser, createCollabDto.PeriodDateTime);
        if (collab == null) return null;

        var createdCollab = _collaboratorRepository.Add(collab);
        if (createCollabDto == null) return null;

        _context.SaveChanges();

        return new CollaboratorCreatedDto(createdCollab.Id, createdCollab.UserId, createdCollab.PeriodDateTime);
    }

    //UC15: Como gestor de RH, quero listar os colaboradores que já registaram períodos de férias superiores a x dias 
    public async Task<IEnumerable<CollaboratorDTO>> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        var holidayPlans = await _holidayPlanRepository.FindAllWithHolidayPeriodsLongerThanAsync(days);
        var collabIds = holidayPlans.Select(hp => hp.CollaboratorId);
        var collabs = await _collaboratorRepository.GetByIdsAsync(collabIds);

        return collabs.Select(_mapper.Map<Collaborator, CollaboratorDTO>);
    }

    // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
    public async Task<IEnumerable<CollaboratorDTO>> FindAllWithHolidayPeriodsBetweenDates(PeriodDate periodDate)
    {
        var holidayPlans = await _holidayPlanRepository.FindHolidayPlansWithinPeriodAsync(periodDate);

        var collabIds = holidayPlans.Select(holidayPlans => holidayPlans.CollaboratorId);
        var collabs = await _collaboratorRepository.GetByIdsAsync(collabIds);

        return collabs.Select(_mapper.Map<Collaborator, CollaboratorDTO>);
    }

    public async Task<Result<IEnumerable<CollaboratorDTO>>> FindAllByProject(Guid projectId)
    {
        try
        {
            var assocs = await _associationProjectCollaboratorRepository.FindAllByProjectAsync(projectId);
            var collabsIds = assocs.Select(c => c.CollaboratorId);
            var collabs = await _collaboratorRepository.GetByIdsAsync(collabsIds);
            var result = collabs.Select(_mapper.Map<Collaborator, CollaboratorDTO>);
            return Result<IEnumerable<CollaboratorDTO>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<CollaboratorDTO>>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    public async Task<Result<IEnumerable<CollaboratorDTO>>> FindAllByProjectAndBetweenPeriod(Guid projectId, PeriodDate periodDate)
    {
        try
        {
            var assocs = await _associationProjectCollaboratorRepository.FindAllByProjectAndIntersectingPeriodAsync(projectId, periodDate);
            var collabsIds = assocs.Select(c => c.CollaboratorId);
            var collabs = await _collaboratorRepository.GetByIdsAsync(collabsIds);
            var result = collabs.Select(_mapper.Map<Collaborator, CollaboratorDTO>);
            return Result<IEnumerable<CollaboratorDTO>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<CollaboratorDTO>>.Failure(Error.InternalServerError(ex.Message));
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

    //UC13
    public async Task<IEnumerable<HolidayPeriodDTO>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(Guid collaboratorId, PeriodDate periodDate)
    {
        var result = await _holidayPlanRepository.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(collaboratorId, periodDate);

        return result.Select(_mapper.Map<HolidayPeriod, HolidayPeriodDTO>);
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
