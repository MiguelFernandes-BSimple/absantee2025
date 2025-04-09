using Domain.IRepository;
using Domain.Interfaces;

namespace Application.Services;
public class HolidayPlanService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;

    public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
    {
        this._associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        this._holidayPlanRepository = holidayPlanRepository;
    }

    //UC16: Como gestor de projeto, quero listar quantos dias de férias um colaborador tem marcado durante um projeto
    public int GetHolidayDaysOfCollaboratorInProject(long projectId, long collaboratorId)
    {

        var association = _associationProjectCollaboratorRepository.FindByProjectAndCollaborator(projectId, collaboratorId) ?? throw new Exception("A associação com os parâmetros fornecidos não existe.");

        int numberOfHolidayDays = 0;

        var collaboratorHolidayPlan = _holidayPlanRepository.FindHolidayPlanByCollaborator(collaboratorId);

        if (collaboratorHolidayPlan == null)
        {
            return numberOfHolidayDays;
        }

        numberOfHolidayDays = collaboratorHolidayPlan.GetNumberOfHolidayDaysBetween(
            association.GetPeriodDate()
        );

        return numberOfHolidayDays;
    }


    // UC19 - Given a collaborator and a period to search for, return the holiday periods that contain weekends.
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(
        ICollaborator collaborator,
        IPeriodDate searchPeriod
    )
    {
        if (!searchPeriod.ContainsWeekend())
            return Enumerable.Empty<IHolidayPeriod>();

        IEnumerable<IHolidayPeriod> holidayPeriodsBetweenDates =
            _holidayPlanRepository.FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator, searchPeriod);

        IEnumerable<IHolidayPeriod> hp = holidayPeriodsBetweenDates
            .Where(hp => hp.ContainsWeekend());

        return hp;
    }

    // UC20 - Given 2 collaborators and a period to search for, return the overlapping holiday periods they have.
    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(
        ICollaborator collaborator1,
        ICollaborator collaborator2,
        IPeriodDate searchPeriod
    )
    {
        IEnumerable<IHolidayPeriod> holidayPeriodListColab1 =
            _holidayPlanRepository.FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator1, searchPeriod);

        IEnumerable<IHolidayPeriod> holidayPeriodListColab2 =
            _holidayPlanRepository.FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator2, searchPeriod);

        IEnumerable<IHolidayPeriod> hp = holidayPeriodListColab1
            .SelectMany(period1 => holidayPeriodListColab2
                    .Where(period2 => period1.Intersects(period2))
                    .SelectMany(period2 => new List<IHolidayPeriod> { period1, period2 }))
                    .Distinct();

        return hp;
    }

    //UC21: Como gestor de projeto, quero listar os períodos de férias dos colaboradores dum projeto, num período
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
        long projectId,
        IPeriodDate period
    )
    {
        var collaborators = _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriod(
            projectId,
            period
        ).Select(a => a.GetCollaboratorId());

        return _holidayPlanRepository.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collaborators.ToList(), period);

    }
    //uc22
    public int GetHolidayDaysForProjectCollaboratorBetweenDates(
        long projectId,
        long collaboratorId,
        IPeriodDate periodDate
    )
    {
        var association = _associationProjectCollaboratorRepository.FindByProjectAndCollaborator(projectId, collaboratorId);
        if (association == null)
        {
            throw new Exception("No association found for the project and collaborator");
        }


        int totalHolidayDays = 0;
        var holidayPeriods = _holidayPlanRepository.FindHolidayPeriodsByCollaboratorBetweenDates(collaboratorId, periodDate);

        foreach (var holidayColabPeriod in holidayPeriods)
        {

            totalHolidayDays += holidayColabPeriod.GetNumberOfCommonUtilDays();
        }

        return totalHolidayDays;
    }

    public int GetHolidayDaysForProjectAllCollaboratorBetweenDates(long project, IPeriodDate searchPeriod)
    {
        var associations = _associationProjectCollaboratorRepository.FindAllByProject(project);

        int totalHolidayDays = 0;

        var collabList = associations.Select(a => a.GetCollaboratorId());
        var holidayPeriods = _holidayPlanRepository.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collabList.ToList(), searchPeriod);

        foreach (var period in holidayPeriods)
        {
            totalHolidayDays += period.GetDuration();
        }

        return totalHolidayDays;
    }
}
