
using Domain.Models;

namespace Domain.Visitor;


public interface ITrainingModuleVisitor
{
    long Id { get; }
    long subjectId { get; }
    List<PeriodDateTime> Periodos { get; }
}