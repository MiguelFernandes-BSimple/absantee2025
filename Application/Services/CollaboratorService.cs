using Infrastructure.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CollaboratorService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;

    public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
    {
        _associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        _holidayPlanRepository = holidayPlanRepository;
    }

    //UC15: Como gestor de RH, quero listar os colaboradores que já registaram períodos de férias superiores a x dias 
    public IEnumerable<ICollaborator> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        return _holidayPlanRepository.FindAllWithHolidayPeriodsLongerThan(days).Select(p => p.GetCollaborator()).Distinct();
    }

    // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
    public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(IPeriodDate periodDate)
    {
        return _holidayPlanRepository.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(periodDate).Select(h => h.GetCollaborator()).Distinct();
    }

    public IEnumerable<ICollaborator> FindAllByProject(IProject project)
    {
        return _associationProjectCollaboratorRepository.FindAllByProject(project).Select(a => a.GetCollaborator());
    }

    public IEnumerable<ICollaborator> FindAllByProjectAndBetweenPeriod(IProject project, IPeriodDate periodDate)
    {
        return _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriod(project, periodDate).Select(a => a.GetCollaborator());
    }

}
