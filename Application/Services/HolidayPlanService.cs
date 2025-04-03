using Infrastructure.Interfaces;
using Domain.Interfaces;
using Domain;
using Domain.Models;

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
    public int GetHolidayDaysOfCollaboratorInProject(IProject project, ICollaborator collaborator)
    {

        var association = _associationProjectCollaboratorRepository.FindByProjectAndCollaborator(project, collaborator);

        if (association == null)
            throw new Exception("A associação com os parâmetros fornecidos não existe.");

        int numberOfHolidayDays = 0;

        var collaboratorHolidayPlan = _holidayPlanRepository.FindHolidayPlanByCollaborator(collaborator);

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
        if (!Utils.ContainsWeekend(searchPeriod.GetInitDate(), searchPeriod.GetFinalDate()))
            return Enumerable.Empty<IHolidayPeriod>();

        IEnumerable<IHolidayPeriod> holidayPeriodsBetweenDates =
            _holidayPlanRepository.FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator, searchPeriod);

        if (!holidayPeriodsBetweenDates.ToList().Any())
            return Enumerable.Empty<IHolidayPeriod>();

        IEnumerable<IHolidayPeriod> hp = holidayPeriodsBetweenDates.Where(holidayPeriod =>
        {
            DateOnly intersectionStart = Utils.DataMax(holidayPeriod.GetPeriodDate().GetInitDate(), searchPeriod.GetInitDate());
            DateOnly intersectionEnd = Utils.DataMin(holidayPeriod.GetPeriodDate().GetFinalDate(), searchPeriod.GetFinalDate());
            return intersectionStart <= intersectionEnd
                && Utils.ContainsWeekend(intersectionStart, intersectionEnd);
        });

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

        if (!holidayPeriodListColab1.ToList().Any())
            return Enumerable.Empty<IHolidayPeriod>();

        IEnumerable<IHolidayPeriod> holidayPeriodListColab2 =
            _holidayPlanRepository.FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator2, searchPeriod);

        if (!holidayPeriodListColab2.ToList().Any())
            return Enumerable.Empty<IHolidayPeriod>();

        IEnumerable<IHolidayPeriod> hp = holidayPeriodListColab1
            .SelectMany(period1 => holidayPeriodListColab2
                    .Where(period2 =>
                        period1.GetPeriodDate().GetInitDate() <= period2.GetPeriodDate().GetFinalDate()
                        && period1.GetPeriodDate().GetFinalDate() >= period2.GetPeriodDate().GetInitDate())
                    .SelectMany(period2 => new List<IHolidayPeriod> { period1, period2 }))
                    .Distinct();

        return hp;
    }

    //UC21: Como gestor de projeto, quero listar os períodos de férias dos colaboradores dum projeto, num período
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
        IProject project,
        IPeriodDate period
    )
    {
        var validCollaborators = _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriod(
            project,
            period
        ).Select(a => a.GetCollaborator());

        return _holidayPlanRepository.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(validCollaborators.ToList(), period);

    }
    //uc22
    public int GetHolidayDaysForProjectCollaboratorBetweenDates(
        IProject project,
        ICollaborator collaborator,
        IPeriodDate periodDate
    )
    {
        var association = _associationProjectCollaboratorRepository.FindByProjectAndCollaborator(project, collaborator);
        if (association == null)
        {
            throw new Exception("No association found for the project and collaborator");
        }


        int totalHolidayDays = 0;
        var holidayPeriods = _holidayPlanRepository.FindHolidayPeriodsByCollaboratorBetweenDates(collaborator, periodDate);

        if(holidayPeriods.Count == 0) {
            throw new Exception("No holiday periods found for the collaborator");
        }

        foreach (var holidayColabPeriod in holidayPeriods)
        {
            PeriodDate period = new PeriodDate(holidayColabPeriod.GetPeriodDate().GetInitDate(), holidayColabPeriod.GetPeriodDate().GetFinalDate());

            totalHolidayDays += holidayColabPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(period);
        }

        return totalHolidayDays;
    }

    public int GetHolidayDaysForProjectAllCollaboratorBetweenDates(IProject project, IPeriodDate searchPeriod)
    {

        var associations = _associationProjectCollaboratorRepository.FindAllByProject(project);

        int totalHolidayDays = 0;

        foreach (var association in associations)
        {
            var holidayPlans = _holidayPlanRepository.GetHolidayPlansByAssociations(association);

            foreach (var holidayPlan in holidayPlans)
            {
                var holidayPeriods = holidayPlan.GetHolidayPeriods().Where(hp => hp.Intersects(searchPeriod));
                foreach (var period in holidayPeriods)
                {
                    totalHolidayDays += period.GetDuration();
                }
            }
        }

        return totalHolidayDays;

    }
}
