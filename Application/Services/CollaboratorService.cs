using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using System.Linq;
using Domain.Factory;

namespace Application.Services;

public class CollaboratorService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;
    private ICollaboratorRepository _collaboratorRepository;
    private IUserRepository _userRepository;
    private ICheckCollaboratorFactory _checkCollaboratorFactory;

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

    public IEnumerable<ICollaborator> FindAllByProject(long projectId)
    {
        var collabsIds = _associationProjectCollaboratorRepository.FindAllByProject(projectId).Select(a => a.GetCollaboratorId());
        return _collaboratorRepository.Find(c => collabsIds.Contains(c.GetId()));
    }

    public IEnumerable<ICollaborator> FindAllByProjectAndBetweenPeriod(long projectId, IPeriodDate periodDate)
    {
        var collabsIds = _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriod(projectId, periodDate).Select(a => a.GetCollaboratorId());
        return _collaboratorRepository.Find(c => collabsIds.Contains(c.GetId()));
    }

    public bool Add(long userId, IPeriodDateTime periodDateTime)
    {
        var user = _userRepository.GetById((int)userId);
        if (user == null)
            return false;

        ICollaborator colab;
        try
        {
            colab = _checkCollaboratorFactory.Create(user, periodDateTime);
        }
        catch (Exception)
        {
            return false;
        }
        _collaboratorRepository.Add(colab);
        
        return true;
    }
}
