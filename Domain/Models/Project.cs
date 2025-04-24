using System.Text.RegularExpressions;
using Domain.Interfaces;

namespace Domain.Models;
public class Project : IProject
{
    public Guid Id { get; }
    public string Title { get; }
    public string Acronym { get; }
    public PeriodDate PeriodDate { get; }
    public Project(string title, string acronym, PeriodDate periodDate)
    {
        Regex tituloRegex = new Regex(@"^.{1,50}$");
        Regex acronymRegex = new Regex(@"^[A-Z0-9]{1,10}$");
        if (!tituloRegex.IsMatch(title) || !acronymRegex.IsMatch(acronym))
        {
            throw new ArgumentException("Invalid Arguments");
        }
        this.Id = Guid.NewGuid();
        this.Title = title;
        this.Acronym = acronym;
        this.PeriodDate = periodDate;
    }

    public Project(Guid id, string title, string acronym, PeriodDate periodDate)
    {
        Id = id;
        Title = title;
        Acronym = acronym;
        PeriodDate = periodDate;
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