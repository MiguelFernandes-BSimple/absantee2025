using Domain;
public class HolidayPlanRepository : IHolidayPlanRepository
{
    private List<IHolidayPlan> _holidayPlans;

    public HolidayPlanRepository(List<IHolidayPlan> holidayPlans)
    {
        _holidayPlans = holidayPlans;
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(IColaborator colaborator, DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsLongerThan(int days)
    {
        return _holidayPlans
            .Where(p => p.HasPeriodLongerThan(days))
            .Select(p => p.GetColaborator())
            .Distinct();

    }

    public int GetHolidayDaysOfCollaboratorInProject(IAssociationProjectColaborator association)
    {

        int numberOfHolidayDays = 0;

        IHolidayPlan? collaboratorHolidayPlan = _holidayPlans.SingleOrDefault(p => p.GetColaborator() == association.GetColaborator());

        if (collaboratorHolidayPlan == null)
            return 0;

        numberOfHolidayDays = collaboratorHolidayPlan.GetNumberOfHolidayDaysBetween(association.GetInitDate(), association.GetFinalDate());

        return numberOfHolidayDays;
    }

    public IHolidayPeriod GetHolidayPeriodContainingDate(IColaborator colaborator, DateOnly date)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(IColaborator colaborator, DateOnly initDate, DateOnly endDate, int days)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorThatIncludeWeekends(IColaborator colaborator)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(IColaborator colaborator1, IColaborator colaborator2, DateOnly initDate, DateOnly endDate)
    {
        IEnumerable<IHolidayPeriod> holidayPeriodListColab1 = FindAllHolidayPeriodsForCollaboratorBetweenDates(colaborator1, initDate, endDate);
        IEnumerable<IHolidayPeriod> holidayPeriodListColab2 = FindAllHolidayPeriodsForCollaboratorBetweenDates(colaborator2, initDate, endDate);

        return holidayPeriodListColab1
            .SelectMany(period1 => holidayPeriodListColab2
                .Where(period2 => period1.GetInitDate() <= period2.GetFinalDate() && period1.GetFinalDate() >= period2.GetInitDate())
                .SelectMany(period2 => new List<IHolidayPeriod>{period1, period2}))
                    .Distinct();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(IProject project, DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public int GetHolidayDaysForProjectCollaboratorBetweenDates(IAssociationProjectColaborator association, DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(IProject project, DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }
}
