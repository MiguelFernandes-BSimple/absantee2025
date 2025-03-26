using Domain;

public interface IHolidayPeriod
{
    public DateOnly GetInitDate();
    public DateOnly GetFinalDate();
    public bool HolidayPeriodOverlap(IHolidayPeriod periodoFerias);
    public int GetNumberOfCommonDaysBetweenPeriods(DateOnly initDate, DateOnly finalDate);
}