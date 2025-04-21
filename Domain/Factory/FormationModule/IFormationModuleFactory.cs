using Domain.Models;
using Domain.Visitor;
namespace Domain.Factory;
using Domain.Interfaces;

public interface IFormationModuleFactory
{
    FormationModule Create(long formationSubjectId, List<IFormationPeriod> formationPeriods);
    FormationModule Create(IFormationModuleVisitor visitor);
}