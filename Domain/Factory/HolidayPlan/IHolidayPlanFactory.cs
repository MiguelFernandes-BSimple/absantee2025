using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface IHolidayPlanFactory
{
    HolidayPlan Create(long collaboratorId, List<IHolidayPeriod> holidayPeriods);
    HolidayPlan Create(IHolidayPlanVisitor visitor);
}

