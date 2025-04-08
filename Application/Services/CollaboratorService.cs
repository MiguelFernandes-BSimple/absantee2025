using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using Domain.IRepository;

namespace Application.Services;

public class CollaboratorService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;
    private ICollaboratorRepository _collaboratorRepository;

    public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
    {
        _associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        _holidayPlanRepository = holidayPlanRepository;
    }

    //UC15: Como gestor de RH, quero listar os colaboradores que já registaram períodos de férias superiores a x dias 
    public IEnumerable<ICollaborator> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        return _holidayPlanRepository.FindAllWithHolidayPeriodsLongerThan(days).Select(p => p.GetCollaborator());
    }

    // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
    public IEnumerable<ICollaborator> FindAllWithHolidayPeriodsBetweenDates(IPeriodDate periodDate)
    {
        return _holidayPlanRepository.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(periodDate).Select(h => h.GetCollaborator());
    }

    public IEnumerable<ICollaborator> FindAllByProject(IProject project)
    {
        var collabsIds = _associationProjectCollaboratorRepository.FindAllByProject(project).Select(a => a.GetCollaboratorId());
        return _collaboratorRepository.Find(c => collabsIds.Contains(c.GetId()));
    }

    public IEnumerable<ICollaborator> FindAllByProjectAndBetweenPeriod(IProject project, IPeriodDate periodDate)
    {
        return _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriod(project, periodDate).Select(a => a.GetCollaborator());
    }

}
