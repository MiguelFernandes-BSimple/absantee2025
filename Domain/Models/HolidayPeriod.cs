using Domain.Interfaces;

namespace Domain.Models;

public class HolidayPeriod : IHolidayPeriod
{
    public Guid Id { get; }
    public PeriodDate PeriodDate { get; }

    public HolidayPeriod(PeriodDate periodDate)
    {
        Id = Guid.NewGuid();
        PeriodDate = periodDate;
    }

    public HolidayPeriod(Guid id, PeriodDate periodDate)
    {
        Id = id;
        PeriodDate = periodDate;
    }

    public int GetDuration()
    {
        return PeriodDate.Duration();
    }

    public bool IsLongerThan(int days)
    {
        if (GetDuration() > days)
            return true;

        return false;
    }

    public bool Contains(IHolidayPeriod holidayPeriod)
    {
        return PeriodDate.Contains(holidayPeriod.PeriodDate);
    }

    public bool Contains(PeriodDate periodDate)
    {
        return PeriodDate.Contains(periodDate);
    }

    public bool ContainsDate(DateOnly date)
    {
        return PeriodDate.ContainsDate(date);
    }

    public bool ContainsWeekend()
    {
        return PeriodDate.ContainsWeekend();
    }

    public int GetInterceptionDurationInDays(PeriodDate periodDate)
    {
        PeriodDate? interceptionPeriod = PeriodDate.GetIntersection(periodDate);

        if (interceptionPeriod == null)
            return 0;

        return interceptionPeriod.Duration();
    }

    public int GetNumberOfCommonUtilDaysBetweenPeriods(PeriodDate periodDate)
    {
        return PeriodDate.GetNumberOfCommonUtilDaysBetweenPeriods(periodDate);
    }
    public int GetNumberOfCommonUtilDays()
    {
        return PeriodDate.GetNumberOfCommonUtilDays();
    }

    public bool Intersects(PeriodDate periodDate)
    {
        return PeriodDate.Intersects(periodDate);
    }

    public bool Intersects(IHolidayPeriod holidayPeriod)
    {
        return PeriodDate.Intersects(holidayPeriod.PeriodDate);
    }

}