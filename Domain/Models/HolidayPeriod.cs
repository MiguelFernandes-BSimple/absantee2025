using Domain.Interfaces;
namespace Domain.Models;

public class HolidayPeriod : IHolidayPeriod
{
    private IPeriodDate _periodDate;

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

    public int GetInterceptionDurationInDays(IPeriodDate periodDate)
    {
        IPeriodDate? interceptionPeriod = _periodDate.GetIntersection(periodDate);

        if (interceptionPeriod == null)
            return 0;

        return interceptionPeriod.Duration();
    }

    public int GetNumberOfCommonUtilDaysBetweenPeriods(IPeriodDate periodDate)
    {
        IPeriodDate? interceptionPeriod = _periodDate.GetIntersection(periodDate);

        if (interceptionPeriod != null)
        {
            int weekdayCount = 0;

            for (DateOnly date = interceptionPeriod.GetInitDate(); date <= interceptionPeriod.GetFinalDate(); date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekdayCount++;
                }
            }
            return weekdayCount;
        }

        return 0;
    }
    public int GetNumberOfCommonUtilDays()
    {

        int weekdayCount = 0;
        for (DateOnly date = _periodDate.GetInitDate(); date <= _periodDate.GetFinalDate(); date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            {
                weekdayCount++;
            }
        }
        return weekdayCount;
    }

    public bool ContainsDate(DateOnly date)
    {
        return _periodDate.ContainsDate(date);
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


    public bool ContainsWeekend()
    {
        return _periodDate.ContainsWeekend();
    }
}
