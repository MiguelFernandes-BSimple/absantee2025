namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    public bool HasCollaborator(ICollaborator collab);
    IHolidayPeriod? GetHolidayPeriodContainingDate(DateOnly date);
    IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsBetweenDatesLongerThan(DateOnly initDate, DateOnly endDate, int days);
    public ICollaborator GetCollaborator();
    public IEnumerable<IHolidayPeriod> GetHolidayPeriods();
    int GetNumberOfHolidayDaysBetween(DateOnly initDate, DateOnly endDate);
    bool HasPeriodLongerThan(int days);
}
