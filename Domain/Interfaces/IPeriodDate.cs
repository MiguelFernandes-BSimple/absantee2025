namespace Domain.Interfaces;
public interface IPeriodDate
{
    public DateOnly GetInitDate();
    public DateOnly GetFinalDate();
    public bool IsFinalDateSmallerThan(DateOnly date);
    public bool IsInitDateSmallerThan(DateOnly date);
    public bool Intersects(IPeriodDate periodDate);
    public IPeriodDate? GetIntersection(IPeriodDate periodDate);
    public bool Contains(IPeriodDate periodDate);
    public bool ContainsDate(DateOnly date);
    public int Duration();
    public bool ContainsWeekend();
    public int GetNumberOfCommonUtilDaysBetweenPeriods(IPeriodDate periodDate);
    public int GetNumberOfCommonUtilDays();
}