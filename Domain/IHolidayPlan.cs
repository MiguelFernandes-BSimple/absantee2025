namespace Domain;

public interface IHolidayPlan
{
    public List<IHolidayPeriod> GetHolidayPeriodsList();
    public IColaborator GetColaborator();
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
}