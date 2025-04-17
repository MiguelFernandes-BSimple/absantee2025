using Domain.Models;

namespace Domain.Interfaces;

public interface IProject
{
    public bool ContainsDates(PeriodDate periodDate);
    public bool IsFinished();
    public long GetId();
}