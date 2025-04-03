namespace Domain.Interfaces;

public interface IHolidayPeriod
{
    public IPeriodDate GetPeriodDate();
    public bool Contains(IHolidayPeriod holidayPeriod); //overlap
    public int GetInterceptionDurationInDays(IPeriodDate periodDate);
    public int GetDuration();
    public bool ContainsDate(DateOnly date);
    public int GetNumberOfCommonUtilDaysBetweenPeriods(IPeriodDate periodDate);
    public int GetNumberOfCommonUtilDays();
    public bool IsLongerThan(int days);
    public bool Intersects(IPeriodDate periodDate);
    public bool Intersects(IHolidayPeriod holidayPeriod);
    public IPeriodDate? GetIntersectionPeriod(IPeriodDate periodDate);
    public IPeriodDate? GetIntersectionPeriod(IHolidayPeriod holidayPeriod);
    public bool ContainsWeekend();
}
