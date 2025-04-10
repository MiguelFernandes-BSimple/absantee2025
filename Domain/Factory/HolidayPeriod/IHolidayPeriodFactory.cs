using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface IHolidayPeriodFactory
{
    HolidayPeriod Create(long holidayPlanId, PeriodDate periodDate);
    HolidayPeriod Create(IHolidayPeriodVisitor visitor);
}

