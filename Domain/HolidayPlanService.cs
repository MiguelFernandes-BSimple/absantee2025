namespace Domain;

public class HolidayPlanService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;

    public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
    {
        this._associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        this._holidayPlanRepository = holidayPlanRepository;
    }

    // UC19 - Given a collaborator and a period to search for, return the holiday periods that contain weekends.
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(
        ICollaborator collaborator,
        DateOnly searchInitDate,
        DateOnly searchEndDate
    )
    {
        if (!Utils.ContainsWeekend(searchInitDate, searchEndDate))
            return Enumerable.Empty<IHolidayPeriod>();

        IEnumerable<IHolidayPeriod> holidayPeriodsBetweenDates =
            _holidayPlanRepository.FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator, searchInitDate, searchEndDate);

        IEnumerable<IHolidayPeriod> hp = holidayPeriodsBetweenDates.Where(holidayPeriod =>
        {
            DateOnly intersectionStart = Utils.DataMax(holidayPeriod.GetInitDate(), searchInitDate);
            DateOnly intersectionEnd = Utils.DataMin(holidayPeriod.GetFinalDate(), searchEndDate);
            return intersectionStart <= intersectionEnd
                && Utils.ContainsWeekend(intersectionStart, intersectionEnd);
        });

        return hp;
    }

    // UC20 - Given 2 collaborators and a period to search for, return the overlapping holiday periods they have.
    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(
        ICollaborator collaborator1,
        ICollaborator collaborator2,
        DateOnly searchInitDate,
        DateOnly searchEndDate
    )
    {
        IEnumerable<IHolidayPeriod> holidayPeriodListColab1 =
            _holidayPlanRepository.FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator1, searchInitDate, searchEndDate);

        IEnumerable<IHolidayPeriod> holidayPeriodListColab2 =
            _holidayPlanRepository.FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator2, searchInitDate, searchEndDate);

        IEnumerable<IHolidayPeriod> hp = holidayPeriodListColab1
            .SelectMany(period1 => holidayPeriodListColab2
                    .Where(period2 =>
                        period1.GetInitDate() <= period2.GetFinalDate()
                        && period1.GetFinalDate() >= period2.GetInitDate())
                    .SelectMany(period2 => new List<IHolidayPeriod> { period1, period2 }))
                    .Distinct();

        return hp;
    }

    //UC21: Como gestor de projeto, quero listar os períodos de férias dos colaboradores dum projeto, num período
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
        IProject project,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        var validCollaborators = _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriod(
            project,
            initDate,
            endDate
        ).Select(a => a.GetCollaborator());

        if (initDate > endDate)
        {
            return Enumerable.Empty<IHolidayPeriod>();
        }
        return _holidayPlanRepository.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(validCollaborators.ToList(), initDate, endDate);

    }
    //uc22
    public int GetHolidayDaysForProjectCollaboratorBetweenDates(
        IProject project,
        ICollaborator collaborator,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        if (initDate > endDate)
        {
            return 0;
        }
        var association = _associationProjectCollaboratorRepository.FindByProjectandCollaborator(project, collaborator);
        if (association == null)
        {
            throw new Exception("");
        }


        int totalHolidayDays = 0;
        var holidayPeriods = _holidayPlanRepository.FindHolidayPeriodsByCollaborator(collaborator);

        foreach (var holidayColabPeriod in holidayPeriods)
        {
            DateOnly holidayStart = holidayColabPeriod.GetInitDate();
            DateOnly holidayEnd = holidayColabPeriod.GetFinalDate();

            totalHolidayDays += holidayColabPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(
                holidayStart,
                holidayEnd
            );
        }

        return totalHolidayDays;
    }

    public int GetHolidayDaysForProjectCollaboratorBetweenDates(IProject project, DateOnly initDate, DateOnly endDate)
    {
        if (initDate > endDate)
        {
            return 0;
        }

        var associations = _associationProjectCollaboratorRepository.FindAllByProject(project);

        int totalHolidayDays = 0;

        foreach (var association in associations)
        {
            var holidayPlans = _holidayPlanRepository.GetHolidayPlansByAssociations(association);

            foreach (var holidayPlan in holidayPlans)
            {
                var holidayPeriods = holidayPlan.GetHolidayPeriods()
                    .Where(hp => hp.GetInitDate() <= endDate && hp.GetFinalDate() >= initDate);

                foreach (var period in holidayPeriods)
                {
                    totalHolidayDays += period.GetDurationInDays(initDate, endDate);
                }
            }
        }

        return totalHolidayDays;
    }
}
