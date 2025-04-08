using Domain.Interfaces;

namespace Domain.Models;
public class PeriodDate : IPeriodDate
{


    private long _id;
    
    private DateOnly _initDate;
    private DateOnly _finalDate;

    public PeriodDate(DateOnly initDate, DateOnly finalDate)
    {
        if (initDate > finalDate)
            throw new ArgumentException("Invalid Arguments");
        _initDate = initDate;
        _finalDate = finalDate;
    }

    public void SetId(long id)
    {
        _id = id;
    }

    public long GetId()
    {
        return _id;
    }


    public DateOnly GetInitDate()
    {
        return _initDate;
    }

    public DateOnly GetFinalDate()
    {
        return _finalDate;
    }

    public bool IsFinalDateSmallerThan(DateOnly date)
    {
        return date > _finalDate;
    }

    public bool IsInitDateSmallerThan(DateOnly date)
    {
        return date > _initDate;
    }

    public bool Intersects(IPeriodDate periodDate)
    {
        return _initDate <= periodDate.GetFinalDate() && periodDate.GetInitDate() <= _finalDate;
    }

    public IPeriodDate? GetIntersection(IPeriodDate periodDate)
    {
        DateOnly effectiveStart = _initDate > periodDate.GetInitDate() ? _initDate : periodDate.GetInitDate();
        DateOnly effectiveEnd = _finalDate < periodDate.GetFinalDate() ? _finalDate : periodDate.GetFinalDate();

        if (effectiveStart > effectiveEnd)
        {
            return null; // No valid intersection
        }

        return new PeriodDate(effectiveStart, effectiveEnd);
    }

    public int Duration()
    {
        return _finalDate.DayNumber - _initDate.DayNumber + 1;
    }

    public bool Contains(IPeriodDate periodDate)
    {
        return _initDate <= periodDate.GetInitDate()
        && _finalDate >= periodDate.GetFinalDate();
    }

    public bool ContainsDate(DateOnly date)
    {
        return _initDate <= date && _finalDate >= date;
    }

    public bool ContainsWeekend()
    {

        for (var date = _initDate; date <= _finalDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                return true;
            }
        }
        return false;
    }

    public int GetNumberOfCommonUtilDaysBetweenPeriods(IPeriodDate periodDate)
    {
        IPeriodDate? interceptionPeriod = GetIntersection(periodDate);

        if (interceptionPeriod != null)
        {
            return interceptionPeriod.GetNumberOfCommonUtilDays();
        }

        return 0;
    }

    public int GetNumberOfCommonUtilDays()
    {
        int weekdayCount = 0;
        for (DateOnly date = _initDate; date <= _finalDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            {
                weekdayCount++;
            }
        }
        return weekdayCount;
    }
}

