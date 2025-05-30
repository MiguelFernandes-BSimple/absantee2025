using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface IHolidayPlanFactory
{
    Task<HolidayPlan> Create(ICollaborator collaborator, List<PeriodDate> holidayPeriods);
    HolidayPlan Create(IHolidayPlanVisitor visitor);
}

