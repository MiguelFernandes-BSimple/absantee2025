using Domain.Interfaces;

namespace Domain.Visitor;

public interface IHolidayPlanVisitor
{
    Guid Id { get; }
    Guid CollaboratorId { get; }
    List<IHolidayPeriod> GetHolidayPeriods();
}