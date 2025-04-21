
using Domain.Models;

namespace Domain.Visitor;


public interface ITrainingModuleVisitor
{
    long Id { get; }
    Subject Assunto { get; }
    List<PeriodDateTime> Periodos { get; }
}