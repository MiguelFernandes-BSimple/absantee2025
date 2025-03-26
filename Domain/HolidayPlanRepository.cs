using System.IO.Compression;
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
        _holidayPlans = new List<IHolidayPlan>(){ holidayPlan };
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

    public IHolidayPeriod? GetHolidayPeriodContainingDate(IColaborator colaborator, DateOnly date)
    {
        return _holidayPlans.Where(a => a.HasCollaborator(colaborator))
                .Select(a => a.GetHolidayPeriodContainingDate(date)).FirstOrDefault();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(IColaborator colaborator, DateOnly initDate, DateOnly endDate, int days)
    {
        return _holidayPlans.Where(a => a.HasCollaborator(colaborator))
                .SelectMany(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(initDate, endDate, days));
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorThatIncludeWeekends(IColaborator colaborator)
    {
        throw new NotImplementedException();
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
