using Domain.Interfaces;
using Domain.Models;

namespace Domain.Visitor;

public interface IFormationModuleVisitor
{
    long Id { get; }
    long FormationSubjectId { get; }
    List<PeriodDateTime> GetFormationPeriods();
}