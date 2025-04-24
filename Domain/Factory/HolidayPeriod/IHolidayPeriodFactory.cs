using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface IHolidayPeriodFactory
{
    HolidayPeriod Create(Guid holidayPlanId, PeriodDate periodDate);
    HolidayPeriod Create(IHolidayPeriodVisitor visitor);
}

