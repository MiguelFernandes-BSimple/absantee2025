using Domain.Models;

namespace Domain.Visitor;

public interface ITrainingModuleVisitor
{
    public long Id { get; }
    public long SubjectId { get; }
    public List<PeriodDateTime> Periods { get; }
}