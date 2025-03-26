namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    IColaborator GetColaborator();
    IEnumerable<IHolidayPeriod> GetHolidayPeriods();
}
