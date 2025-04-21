using Domain.IRepository;
using Domain.Interfaces;
using Domain.Factory;
using Domain.Models;
using System.Threading.Tasks;
using System.IO.Compression;

namespace Application.Services;

public class CollaboratorService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;
    private ICollaboratorRepository _collaboratorRepository;
    private IUserRepository _userRepository;
    private ICollaboratorFactory _collaboratorFactory;
    private IFormationSubjectRepository _formationSubjectRepository;
    private IFormationModuleRepository _formationModuleRepository;
    private IAssociationFormationModuleCollaboratorRepository _associationFormationModuleCollaboratorRepository;

    public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository, ICollaboratorRepository collaboratorRepository, IUserRepository userRepository, ICollaboratorFactory checkCollaboratorFactory, IFormationModuleRepository formationModuleRepository, IFormationSubjectRepository formationSubjectRepository, IAssociationFormationModuleCollaboratorRepository associationFormationModuleCollaboratorRepository)
    {
        _associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        _holidayPlanRepository = holidayPlanRepository;
        _collaboratorRepository = collaboratorRepository;
        _userRepository = userRepository;
        _collaboratorFactory = checkCollaboratorFactory;
        _formationModuleRepository = formationModuleRepository;
        _formationSubjectRepository = formationSubjectRepository;
        _associationFormationModuleCollaboratorRepository = associationFormationModuleCollaboratorRepository;
    }

    //UC15: Como gestor de RH, quero listar os colaboradores que já registaram períodos de férias superiores a x dias 
    public async Task<IEnumerable<ICollaborator>> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        var holidayPlans = await _holidayPlanRepository.FindAllWithHolidayPeriodsLongerThanAsync(days);
        var collabIds = holidayPlans.Select(hp => hp.GetCollaboratorId());
        return await _collaboratorRepository.GetByIdsAsync(collabIds);
    }

    // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
    public async Task<IEnumerable<ICollaborator>> FindAllWithHolidayPeriodsBetweenDates(PeriodDate periodDate)
    {
        var holidayPlans = await _holidayPlanRepository.FindHolidayPlansWithinPeriodAsync(periodDate);
        var collabIds = holidayPlans.Select(hp => hp.GetCollaboratorId());
        return await _collaboratorRepository.GetByIdsAsync(collabIds);
    }

    public async Task<IEnumerable<ICollaborator>> FindAllByProject(long projectId)
    {
        var assocs = await _associationProjectCollaboratorRepository.FindAllByProjectAsync(projectId);
        var collabsIds = assocs.Select(c => c.GetCollaboratorId());
        return await _collaboratorRepository.GetByIdsAsync(collabsIds);
    }

    public async Task<IEnumerable<ICollaborator>> FindAllByProjectAndBetweenPeriod(long projectId, PeriodDate periodDate)
    {
        var collabs = await _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriodAsync(projectId, periodDate);
        var collabsIds = collabs.Select(c => c.GetCollaboratorId());
        return await _collaboratorRepository.GetByIdsAsync(collabsIds);
    }

    public async Task<bool> Add(long userId, PeriodDateTime periodDateTime)
    {
        ICollaborator colab;
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

    public async Task<IEnumerable<ICollaborator>> GetByNames(string names)
    {
        var users = await _userRepository.GetByNamesAsync(names);
        var userIds = users.Select(u => u.GetId());
        return await _collaboratorRepository.GetByIdsAsync(userIds);
    }

    public async Task<IEnumerable<ICollaborator>> GetBySurnames(string surnames)
    {
        var users = await _userRepository.GetBySurnamesAsync(surnames);
        var userIds = users.Select(u => u.GetId());
        return await _collaboratorRepository.GetByIdsAsync(userIds);
    }

    public async Task<IEnumerable<ICollaborator>> GetByNamesAndSurnames(string names, string surnames)
    {
        var users = await _userRepository.GetByNamesAndSurnamesAsync(names, surnames);
        var userIds = users.Select(u => u.GetId());
        return await _collaboratorRepository.GetByIdsAsync(userIds);
    }

    public async Task<IEnumerable<ICollaborator>> GetActiveCollaboratorsWithoutFormationIn(string formationModuleTitle)
    {
        var formationSubject = await _formationSubjectRepository.GetByTitleAsync(formationModuleTitle);

        if (formationSubject == null)
        {
            throw new ArgumentException("Given subject does not exist.");
        }

        var formationModule = await _formationModuleRepository.GetBySubjectId(formationSubject.GetId());

        if (formationModule == null)
        {
            throw new ArgumentException("There is no formation module with given subject.");
        }

        var associations = await _associationFormationModuleCollaboratorRepository.FindAllByFormationModuleAsync(formationModule.GetId());

        var collabIdsWithModule = associations
            .Select(a => a.GetCollaboratorId())
            .ToList();

        var activeCollaborators = await _collaboratorRepository.GetActiveCollaboratorsAsync();

        var collaboratorWithoutFormation = activeCollaborators
                .Where(c => !collabIdsWithModule.Contains(c.GetId()));

        return collaboratorWithoutFormation;
    }

    public async Task<IEnumerable<ICollaborator>> GetCollaboratorsWithFinishedFormationOfSubjectAfter(string formationModuleTitle, DateOnly date)
    {
        var formationSubject = await _formationSubjectRepository.GetByTitleAsync(formationModuleTitle);

        if (formationSubject == null)
        {
            throw new ArgumentException("Given subject does not exist.");
        }

        var formationModule = await _formationModuleRepository.GetBySubjectId(formationSubject.GetId());

        if (formationModule == null)
        {
            throw new ArgumentException("There is no formation module with given subject.");
        }

        var associations = await _associationFormationModuleCollaboratorRepository.FindAllByFormationModuleAsync(formationModule.GetId());

        var associationsFinishedAfter = associations.Where(a => a._periodDate.GetFinalDate() > date);

        var collaboratorIds = associationsFinishedAfter.Select(a => a.GetCollaboratorId()).Distinct();

        var collaborators = await _collaboratorRepository.GetByIdsAsync(collaboratorIds);

        return collaborators;
    }

}






