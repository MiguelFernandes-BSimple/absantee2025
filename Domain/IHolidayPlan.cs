namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    bool HasPeriodBiggerThan(int days);
    IColaborator GetColaborator();
}