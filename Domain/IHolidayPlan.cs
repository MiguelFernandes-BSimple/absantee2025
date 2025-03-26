namespace Domain;

public interface IHolidayPlan
{
    public List<IHolidayPeriod> GetHolidayPeriodsList();
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    IColaborator GetCollaborator();
    int GetNumberOfHolidayDaysBetween(DateOnly initDate, DateOnly endDate);

    bool HasPeriodLongerThan(int days);
    IColaborator GetColaborator();
}