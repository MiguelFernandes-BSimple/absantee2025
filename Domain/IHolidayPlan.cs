namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    public List<IHolidayPeriod> GetHolidayPeriods();
    public int GetDurationInDays(DateOnly initDate, DateOnly endDate);
    public bool HasCollaborator(ICollaborator collab);
    IHolidayPeriod? GetHolidayPeriodContainingDate(DateOnly date);
    IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsBetweenDatesLongerThan(DateOnly initDate, DateOnly endDate, int days);
    public ICollaborator GetCollaborator();
    int GetNumberOfHolidayDaysBetween(DateOnly initDate, DateOnly endDate);
    bool HasPeriodLongerThan(int days);
}
