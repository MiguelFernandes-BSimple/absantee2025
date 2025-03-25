namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    bool HasPeriodLongerThan(int days);
    IColaborator GetColaborator();
}