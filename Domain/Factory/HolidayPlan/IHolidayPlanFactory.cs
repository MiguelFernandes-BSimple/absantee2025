using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface IHolidayPlanFactory
{
    Task<HolidayPlan> Create(Collaborator collaborator, List<PeriodDate> holidayPeriods);
    HolidayPlan Create(IHolidayPlanVisitor visitor);
}

