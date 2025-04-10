using Domain.Models;

namespace Domain.Visitor;

public interface IHolidayPeriodVisitor
{
    long Id { get; }
    PeriodDate PeriodDate { get; }
}

