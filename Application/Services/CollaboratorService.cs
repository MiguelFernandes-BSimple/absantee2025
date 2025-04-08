using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using Domain.IRepository;
using System.Linq;

namespace Application.Services;

public class CollaboratorService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;
    private ICollaboratorRepository _collaboratorRepository;

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

    public IEnumerable<ICollaborator> FindAllByProject(IProject project)
    {
        var collabsIds = _associationProjectCollaboratorRepository.FindAllByProject(project).Select(a => a.GetCollaboratorId());
        return _collaboratorRepository.Find(c => collabsIds.Contains(c.GetId()));
    }

    public IEnumerable<ICollaborator> FindAllByProjectAndBetweenPeriod(IProject project, IPeriodDate periodDate)
    {
        var collabsIds = _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriod(project, periodDate).Select(a => a.GetCollaboratorId());
        return _collaboratorRepository.Find(c => collabsIds.Contains(c.GetId()));
    }

}
