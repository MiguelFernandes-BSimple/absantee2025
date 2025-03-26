using Domain;

public class HolidayPlanRepository : IHolidayPlanRepository
{
    private List<IHolidayPlan> holidayPlans = new List<IHolidayPlan>();
    private readonly IAssociationProjectColaboratorRepository _associationRepo;

    public HolidayPlanRepository(IAssociationProjectColaboratorRepository associationRepo)
    {
        _associationRepo =
            associationRepo ?? throw new ArgumentNullException(nameof(associationRepo));
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(
        IColaborator colaborator,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(
        DateOnly initDate,
        DateOnly endDate
    )
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsLongerThan(int days)
    {
        throw new NotImplementedException();
    }

    public int GetHolidayDaysInProject(IProject project)
    {
        throw new NotImplementedException();
    }

    public IHolidayPeriod GetHolidayPeriodContainingDate(IColaborator colaborator, DateOnly date)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(
        IColaborator colaborator,
        DateOnly initDate,
        DateOnly endDate,
        int days
    )
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorThatIncludeWeekends(
        IColaborator colaborator
    )
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(
        IColaborator colaborator1,
        IColaborator colaborator2,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        throw new NotImplementedException();
    }

    //uc21
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
        IProject project,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        var validCollaborators = _associationRepo.FindAllProjectCollaboratorsBetween(
            project,
            initDate,
            endDate
        );

        if (project == null)
            throw new ArgumentNullException(nameof(project));
        return holidayPlans
            .Where(hp => validCollaborators.Contains(hp.GetColaborator()))
            .SelectMany(hp =>
                hp.GetHolidayPeriods()
                    .Where(hp => hp.GetInitDate() <= endDate && hp.GetFinalDate() >= initDate)
            );
    }

    //uc22
    public int GetHolidayDaysForProjectCollaboratorBetweenDates(
        IProject project,
        IColaborator colaborator,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        var validCollaborators = _associationRepo.FindAllProjectCollaboratorsBetween(
            project,
            initDate,
            endDate
        );
        if (project == null)
            throw new ArgumentNullException(nameof(project));
        if (colaborator == null)
            throw new ArgumentNullException(nameof(colaborator));
        var holidayPeriods = holidayPlans
            .Where(hp => validCollaborators.Contains(hp.GetColaborator()))
            .SelectMany(hp =>
                hp.GetHolidayPeriods()
                    .Where(hp => hp.GetInitDate() <= endDate && hp.GetFinalDate() >= initDate)
            )
            .ToList();
        if (!holidayPeriods.Any()) // If no matching holiday periods
        {
            return 0;
        }

        return holidayPeriods.Sum(period =>
        {
            var periodStart = period.GetInitDate() < initDate ? initDate : period.GetInitDate();
            var periodEnd = period.GetFinalDate() > endDate ? endDate : period.GetFinalDate();
            return (periodEnd.DayNumber - periodStart.DayNumber) + 1;
        });
    }

    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(
        IProject project,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        throw new NotImplementedException();
    }

    public void AddHolidayPlan(IHolidayPlan holidayPlan)
    {
        holidayPlans.Add(holidayPlan);
    }
}
