using Domain.Models;

namespace Domain.Interfaces;

public interface IProject
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Acronym { get; set; }
    public PeriodDate PeriodDate { get; set; }
    public bool ContainsDates(PeriodDate periodDate);
    public bool IsFinished();
    public long GetId();
    public string GetTitle();
    public string GetAcronym();
    public PeriodDate GetPeriodDate();
}