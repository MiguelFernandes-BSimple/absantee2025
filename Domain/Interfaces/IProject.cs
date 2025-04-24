using Domain.Models;

namespace Domain.Interfaces;

public interface IProject
{
    public Guid Id { get; }
    public string Title { get; }
    public string Acronym { get; }
    public PeriodDate PeriodDate { get; }
    public bool ContainsDates(PeriodDate periodDate);
    public bool IsFinished();

}