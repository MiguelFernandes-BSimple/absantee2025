using Domain.Models;

namespace Domain.Visitor;

public interface ITrainingPeriodVisitor
{
    Guid Id { get; }
    PeriodDate PeriodDate { get; }
}