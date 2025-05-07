using System.Text.RegularExpressions;
using Domain.Interfaces;

namespace Domain.Models;
public class Project : IProject
{
    public Guid Id { get; }
    public string Title { get; }
    public string Acronym { get; }
    public PeriodDate PeriodDate { get; }

    public  Project(string title, string acronym, PeriodDate periodDate)
    {
        Regex titleRegex = new Regex(@"^.{1,50}$");
        Regex acronymRegex = new Regex(@"^[A-Z0-9]{1,10}$");

        if (!titleRegex.IsMatch(title))
            throw new ArgumentException("Title must be between 1 and 50 characters.");

        if (!acronymRegex.IsMatch(acronym))
            throw new ArgumentException("Acronym must be 1 to 10 characters long and contain only uppercase letters and digits.");

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