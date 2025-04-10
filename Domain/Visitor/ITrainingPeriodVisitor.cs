using Domain.Models;

namespace Domain.Visitor
{
    public interface ITrainingPeriodVisitor
    {
        long Id { get; }
        PeriodDate PeriodDate { get; }
    }
}