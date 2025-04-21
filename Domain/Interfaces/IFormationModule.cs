using Domain.Models;

namespace Domain.Interfaces;

public interface IFormationModule
{
    public long GetId();
    public List<PeriodDateTime> GetFormationPeriods();
    long GetFormationSubjectId();
}