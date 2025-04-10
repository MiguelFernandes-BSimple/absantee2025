using System.Text.RegularExpressions;
using Domain.Interfaces;

namespace Domain.Models;
public class Project : IProject
{
    private long _id;
    private string _title;
    private string _acronym;
    private IPeriodDate _periodDate;

    public Project(string title, string acronym, IPeriodDate periodDate)
    {
        this._title = title;
        this._acronym = acronym;
        this._periodDate = periodDate;
    }


    public long GetId()
    {
        return _id;
    }

    public void SetId(long id)
    {
        _id = id;
    }

    public string GetTitle()
    {
        return _title;
    }

    public string GetAcronym()
    {
        return _acronym;
    }

    public IPeriodDate GetPeriodDate()
    {
        return _periodDate;
    }

    public bool ContainsDates(IPeriodDate periodDate)
    {
        return _periodDate.Contains(periodDate);
    }

    public bool IsFinished()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        return _periodDate.IsFinalDateSmallerThan(today);
    }
}