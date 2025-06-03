using System.Text.RegularExpressions;
using Domain.Interfaces;

namespace Domain.Models;

public class Project : IProject
{
    public Guid Id { get; }
    public string Title { get; private set; }
    public string Acronym { get; private set; }
    public PeriodDate PeriodDate { get; private set; }

    public Project(string title, string acronym, PeriodDate periodDate)
    {
        ValidateTitle(title);
        ValidateAcronym(acronym);

        Id = Guid.NewGuid();
        Title = title;
        Acronym = acronym;
        PeriodDate = periodDate;
    }

    public Project(Guid id, string title, string acronym, PeriodDate periodDate)
    {
        Id = id;
        Title = title;
        Acronym = acronym;
        PeriodDate = periodDate;
    }

    public bool ValidateTitle(string title)
    {
        Regex titleRegex = new Regex(@"^.{1,50}$");

        if (!titleRegex.IsMatch(title))
            throw new ArgumentException("Title must be between 1 and 50 characters.");
        return titleRegex.IsMatch(title);
    }

    public bool ValidateAcronym(string acronym)
    {
        Regex acronymRegex = new Regex(@"^[A-Z0-9]{1,10}$");

        if (!acronymRegex.IsMatch(acronym))
            throw new ArgumentException("Acronym must be 1 to 10 characters long and contain only uppercase letters and digits.");
        return acronymRegex.IsMatch(acronym);
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

    public void UpdateTitle(string title)
    {
        ValidateTitle(title);
        Title = title;
    }

    public void UpdateAcronym(string acronym)
    {
        ValidateAcronym(acronym);
        Acronym = acronym;
    }

    public void UpdatePeriodDate(PeriodDate periodDate)
    {
        PeriodDate = periodDate;
    }
}