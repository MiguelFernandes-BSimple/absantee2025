using Domain.Models;

namespace Domain.Interfaces;

public interface IProject
{
    public Guid Id { get; }
    public string Title { get; }
    public string Acronym { get; }
    public PeriodDate PeriodDate { get; }
    public bool ValidateTitle(string title);
    public bool ValidateAcronym(string acronym);
    public void UpdateTitle(string title);
    public void UpdateAcronym(string acronym);
    public void UpdatePeriodDate(PeriodDate periodDate);
    public bool ContainsDates(PeriodDate periodDate);
    public bool IsFinished();

}