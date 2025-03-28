using System.Linq.Expressions;
using Domain;

public interface IHolidayPeriod
{
    public DateOnly GetInitDate();
    public DateOnly GetFinalDate();
    public bool Contains(IHolidayPeriod periodoFerias); //overlap
    public int GetDurationInDays(DateOnly initDate, DateOnly endDate);
    public int GetDuration();
    public bool ContainsDate(DateOnly date);
    public bool ContainedBetween(DateOnly ini, DateOnly end);
    public int GetNumberOfCommonUtilDaysBetweenPeriods(DateOnly initDate, DateOnly finalDate);
    public bool IsLongerThan(int days);
}
