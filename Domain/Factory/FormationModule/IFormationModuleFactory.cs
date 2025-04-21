using Domain.Models;
using Domain.Visitor;
namespace Domain.Factory;

public interface IFormationModuleFactory
{
    FormationModule Create(long formationSubjectId, List<PeriodDateTime> formationPeriods);
    FormationModule Create(IFormationModuleVisitor visitor);
}