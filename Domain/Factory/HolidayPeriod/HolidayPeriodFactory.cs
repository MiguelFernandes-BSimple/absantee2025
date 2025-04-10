using System.Runtime.CompilerServices;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public class HolidayPeriodFactory : IHolidayPeriodFactory
{
    private readonly IHolidayPlanRepository _holidayPlanRepository;

    public HolidayPeriodFactory(IHolidayPlanRepository holidayPlanRepository)
    {
        _holidayPlanRepository = holidayPlanRepository;
    }

    public HolidayPeriod Create(long holidayPlanId, PeriodDate periodDate)
    {
        // is unique 
        if (_holidayPlanRepository.Exists(holidayPlanId, periodDate))
            return null;

        return new HolidayPeriod(periodDate);
    }

    public HolidayPeriod Create(IHolidayPeriodVisitor visitor)
    {
        return new HolidayPeriod(visitor.Id, visitor.PeriodDate);
    }
}