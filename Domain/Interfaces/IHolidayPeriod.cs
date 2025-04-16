namespace Domain.Interfaces;

public interface IHolidayPeriod
{
    public long GetId();
    public void SetId(long id);
    public IPeriodDate GetPeriodDate();
    public bool Contains(IHolidayPeriod holidayPeriod);
    public bool Contains(IPeriodDate periodDate);
    public bool ContainsDate(DateOnly date);
    public bool ContainsWeekend();
    public int GetInterceptionDurationInDays(IPeriodDate periodDate);
    public int GetDuration();
    public int GetNumberOfCommonUtilDaysBetweenPeriods(IPeriodDate periodDate);
    public int GetNumberOfCommonUtilDays();
    public bool IsLongerThan(int days);
    public bool Intersects(IPeriodDate periodDate);
    public bool Intersects(IHolidayPeriod holidayPeriod);
}
