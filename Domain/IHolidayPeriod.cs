using System.Linq.Expressions;
using Domain;

public interface IHolidayPeriod
{
    public DateOnly GetInitDate();
    public DateOnly GetFinalDate();
    public int GetDuration();
    public bool HolidayPeriodOverlap(IHolidayPeriod periodoFerias);
    public bool IsLongerThan(int days);
}