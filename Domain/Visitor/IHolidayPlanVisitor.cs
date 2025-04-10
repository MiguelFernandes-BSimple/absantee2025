using Domain.Interfaces;

namespace Domain.Visitor;

public interface IHolidayPlanVisitor
{
    long Id { get; }
    long CollaboratorId { get; }
    List<IHolidayPeriod> HolidayPeriods { get; }
}

