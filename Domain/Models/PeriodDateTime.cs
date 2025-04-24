using Domain.Interfaces;

namespace Domain.Models;

public class PeriodDateTime
{
    public DateTime InitDate { get; set; }
    public DateTime FinalDate { get; set; }

    public PeriodDateTime(DateTime initDate, DateTime finalDate)
    {
        if (CheckInputFields(initDate, finalDate))
        {
            InitDate = initDate;
            FinalDate = finalDate;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    public PeriodDateTime(PeriodDate periodDate) : this(
        periodDate.GetInitDate().ToDateTime(TimeOnly.MinValue),
        periodDate.GetFinalDate().ToDateTime(TimeOnly.MinValue))
    {
    }

    private bool CheckInputFields(DateTime initDate, DateTime finalDate)
    {
        if (initDate > finalDate)
            return false;

        return true;
    }

    public DateTime GetInitDate()
    {
        return InitDate;
    }

    public DateTime GetFinalDate()
    {
        return FinalDate;
    }
    public void SetFinalDate(DateTime finalDate)
    {
        this.FinalDate = finalDate;
    }

    public bool IsFinalDateUndefined()
    {
        return FinalDate == DateTime.MaxValue;
    }

    public bool IsFinalDateSmallerThan(DateTime date)
    {
        return date > FinalDate;
    }

    public bool Contains(PeriodDateTime periodDateTime)
    {
        return InitDate <= periodDateTime.GetInitDate()
            && FinalDate >= periodDateTime.GetFinalDate();
    }

    public bool Intersects(PeriodDateTime periodDateTime)
    {
        return InitDate <= periodDateTime.GetFinalDate() && periodDateTime.GetInitDate() <= FinalDate;
    }
}

