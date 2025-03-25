namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    public bool HasColaborator(IColaborator colaborator);
    public IColaborator GetColaborator();
    public IEnumerable<IHolidayPeriod> GetHolidayPeriods();
}
