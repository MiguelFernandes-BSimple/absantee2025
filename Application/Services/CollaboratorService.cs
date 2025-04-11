using Domain.IRepository;
using Domain.Interfaces;
using Domain.Factory;

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
        return _collaboratorRepository.Find(c => collabIds.Contains(c.GetId()));
    }

    // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
    public async Task<IEnumerable<ICollaborator>> FindAllWithHolidayPeriodsBetweenDates(IPeriodDate periodDate)
    {
        var holidayPlans = await _holidayPlanRepository.FindHolidayPlansWithinPeriodAsync(periodDate);
        var collabIds = holidayPlans.Select(hp => hp.GetCollaboratorId());
        return _collaboratorRepository.Find(c => collabIds.Contains(c.GetId()));
    }

    public async Task<IEnumerable<ICollaborator>> FindAllByProject(long projectId)
    {
        var assocs = await _associationProjectCollaboratorRepository.FindAllByProjectAsync(projectId);
        var collabsIds = assocs.Select(c => c.GetCollaboratorId());
        return _collaboratorRepository.Find(c => collabsIds.Contains(c.GetId()));
    }

    public async Task<IEnumerable<ICollaborator>> FindAllByProjectAndBetweenPeriod(long projectId, IPeriodDate periodDate)
    {
        var collabs = await _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriodAsync(projectId, periodDate);
        var collabsIds = collabs.Select(c => c.GetCollaboratorId());
        return _collaboratorRepository.Find(c => collabsIds.Contains(c.GetId()));
    }

    public bool Add(long userId, IPeriodDateTime periodDateTime)
    {
        ICollaborator colab;
        try
        {
            colab = _collaboratorFactory.Create(userId, periodDateTime);
        }
        catch (Exception)
        {
            return false;
        }
        _collaboratorRepository.Add(colab);
        
        return true;
    }

    public async Task<bool> HasNames(long collaboratorId, string names)
    {
        ICollaborator? colab = _collaboratorRepository.GetById(collaboratorId);
        if (colab == null)
            return false;

        return await _userRepository.HasNames(colab.GetUserId(), names);
    }

    public async Task<bool> HasSurnames(long collaboratorId, string surnames)
    {
        ICollaborator? colab = _collaboratorRepository.GetById(collaboratorId);
        if (colab == null)
            return false;

        return await _userRepository.HasSurnames(colab.GetUserId(), surnames);
    }

    public async Task<bool> HasNamesAndSurnames(long collaboratorId, string names, string surnames)
    {
        ICollaborator? colab = _collaboratorRepository.GetById(collaboratorId);
        if (colab == null)
            return false;

        return await _userRepository.HasNamesAndSurnames(colab.GetUserId(), names, surnames);
    }
}
