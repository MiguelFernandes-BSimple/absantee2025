using System.Text.RegularExpressions;
using Domain.Interfaces;

namespace Domain.Models;
public class Project : IProject
{
    private long _id;
    private string _title;
    private string _acronym;
    private PeriodDate _periodDate;

    public Project(long id, string title, string acronym, PeriodDate periodDate)
    {
        Regex tituloRegex = new Regex(@"^.{1,50}$");
        Regex siglaRegex = new Regex(@"^[A-Z0-9]{1,10}$");
        if (!tituloRegex.IsMatch(title) || !siglaRegex.IsMatch(acronym))
        {
            throw new ArgumentException("Invalid Arguments");
        }
        this._id = id;
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

    public PeriodDate GetPeriodDate()
    {
        return _periodDate;
    }

    public bool ContainsDates(PeriodDate periodDate)
    {
        return _periodDate.Contains(periodDate);
    }

    public bool IsFinished()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        return _periodDate.IsFinalDateSmallerThan(today);
    }
}