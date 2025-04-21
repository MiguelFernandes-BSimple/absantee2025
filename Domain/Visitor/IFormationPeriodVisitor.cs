using Domain.Models;

namespace Domain.Visitor
{
    public interface IFormationPeriodVisitor
    {
        long Id { get; }
        PeriodDate PeriodDate { get; }
    }
}