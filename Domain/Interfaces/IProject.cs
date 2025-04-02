namespace Domain.Interfaces;

public interface IProject
{
    public bool ContainsDates(IPeriodDate periodDate);
    public bool IsFinished();
}