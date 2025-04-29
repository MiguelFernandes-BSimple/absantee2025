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
    private IAssociationTrainingModuleCollaboratorsRepository _trainingModuleCollaboratorsRepository;
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
        _trainingModuleCollaboratorsRepository = trainingModuleCollaboratorsRepository;
        _trainingModuleRepository = trainingModuleRepository;
        _mapper = mapper;
    }

    // uc10
    public async Task<IEnumerable<Guid>> GetByNames(string names)
    {
        var users = await _userRepository.GetByNamesAsync(names);
        var userIds = users.Select(u => u.Id);
        var collabs = await _collaboratorRepository.GetByIdsAsync(userIds);
        return collabs.Select(c => c.Id);
    }

    public async Task<IEnumerable<Guid>> GetBySurnames(string surnames)
    {
        var users = await _userRepository.GetBySurnamesAsync(surnames);
        var userIds = users.Select(u => u.Id);
        var collabs = await _collaboratorRepository.GetByIdsAsync(userIds);
        return collabs.Select(c => c.Id);
    }

    public async Task<IEnumerable<Guid>> GetByNamesAndSurnames(string names, string surnames)
    {
        var users = await _userRepository.GetByNamesAndSurnamesAsync(names, surnames);
        var userIds = users.Select(u => u.Id);
        var collabs = await _collaboratorRepository.GetByIdsAsync(userIds);
        return collabs.Select(c => c.Id);
    }

    public async Task<CollaboratorCreatedDto> Create(CreateCollaboratorDto createCollabDto)
    {

        var user = await _userFactory.Create(createCollabDto.Names, createCollabDto.Surnames, createCollabDto.Email, createCollabDto.deactivationDate);

        if (user == null) return null;

        // corrigir o savechanges - deve ser uma transação
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
    public async Task<IEnumerable<Collaborator>> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        var holidayPlans = await _holidayPlanRepository.FindAllWithHolidayPeriodsLongerThanAsync(days);
        var collabIds = holidayPlans.Select(hp => hp.CollaboratorId);
        return await _collaboratorRepository.GetByIdsAsync(collabIds);
    }

    // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
    public async Task<IEnumerable<Collaborator>> FindAllWithHolidayPeriodsBetweenDates(PeriodDate periodDate)
    {
        var holidayPlans = await _holidayPlanRepository.FindHolidayPlansWithinPeriodAsync(periodDate);
        var collabIds = holidayPlans.Select(hp => hp.CollaboratorId);
        return await _collaboratorRepository.GetByIdsAsync(collabIds);
    }

    public async Task<IEnumerable<CollaboratorDTO>> FindAllByProject(Guid projectId)
    {
        var assocs = await _associationProjectCollaboratorRepository.FindAllByProjectAsync(projectId);
        var collabsIds = assocs.Select(c => c.CollaboratorId);
        var collabs = await _collaboratorRepository.GetByIdsAsync(collabsIds);
        return collabs.Select(c => _mapper.Map<Collaborator, CollaboratorDTO>(c));
    }

    public async Task<IEnumerable<CollaboratorDTO>> FindAllByProjectAndBetweenPeriod(Guid projectId, PeriodDate periodDate)
    {
        var assocs = await _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriodAsync(projectId, periodDate);
        var collabsIds = assocs.Select(c => c.CollaboratorId);
        var collabs = await _collaboratorRepository.GetByIdsAsync(collabsIds);
        return collabs.Select(c => _mapper.Map<Collaborator, CollaboratorDTO>((Collaborator)c));
    }

    public async Task<bool> Add(Guid userId, PeriodDateTime periodDateTime)
    {
        Collaborator colab;
        try
        {
            colab = await _collaboratorFactory.Create(userId, periodDateTime);
            await _collaboratorRepository.AddAsync(colab);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<ICollection<Collaborator>> GetActiveCollaboratorsWithNoTrainingModuleFinishedInSubject(Guid subjectId)
    {
        // Step 1: Get all active collaborators
        var activeCollaborators = await _collaboratorRepository.GetActiveCollaborators();
        var activeCollaboratorIds = activeCollaborators.Select(c => c.Id).ToList();

        // Step 2: Get training modules that are finished for the subject
        var finishedTrainingModules = await _trainingModuleRepository
            .GetBySubjectIdAndFinished(subjectId, DateTime.Now);

        var finishedTrainingModuleIds = finishedTrainingModules.Select(m => m.Id).ToList();

        // Step 3: Get collaborator-module links for those finished modules
        var finishedCollaborators = await _trainingModuleCollaboratorsRepository
            .GetByTrainingModuleIds(finishedTrainingModuleIds);

        var collaboratorsWithFinishedModules = finishedCollaborators
            .Select(c => c.CollaboratorId)
            .Distinct()
            .ToHashSet();

        // Step 4: Filter active collaborators that are NOT in the above list
        var result = activeCollaborators
            .Where(c => !collaboratorsWithFinishedModules.Contains(c.Id))
            .ToList();

        return result;
    }

    public async Task<IEnumerable<Collaborator>> GetAllByFinishedTrainingModuleInSubjectAfterPeriod(Guid subjectId, DateTime date)
    {
        // Step 1: Get training modules that are finished for the subject
        var finishedTrainingModules = await _trainingModuleRepository
            .GetBySubjectIdAndFinished(subjectId, date);

        var trainingModulesIds = finishedTrainingModules.Select(t => t.Id).ToList();

        // Step 2: Get collaborator-module links for those finished modules
        var trainingModuleCollaborators = await _trainingModuleCollaboratorsRepository.GetByTrainingModuleIds(trainingModulesIds);

        var collabsIds = trainingModuleCollaborators.Select(t => t.CollaboratorId).ToList();

        // Step 3: Get collaborators
        var collabs = await _collaboratorRepository.GetByIdsAsync(collabsIds);

        return collabs;
    }
}
