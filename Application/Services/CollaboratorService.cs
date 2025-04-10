using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using Domain.IRepository;
using System.Linq;
using Domain.Factory;
using System.Threading.Tasks;

namespace Application.Services;

public class CollaboratorService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;
    private ICollaboratorRepository _collaboratorRepository;
    private IUserRepository _userRepository;
    private ICollaboratorFactory _checkCollaboratorFactory;

    public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository, ICollaboratorRepository collaboratorRepository)
    {
        _associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        _holidayPlanRepository = holidayPlanRepository;
        _collaboratorRepository = collaboratorRepository;
    }

    //UC15: Como gestor de RH, quero listar os colaboradores que já registaram períodos de férias superiores a x dias 
    public IEnumerable<ICollaborator> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        var collabIds = _holidayPlanRepository.FindAllWithHolidayPeriodsLongerThan(days).Select(hp => hp.GetCollaboratorId());
        return _collaboratorRepository.Find(c => collabIds.Contains(c.GetId()));
    }

    // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
    public IEnumerable<ICollaborator> FindAllWithHolidayPeriodsBetweenDates(IPeriodDate periodDate)
    {
        var collabIds = _holidayPlanRepository.FindHolidayPlansWithinPeriod(periodDate).Select(hp => hp.GetCollaboratorId());
        return _collaboratorRepository.Find(c => collabIds.Contains(c.GetId()));
    }

    public async Task<IEnumerable<ICollaborator>> FindAllByProject(long projectId)
    {
        var collabs = await _associationProjectCollaboratorRepository.FindAllByProjectAsync(projectId);
        var collabsIds = collabs.Select(c => c.GetCollaboratorId());
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
            colab = _checkCollaboratorFactory.Create(userId, periodDateTime);
        }
        catch (Exception)
        {
            return false;
        }
        _collaboratorRepository.Add(colab);
        
        return true;
    }
}
