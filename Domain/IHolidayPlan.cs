namespace Domain;

public interface IHolidayPlan
{
    bool AddHolidayPeriod(IHolidayPeriod holidayPeriod);
    public bool HasCollaborator(IColaborator colab);
    IHolidayPeriod? GetHolidayPeriodContainingDate(DateOnly date);
    IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsBetweenDatesLongerThan(DateOnly initDate, DateOnly endDate, int days);
    int GetNumberOfHolidayDaysBetween(DateOnly initDate, DateOnly endDate);

    bool HasPeriodLongerThan(int days);
    IColaborator GetColaborator();
}