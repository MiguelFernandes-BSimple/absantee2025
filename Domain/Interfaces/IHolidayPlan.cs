namespace Domain.Interfaces;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    public List<IHolidayPeriod> GetHolidayPeriods();
    public int GetDurationInDays(IPeriodDate periodDate);
    public bool HasCollaborator(ICollaborator collab);
    IHolidayPeriod? GetHolidayPeriodContainingDate(DateOnly date);
    IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsBetweenDatesLongerThan(IPeriodDate periodDate, int days);
    public ICollaborator GetCollaborator();
    int GetNumberOfHolidayDaysBetween(IPeriodDate periodDate);
    bool HasPeriodLongerThan(int days);
    public IEnumerable<IHolidayPeriod> GetHolidayPeriodsBetweenPeriod(IPeriodDate period);
}
