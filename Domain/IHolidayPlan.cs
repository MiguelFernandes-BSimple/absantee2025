namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    public bool HasCollaborator(ICollaborator colab);
    IHolidayPeriod? GetHolidayPeriodContainingDate(DateOnly date);
    IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsBetweenDatesLongerThan(DateOnly initDate, DateOnly endDate, int days);
    public ICollaborator GetColaborator();
    public IEnumerable<IHolidayPeriod> GetHolidayPeriods();
    int GetNumberOfHolidayDaysBetween(DateOnly initDate, DateOnly endDate);

    bool HasPeriodLongerThan(int days);
}
