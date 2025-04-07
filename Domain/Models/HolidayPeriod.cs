using Domain.Interfaces;

namespace Domain.Models;

public class HolidayPeriod : IHolidayPeriod
{
    public IPeriodDate _periodDate { get; set; }

    public HolidayPeriod(IPeriodDate periodDate)
    {
        _periodDate = periodDate;
    }

    public IPeriodDate GetPeriodDate()
    {
        return _periodDate;
    }

    public int GetDuration()
    {
        return _periodDate.Duration();
    }

    public bool IsLongerThan(int days)
    {
        if (GetDuration() > days)
            return true;

        return false;
    }

    public bool Contains(IHolidayPeriod holidayPeriod)
    {
        return _periodDate.Contains(holidayPeriod.GetPeriodDate());
    }

    public bool Contains(IPeriodDate periodDate)
    {
        return _periodDate.Contains(periodDate);
    }

    public bool ContainsDate(DateOnly date)
    {
        return _periodDate.ContainsDate(date);
    }

    public bool ContainsWeekend()
    {
        return _periodDate.ContainsWeekend();
    }

    public int GetInterceptionDurationInDays(IPeriodDate periodDate)
    {
        IPeriodDate? interceptionPeriod = _periodDate.GetIntersection(periodDate);

        if (interceptionPeriod == null)
            return 0;

        return interceptionPeriod.Duration();
    }

    public int GetNumberOfCommonUtilDaysBetweenPeriods(IPeriodDate periodDate)
    {
        return _periodDate.GetNumberOfCommonUtilDaysBetweenPeriods(periodDate);
    }
    public int GetNumberOfCommonUtilDays()
    {
        return _periodDate.GetNumberOfCommonUtilDays();
    }

    public bool Intersects(IPeriodDate periodDate)
    {
        return _periodDate.Intersects(periodDate);
    }

    public bool Intersects(IHolidayPeriod holidayPeriod)
    {
        return _periodDate.Intersects(holidayPeriod.GetPeriodDate());
    }

    public IPeriodDate? GetIntersectionPeriod(IPeriodDate periodDate)
    {
        return periodDate.GetIntersection(_periodDate);
    }

    public IPeriodDate? GetIntersectionPeriod(IHolidayPeriod holidayPeriod)
    {
        return holidayPeriod.GetPeriodDate().GetIntersection(_periodDate);
    }
}