using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface IHolidayPeriodFactory
{
    Task<HolidayPeriod> Create(Guid holidayPlanId, DateOnly initDate, DateOnly finalDate);
    HolidayPeriod CreateWithoutHolidayPlan(Collaborator collaborator, DateOnly initDate, DateOnly finalDate);
    HolidayPeriod Create(IHolidayPeriodVisitor visitor);
}

