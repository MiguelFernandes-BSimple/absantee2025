using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface IHolidayPeriodFactory
{
    HolidayPeriod Create(long holidayPlanId, IPeriodDate periodDate);
    HolidayPeriod Create(IHolidayPeriodVisitor visitor);
}

