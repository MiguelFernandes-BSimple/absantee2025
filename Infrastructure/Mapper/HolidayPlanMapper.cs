using Domain.Factory;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class HolidayPlanMapper : IMapper<HolidayPlan, HolidayPlanDataModel>
{
    private readonly HolidayPlanFactory _holidayPlanFactory;
    private readonly HolidayPeriodMapper _holidayPeriodMapper;

    public HolidayPlanMapper(HolidayPlanFactory holidayPlanFactory, HolidayPeriodMapper holidayPeriodMapper)
    {
        _holidayPlanFactory = holidayPlanFactory;
        _holidayPeriodMapper = holidayPeriodMapper;
    }

    public HolidayPlan ToDomain(HolidayPlanDataModel holidayPlanDM)
    {
        HolidayPlan holidayPlan = _holidayPlanFactory.Create(holidayPlanDM);

        return holidayPlan;
    }

    public IEnumerable<HolidayPlan> ToDomain(IEnumerable<HolidayPlanDataModel> holidayPeriodsDM)
    {
        return holidayPeriodsDM.Select(ToDomain);
    }

    public HolidayPlanDataModel ToDataModel(HolidayPlan holidayPlans)
    {
        return new HolidayPlanDataModel(holidayPlans, _holidayPeriodMapper);
    }

    public IEnumerable<HolidayPlanDataModel> ToDataModel(IEnumerable<HolidayPlan> holidayPlans)
    {
        return holidayPlans.Select(ToDataModel);
    }
}