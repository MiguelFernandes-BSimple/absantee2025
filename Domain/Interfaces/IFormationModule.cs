using Domain.Models;

namespace Domain.Interfaces;

public interface IFormationModule
{
    public long GetId();
    public List<IFormationPeriod> GetFormationPeriods();
    long GetFormationSubjectId();
}