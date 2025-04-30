using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface IHolidayPlanFactory
{
    Task<HolidayPlan> Create(Guid collaboratorId, List<HolidayPeriod> holidayPeriods);
    HolidayPlan Create(IHolidayPlanVisitor visitor);
}

