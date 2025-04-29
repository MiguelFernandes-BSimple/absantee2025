using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface IHolidayPlanFactory
{
    Task<HolidayPlan> Create(Guid collaboratorId, List<IHolidayPeriod> holidayPeriods);
    HolidayPlan Create(IHolidayPlanVisitor visitor);
}

