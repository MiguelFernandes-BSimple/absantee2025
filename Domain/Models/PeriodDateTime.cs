using Domain.Interfaces;

namespace Domain.Models;

public class PeriodDateTime
{
    public DateTime _initDate { get; set; }
    public DateTime _finalDate { get; set; }

    public PeriodDateTime() { }


    public PeriodDateTime(DateTime initDate, DateTime finalDate)
    {
        if (CheckInputFields(initDate, finalDate))
        {
            _initDate = initDate;
            _finalDate = finalDate;
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
        return _initDate;
    }

    public DateTime GetFinalDate()
    {
        return _finalDate;
    }
    public void SetFinalDate(DateTime finalDate)
    {
        this._finalDate = finalDate;
    }

    public bool IsFinalDateUndefined()
    {
        return _finalDate == DateTime.MaxValue;
    }

    public bool IsFinalDateSmallerThan(DateTime date)
    {
        return date > _finalDate;
    }

    public bool Contains(PeriodDateTime periodDateTime)
    {
        return _initDate <= periodDateTime.GetInitDate()
            && _finalDate >= periodDateTime.GetFinalDate();
    }

    public bool Intersects(PeriodDateTime periodDateTime)
    {
        return _initDate <= periodDateTime.GetFinalDate() && periodDateTime.GetInitDate() <= _finalDate;
    }

    public bool IsOnGoing()
    {
        return _initDate <= DateTime.Now && _finalDate >= DateTime.Now;
    }
}

