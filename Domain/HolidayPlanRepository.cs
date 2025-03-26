using Domain;
public class HolidayPlanRepository : IHolidayPlanRepository
{
    private List<IHolidayPlan> _holidayPlans = new List<IHolidayPlan>();

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

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(IColaborator colaborator, DateOnly initDate, DateOnly endDate, int days)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(IColaborator colaborator, DateOnly initDate, DateOnly endDate)
    {

        bool hasWeekend = false;
        for (var date = initDate; date <= endDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                hasWeekend = true;
                break;
            }
        }
        if (!hasWeekend)
            throw new Exception("The given period does not include a weekend.");

        // GetHolidayPeriodsListByCollaborator   ????
        List<IHolidayPeriod> holidayPeriodsList = _holidayPlans
            .FirstOrDefault(holidayPlan => holidayPlan.GetColaborator().Equals(colaborator))
            ?.GetHolidayPeriodsList() ?? throw new Exception("The collaborator doesn't have an Holiday Plan.");


        // GetHolidayPeriodListBetweenDates ????
        List<IHolidayPeriod> holidayPeriodsBetweenDates = holidayPeriodsList
            .Where(holidayPeriod => holidayPeriod.GetInitDate() <= endDate && holidayPeriod.GetFinalDate() >= initDate)
            .ToList();

        if (holidayPeriodsBetweenDates.Count == 0)
            throw new Exception("The collaborator doesn't have an Holiday Plan in the given period.");


        List<IHolidayPeriod> holidayPeriodsBetweenDatesThatIncludeWeekends = holidayPeriodsBetweenDates
            .Where(holidayPeriod =>
                Enumerable.Range(0, holidayPeriod.GetFinalDate().DayNumber - holidayPeriod.GetInitDate().DayNumber + 1)
                .Any(days => holidayPeriod.GetInitDate().AddDays(days).DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Saturday))
                .ToList();

        if (holidayPeriodsBetweenDatesThatIncludeWeekends.Count == 0)
            throw new Exception("The collaborator doesn't have an Holiday Period that include weekends in the given period.");


        return holidayPeriodsBetweenDatesThatIncludeWeekends;
    }

    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(IColaborator colaborator1, IColaborator colaborator2, DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
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
