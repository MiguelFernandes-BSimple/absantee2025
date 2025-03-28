using Domain;

public interface IHolidayPeriod
{
    public DateOnly GetInitDate();
    public DateOnly GetFinalDate();
    public bool HolidayPeriodOverlap(IHolidayPeriod periodoFerias);
    int GetDurationInDays(DateOnly start, DateOnly end);
    public int GetNumberOfCommonUtilDaysBetweenPeriods(DateOnly initDate, DateOnly finalDate);
}
