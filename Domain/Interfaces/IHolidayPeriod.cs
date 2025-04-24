using Domain.Models;

namespace Domain.Interfaces;

public interface IHolidayPeriod
{
    Guid Id { get; }
    PeriodDate PeriodDate { get; }
    public bool Contains(IHolidayPeriod holidayPeriod);
    public bool Contains(PeriodDate periodDate);
    public bool ContainsDate(DateOnly date);
    public bool ContainsWeekend();
    public int GetInterceptionDurationInDays(PeriodDate periodDate);
    public int GetDuration();
    public int GetNumberOfCommonUtilDaysBetweenPeriods(PeriodDate periodDate);
    public int GetNumberOfCommonUtilDays();
    public bool IsLongerThan(int days);
    public bool Intersects(PeriodDate periodDate);
    public bool Intersects(IHolidayPeriod holidayPeriod);
}
