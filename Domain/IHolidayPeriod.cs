using System.Linq.Expressions;
using Domain;

public interface IHolidayPeriod
{
    public DateOnly GetInitDate();
    public DateOnly GetFinalDate();
    public int GetDuration();
    public bool HolidayPeriodOverlap(IHolidayPeriod periodoFerias);
    public bool ContainsDate(DateOnly date);
    public bool ContainedBetween(DateOnly ini, DateOnly end);
    public int GetNumberOfCommonDaysBetweenPeriods(DateOnly initDate, DateOnly finalDate);
    public bool IsLongerThan(int days);
}