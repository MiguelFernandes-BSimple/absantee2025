using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface IHolidayPlanFactory
{
    HolidayPlan Create(long collaboratorId);
    HolidayPlan Create(IHolidayPlanVisitor visitor);
}

