using Domain.Interfaces;

namespace Domain.Models;

public class PeriodDateTime : IPeriodDateTime
{
    private DateTime _initDate;
    private DateTime _endDate;

    public PeriodDateTime(DateTime initDate, DateTime endDate)
    {
        if (CheckInputFields(initDate, endDate))
        {
            _initDate = initDate;
            _endDate = endDate;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool CheckInputFields(DateTime initDate, DateTime endDate)
    {
        if (initDate > endDate)
            return false;

        return true;
    }

    public DateTime GetInitDate()
    {
        return _initDate;
    }

    public DateTime GetFinalDate()
    {
        return _endDate;
    }
    public void SetInitDate(DateTime initDate)
    {
        this._initDate = initDate;
    }

    public void SetFinalDate(DateTime endDate)
    {
        this._endDate = endDate;
    }

    public bool IsFinalDateUndefined()
    {
        return _endDate == DateTime.MaxValue;
    }

    public bool Contains(IPeriodDateTime periodDateTime)
    {
        return _initDate <= periodDateTime.GetInitDate()
            && _endDate >= periodDateTime.GetFinalDate();
    }
}

