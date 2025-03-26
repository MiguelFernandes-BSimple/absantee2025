namespace Domain;

public interface IHolidayPlan
{
    public List<IHolidayPeriod> GetHolidayPeriodsList();
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    bool HasPeriodLongerThan(int days);
    IColaborator GetColaborator();
}