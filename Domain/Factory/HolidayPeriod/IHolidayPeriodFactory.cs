using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface IHolidayPeriodFactory
{
    HolidayPeriod Create(Guid holidayPlanId, DateOnly initDate, DateOnly finalDate);
    HolidayPeriod Create(IHolidayPeriodVisitor visitor);
}

