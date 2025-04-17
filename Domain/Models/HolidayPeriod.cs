using Domain.Interfaces;

namespace Domain.Models;

public class HolidayPeriod : IHolidayPeriod
{
    private long _id;
    public PeriodDate _periodDate {  get; set; }

    public HolidayPeriod(PeriodDate periodDate)
    {
        _periodDate = periodDate;
    }

    public HolidayPeriod(long id, PeriodDate periodDate)
    {
        _id = id;
        _periodDate = periodDate;
    }

    public long GetId()
    {
        return _id;
    }

    public void SetId(long id)
    {
        _id = id;
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
        return _periodDate.Contains(holidayPeriod._periodDate);
    }

    public bool Contains(PeriodDate periodDate)
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

    public int GetInterceptionDurationInDays(PeriodDate periodDate)
    {
        PeriodDate? interceptionPeriod = _periodDate.GetIntersection(periodDate);

        if (interceptionPeriod == null)
            return 0;

        return interceptionPeriod.Duration();
    }

    public int GetNumberOfCommonUtilDaysBetweenPeriods(PeriodDate periodDate)
    {
        return _periodDate.GetNumberOfCommonUtilDaysBetweenPeriods(periodDate);
    }
    public int GetNumberOfCommonUtilDays()
    {
        return _periodDate.GetNumberOfCommonUtilDays();
    }

    public bool Intersects(PeriodDate periodDate)
    {
        return _periodDate.Intersects(periodDate);
    }

    public bool Intersects(IHolidayPeriod holidayPeriod)
    {
        return _periodDate.Intersects(holidayPeriod._periodDate);
    }

}