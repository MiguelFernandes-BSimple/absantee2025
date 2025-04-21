using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory.FormationPeriodFactory;

public interface IFormationPeriodFactory
{
    public FormationPeriod Create(long formationModuleId, PeriodDate periodDate);
    public FormationPeriod Create(IFormationPeriodVisitor formationPeriodVisitor);
}