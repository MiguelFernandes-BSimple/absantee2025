using Domain.Factory;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class HolidayPlanMapper : IMapper<IHolidayPlan, HolidayPlanDataModel>
{
    private readonly HolidayPlanFactory _holidayPlanFactory;
    private readonly HolidayPeriodMapper _holidayPeriodMapper;

    public HolidayPlanMapper(HolidayPlanFactory holidayPlanFactory, HolidayPeriodMapper holidayPeriodMapper)
    {
        _holidayPlanFactory = holidayPlanFactory;
        _holidayPeriodMapper = holidayPeriodMapper;
    }

    public IHolidayPlan ToDomain(HolidayPlanDataModel holidayPlanDM)
    {
        HolidayPlan holidayPlan = _holidayPlanFactory.Create(holidayPlanDM);

        return holidayPlan;
    }

    public IEnumerable<IHolidayPlan> ToDomain(IEnumerable<HolidayPlanDataModel> holidayPeriodsDM)
    {
        return holidayPeriodsDM.Select(ToDomain);
    }

    public HolidayPlanDataModel ToDataModel(IHolidayPlan holidayPlans)
    {
        return new HolidayPlanDataModel(holidayPlans, _holidayPeriodMapper);
    }

    public IEnumerable<HolidayPlanDataModel> ToDataModel(IEnumerable<IHolidayPlan> holidayPlans)
    {
        return holidayPlans.Select(ToDataModel);
    }
}