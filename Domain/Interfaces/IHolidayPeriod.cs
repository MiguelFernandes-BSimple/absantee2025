namespace Domain.Interfaces;

public interface IHolidayPeriod
{
    public IPeriodDate GetPeriodDate();
    public bool Contains(IHolidayPeriod holidayPeriod); //overlap
    public int GetInterceptionDurationInDays(IPeriodDate periodDate);
    public int GetDuration();
    public bool ContainsDate(DateOnly date);
    public bool Intersects(IPeriodDate periodDate);
    public int GetNumberOfCommonUtilDaysBetweenPeriods(IPeriodDate periodDate);
    public bool IsLongerThan(int days);
    public IPeriodDate? GetIntersectionPeriod(IHolidayPeriod holidayPeriod);
    public IPeriodDate? GetIntersectionPeriod(IPeriodDate periodDate);
    public bool ContainsWeekend();
}
