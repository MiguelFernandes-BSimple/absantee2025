using System.Text.RegularExpressions;
using Domain.Interfaces;

namespace Domain.Models;
public class Project : IProject
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Acronym { get; set; }
    public PeriodDate PeriodDate { get; set; }
    public Project(long id, string title, string acronym, PeriodDate periodDate)
    {
        Regex tituloRegex = new Regex(@"^.{1,50}$");
        Regex siglaRegex = new Regex(@"^[A-Z0-9]{1,10}$");
        if (!tituloRegex.IsMatch(title) || !siglaRegex.IsMatch(acronym))
        {
            throw new ArgumentException("Invalid Arguments");
        }
        this.Id = id;
        this.Title = title;
        this.Acronym = acronym;
        this.PeriodDate = periodDate;
    }


    public long GetId()
    {
        return Id;
    }

    public void SetId(long id)
    {
        Id = id;
    }

    public string GetTitle()
    {
        return Title;
    }

    public string GetAcronym()
    {
        return Acronym;
    }

    public PeriodDate GetPeriodDate()
    {
        return PeriodDate;
    }

    public bool ContainsDates(PeriodDate periodDate)
    {
        return PeriodDate.Contains(periodDate);
    }

    public bool IsFinished()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        return PeriodDate.IsFinalDateSmallerThan(today);
    }
}