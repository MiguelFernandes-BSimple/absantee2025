namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    IColaborator GetCollaborator();
    int GetNumberOfHolidayDaysBetween(DateOnly initDate, DateOnly endDate);

    bool HasPeriodLongerThan(int days);
    IColaborator GetColaborator();
}