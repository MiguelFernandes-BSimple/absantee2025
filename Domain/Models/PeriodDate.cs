using System.Text.Json.Serialization;
using Domain.Interfaces;

namespace Domain.Models;
public class PeriodDate
{
    public DateOnly initDate { get; set; }
    public DateOnly finalDate { get; set; }

    public PeriodDate(DateOnly initDate, DateOnly finalDate)
    {
        if (initDate > finalDate)
            throw new ArgumentException("Invalid Arguments");
        this.initDate = initDate;
        this.finalDate = finalDate;
    }

    public DateOnly GetInitDate()
    {
        return initDate;
    }

    public DateOnly GetFinalDate()
    {
        return finalDate;
    }

    public bool IsFinalDateSmallerThan(DateOnly date)
    {
        return date > finalDate;
    }

    public bool IsInitDateSmallerThan(DateOnly date)
    {
        return date > initDate;
    }

    public bool Intersects(PeriodDate periodDate)
    {
        return initDate <= periodDate.GetFinalDate() && periodDate.GetInitDate() <= finalDate;
    }

    public PeriodDate? GetIntersection(PeriodDate periodDate)
    {
        DateOnly effectiveStart = initDate > periodDate.GetInitDate() ? initDate : periodDate.GetInitDate();
        DateOnly effectiveEnd = finalDate < periodDate.GetFinalDate() ? finalDate : periodDate.GetFinalDate();

        if (effectiveStart > effectiveEnd)
        {
            return null; // No valid intersection
        }

        return new PeriodDate(effectiveStart, effectiveEnd);
    }

    public int Duration()
    {
        return finalDate.DayNumber - initDate.DayNumber + 1;
    }

    public bool Contains(PeriodDate periodDate)
    {
        return initDate <= periodDate.GetInitDate()
        && finalDate >= periodDate.GetFinalDate();
    }

    public bool ContainsDate(DateOnly date)
    {
        return initDate <= date && finalDate >= date;
    }

    public bool ContainsWeekend()
    {

        for (var date = initDate; date <= finalDate; date = date.AddDays(1))
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
        for (DateOnly date = initDate; date <= finalDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            {
                weekdayCount++;
            }
        }
        return weekdayCount;
    }
}

