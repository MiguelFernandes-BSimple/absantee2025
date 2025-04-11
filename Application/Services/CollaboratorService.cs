using Domain.IRepository;
using Domain.Interfaces;
using Domain.Factory;
using System.Threading.Tasks;

namespace Application.Services;

public class CollaboratorService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;
    private ICollaboratorRepository _collaboratorRepository;
    private IUserRepository _userRepository;
    private ICollaboratorFactory _collaboratorFactory;

    public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository, ICollaboratorRepository collaboratorRepository, IUserRepository userRepository, ICollaboratorFactory checkCollaboratorFactory)
    {
        _associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        _holidayPlanRepository = holidayPlanRepository;
        _collaboratorRepository = collaboratorRepository;
        _userRepository = userRepository;
        _collaboratorFactory = checkCollaboratorFactory;
    }

    //UC15: Como gestor de RH, quero listar os colaboradores que já registaram períodos de férias superiores a x dias 
    public async Task<IEnumerable<ICollaborator>> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        var holidayPlans = await _holidayPlanRepository.FindAllWithHolidayPeriodsLongerThanAsync(days);
        var collabIds = holidayPlans.Select(hp => hp.GetCollaboratorId());
        return await _collaboratorRepository.GetByIdsAsync(collabIds);
    }

    // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
    public async Task<IEnumerable<ICollaborator>> FindAllWithHolidayPeriodsBetweenDates(IPeriodDate periodDate)
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

    public async Task<IEnumerable<ICollaborator>> FindAllByProjectAndBetweenPeriod(long projectId, IPeriodDate periodDate)
    {
        var collabs = await _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriodAsync(projectId, periodDate);
        var collabsIds = collabs.Select(c => c.GetCollaboratorId());
        return await _collaboratorRepository.GetByIdsAsync(collabsIds);
    }

    public async Task<bool> Add(long userId, IPeriodDateTime periodDateTime)
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
        var users = await _userRepository.GetByNames(names);
        var userIds = users.Select(u => u.GetId());
        return await _collaboratorRepository.GetByIdsAsync(userIds);
    }

    public async Task<IEnumerable<ICollaborator>> GetBySurnames(string surnames)
    {
        var users = await _userRepository.GetBySurnames(surnames);
        var userIds = users.Select(u => u.GetId());
        return await _collaboratorRepository.GetByIdsAsync(userIds);
    }

    public async Task<IEnumerable<ICollaborator>> GetByNamesAndSurnames(string names, string surnames)
    {
        var users = await _userRepository.GetByNamesAndSurnames(names, surnames);
        var userIds = users.Select(u => u.GetId());
        return await _collaboratorRepository.GetByIdsAsync(userIds);
    }
}
