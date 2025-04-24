using Domain.Models;

namespace Domain.Visitor;

public interface IHolidayPeriodVisitor
{
    Guid Id { get; }
    PeriodDate PeriodDate { get; }
}

