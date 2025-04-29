using Domain.Interfaces;

namespace Domain.Models;
public class PeriodDate
{
    public DateOnly InitDate { get; set; }
    public DateOnly FinalDate { get; set; }

    public PeriodDate(DateOnly InitDate, DateOnly FinalDate)
    {
        if (InitDate > FinalDate)
            throw new ArgumentException("Invalid Arguments");
        this.InitDate = InitDate;
        this.FinalDate = FinalDate;
    }

    public PeriodDate() { }


    public DateOnly GetInitDate()
    {
        return InitDate;
    }

    public DateOnly GetFinalDate()
    {
        return FinalDate;
    }

    public bool IsFinalDateSmallerThan(DateOnly date)
    {
        return date > FinalDate;
    }

    public bool IsInitDateSmallerThan(DateOnly date)
    {
        return date > InitDate;
    }

    public bool Intersects(PeriodDate periodDate)
    {
        return InitDate <= periodDate.GetFinalDate() && periodDate.GetInitDate() <= FinalDate;
    }

    public PeriodDate? GetIntersection(PeriodDate periodDate)
    {
        DateOnly effectiveStart = InitDate > periodDate.GetInitDate() ? InitDate : periodDate.GetInitDate();
        DateOnly effectiveEnd = FinalDate < periodDate.GetFinalDate() ? FinalDate : periodDate.GetFinalDate();

        if (effectiveStart > effectiveEnd)
        {
            return null; // No valid intersection
        }

        return new PeriodDate(effectiveStart, effectiveEnd);
    }

    public int Duration()
    {
        return FinalDate.DayNumber - InitDate.DayNumber + 1;
    }

    public bool Contains(PeriodDate periodDate)
    {
        return InitDate <= periodDate.GetInitDate()
        && FinalDate >= periodDate.GetFinalDate();
    }

    public bool ContainsDate(DateOnly date)
    {
        return InitDate <= date && FinalDate >= date;
    }

    public bool ContainsWeekend()
    {

        for (var date = InitDate; date <= FinalDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                return true;
            }
        }
        return false;
    }

    public int GetNumberOfCommonUtilDaysBetweenPeriods(PeriodDate periodDate)
    {
        PeriodDate? interceptionPeriod = GetIntersection(periodDate);

        if (interceptionPeriod != null)
        {
            return interceptionPeriod.GetNumberOfCommonUtilDays();
        }

        return 0;
    }

    public int GetNumberOfCommonUtilDays()
    {
        int weekdayCount = 0;
        for (DateOnly date = InitDate; date <= FinalDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            {
                weekdayCount++;
            }
        }
        return weekdayCount;
    }
}

