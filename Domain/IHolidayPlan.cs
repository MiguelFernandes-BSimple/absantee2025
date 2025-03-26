namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);

    public IColaborator GetCollaborator();

    public List<IHolidayPeriod>  GetHolidayPeriods();

    public int GetDurationInDays(DateOnly initDate, DateOnly endDate);


}