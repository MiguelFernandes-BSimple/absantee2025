using Domain;

public class HolidayPlanRepository : IHolidayPlanRepository
{
    private List<IHolidayPlan> _holidayPlans = new List<IHolidayPlan>();

    public HolidayPlanRepository(List<IHolidayPlan> holidayPlans)
    {
        _holidayPlans = holidayPlans;
    }

    public HolidayPlanRepository(IHolidayPlan holidayPlan)
    {
        _holidayPlans = new List<IHolidayPlan>() { holidayPlan };
    }


    private bool IsHolidayPeriodValid(IHolidayPeriod period, DateOnly initDate, DateOnly endDate)
    {
        return period.GetInitDate() <= endDate && period.GetFinalDate() >= initDate;
    }

    public IEnumerable<IHolidayPlan> GetHolidayPlansWithHolidayPeriodValid(DateOnly initDate, DateOnly endDate)
    {
        return _holidayPlans.Where(h => h.GetHolidayPeriods().Any(p => IsHolidayPeriodValid(p, initDate, endDate)));
    }

    // US13 - Como gestor de RH, quero listar os períodos de férias dum collaborador num período
    // * todo - colocar in period
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(
        ICollaborator collaborator,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        // isto deve ser verificado dentro de uma classe period, que ainda está por criar
        if (initDate > endDate)
        {
            return Enumerable.Empty<IHolidayPeriod>();
        }
        else
        {
            return _holidayPlans
                .Where(h => h.HasCollaborator(collaborator))
                .SelectMany(h => h.GetHolidayPeriods())
                .Where(p => IsHolidayPeriodValid(p, initDate, endDate));
        }
    }

     public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsLongerThan(int days)
    {
        return _holidayPlans
            .Where(p => p.HasPeriodLongerThan(days))
            .Select(p => p.GetCollaborator())
            .Distinct();
    }

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(
        DateOnly initDate,
        DateOnly endDate
    )
    {
        // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
        if (initDate > endDate)
        {
            return Enumerable.Empty<ICollaborator>();
        }
        else
        {
            return _holidayPlans
                .Where(h =>
                    h.GetHolidayPeriods().Any(p => IsHolidayPeriodValid(p, initDate, endDate))
                )
                .Select(h => h.GetCollaborator())
                .Distinct();
        }
    }

    public int GetHolidayDaysOfCollaboratorInProject(IAssociationProjectCollaborator association)
    {
        int numberOfHolidayDays = 0;

        IHolidayPlan? collaboratorHolidayPlan = _holidayPlans.SingleOrDefault(p =>
            p.GetCollaborator() == association.GetCollaborator()
        );

        if (collaboratorHolidayPlan == null)
            return 0;

        numberOfHolidayDays = collaboratorHolidayPlan.GetNumberOfHolidayDaysBetween(
            association.GetInitDate(),
            association.GetFinalDate()
        );

        return numberOfHolidayDays;
    }

    public IHolidayPeriod? GetHolidayPeriodContainingDate(ICollaborator collaborator, DateOnly date)
    {
        return _holidayPlans
            .Where(a => a.HasCollaborator(collaborator))
            .Select(a => a.GetHolidayPeriodContainingDate(date))
            .FirstOrDefault();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(
        ICollaborator collaborator,
        DateOnly initDate,
        DateOnly endDate,
        int days
    )
    {
        return _holidayPlans
            .Where(a => a.HasCollaborator(collaborator))
            .SelectMany(a =>
                a.FindAllHolidayPeriodsBetweenDatesLongerThan(initDate, endDate, days)
            );
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(
        ICollaborator collaborator,
        DateOnly searchInitDate,
        DateOnly searchEndDate
    )
    {
        if (!Utils.ContainsWeekend(searchInitDate, searchEndDate))
            return Enumerable.Empty<IHolidayPeriod>();

        IEnumerable<IHolidayPeriod> holidayPeriodsBetweenDates =
            FindAllHolidayPeriodsForCollaboratorBetweenDates(
                collaborator,
                searchInitDate,
                searchEndDate
            );

        IEnumerable<IHolidayPeriod> hp = holidayPeriodsBetweenDates.Where(holidayPeriod =>
        {
            DateOnly intersectionStart = Utils.DataMax(holidayPeriod.GetInitDate(), searchInitDate);
            DateOnly intersectionEnd = Utils.DataMin(holidayPeriod.GetFinalDate(), searchEndDate);
            return intersectionStart <= intersectionEnd
                && Utils.ContainsWeekend(intersectionStart, intersectionEnd);
        });

        return hp;
    }

    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(
        IEnumerable<ICollaborator> collaborators,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        int totalHolidayDays = 0;
        foreach (var collaborator in collaborators)
        {
            var holidayPeriods = _holidayPlans
                .Where(hp => hp.GetCollaborator().Equals(collaborator))
                .SelectMany(hp =>
                    hp.GetHolidayPeriods()
                        .Where(hp => hp.GetInitDate() <= endDate && hp.GetFinalDate() >= initDate)
                );

            totalHolidayDays += holidayPeriods.Sum(hp => hp.GetDurationInDays(initDate, endDate));
        }

        return totalHolidayDays;
    }

    //uc21
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(
                List<ICollaborator> validCollaborators,
                DateOnly initDate,
                DateOnly endDate
            )
    {

        {
            return _holidayPlans
                .Where(hp => validCollaborators.Contains(hp.GetCollaborator()))
                .SelectMany(hp =>
                    hp.GetHolidayPeriods()
                        .Where(hp => hp.GetInitDate() <= endDate && hp.GetFinalDate() >= initDate)
                );
        }

    }

    //uc22
    public List<IHolidayPeriod> FindHolidayPeriodsByCollaborator(
            ICollaborator collaborator
        )
    {
        return _holidayPlans.FirstOrDefault(hp =>
            hp.GetCollaborator().Equals(collaborator))?.GetHolidayPeriods() ?? new List<IHolidayPeriod>();
    }

    public IEnumerable<IHolidayPlan> GetHolidayPlansByAssociations(IAssociationProjectCollaborator association)
    {
        var collaborator = association.GetCollaborator();

        return _holidayPlans
            .Where(hp => hp.GetCollaborator().Equals(collaborator));
    }

    public IEnumerable<IHolidayPlan> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        return _holidayPlans.Where(p => p.HasPeriodLongerThan(days));
    }

    public IHolidayPlan? FindHolidayPlanByCollaborator(ICollaborator collaborator)
    {
        return _holidayPlans.SingleOrDefault(p => p.GetCollaborator() == collaborator);
    }
}
