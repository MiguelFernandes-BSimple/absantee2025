using Domain;

public interface IHolidayPeriod
{
    public DateOnly GetInitDate();
    public DateOnly GetFinalDate();
    public bool HolidayPeriodOverlap(IHolidayPeriod periodoFerias);
    public bool ContainsDate(DateOnly date);
    public bool ContainedBetween(DateOnly ini, DateOnly end);

    public int Length();
}