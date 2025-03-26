namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    public bool HasColaborator(IColaborator colaborator);
    public IColaborator GetColaborator();
    public IEnumerable<IHolidayPeriod> GetHolidayPeriods();
    int GetNumberOfHolidayDaysBetween(DateOnly initDate, DateOnly endDate);

    bool HasPeriodLongerThan(int days);
    IColaborator GetColaborator();
}
