using Domain.IRepository;
using Domain.Interfaces;
using Domain.Factory;
using Domain.Models;
using System.Threading.Tasks;
using System;
using Application.DTO;

namespace Application.Services;

public class CollaboratorService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;
    private ICollaboratorRepository _collaboratorRepository;
    private IUserRepository _userRepository;
    private ICollaboratorFactory _collaboratorFactory;
    private ITrainingModuleCollaboratorsRepository _trainingModuleCollaboratorsRepository;
    private ITrainingModuleRepository _trainingModuleRepository;

    public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository, ICollaboratorRepository collaboratorRepository, IUserRepository userRepository, ICollaboratorFactory checkCollaboratorFactory)
    {
        _associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        _holidayPlanRepository = holidayPlanRepository;
        _collaboratorRepository = collaboratorRepository;
        _userRepository = userRepository;
        _collaboratorFactory = checkCollaboratorFactory;
    }

    public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository, ICollaboratorRepository collaboratorRepository, IUserRepository userRepository, ICollaboratorFactory collaboratorFactory, ITrainingModuleCollaboratorsRepository trainingModuleCollaboratorsRepository, ITrainingModuleRepository trainingModuleRepository) : this(associationProjectCollaboratorRepository, holidayPlanRepository, collaboratorRepository, userRepository, collaboratorFactory)
    {
        _trainingModuleCollaboratorsRepository = trainingModuleCollaboratorsRepository;
        _trainingModuleRepository = trainingModuleRepository;
    }
    public async Task<bool> Add(CollaboratorDTO collaboratorDTO)
    {
        var collab = await _collaboratorFactory.Create(collaboratorDTO.UserId, collaboratorDTO.PeriodDateTime);
        await _collaboratorRepository.AddAsync(collab);
        await _collaboratorRepository.SaveChangesAsync();
        return true;
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
        Collaborator colab;
        colab = await _collaboratorFactory.Create(userId, periodDateTime);
        await _collaboratorRepository.AddAsync(colab);
        await _collaboratorRepository.SaveChangesAsync();
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

    public async Task<ICollection<ICollaborator>> GetActiveCollaboratorsWithNoTrainingModuleFinishedInSubject(long subjectId)
    {
        // Step 1: Get all active collaborators
        var activeCollaborators = await _collaboratorRepository.GetActiveCollaborators();
        var activeCollaboratorIds = activeCollaborators.Select(c => c.GetId()).ToList();

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
            .Where(c => !collaboratorsWithFinishedModules.Contains(c.GetId()))
            .ToList();

        return result;
    }

    public async Task<IEnumerable<ICollaborator>> GetAllByFinishedTrainingModuleInSubjectAfterPeriod(long subjectId, DateTime date)
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
