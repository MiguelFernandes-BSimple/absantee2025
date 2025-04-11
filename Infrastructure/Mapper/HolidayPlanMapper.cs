using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class HolidayPlanMapper
{
    public HolidayPlan ToDomain(HolidayPlanDataModel holidayPlanDM)
    {
        List<IHolidayPeriod> holidayPeriods = holidayPlanDM.HolidayPeriods;
        HolidayPlan holidayPlan = new HolidayPlan(holidayPlanDM.CollaboratorId, holidayPeriods);

        holidayPlan.SetId(holidayPlanDM.Id);

        return holidayPlan;
    }

    public IEnumerable<HolidayPlan> ToDomain(IEnumerable<HolidayPlanDataModel> holidayPeriodsDM)
    {
        return holidayPeriodsDM.Select(ToDomain);
    }

    public HolidayPlanDataModel ToDataModel(HolidayPlan holidayPlans)
    {
        return new HolidayPlanDataModel(holidayPlans);
    }

    public IEnumerable<HolidayPlanDataModel> ToDataModel(IEnumerable<HolidayPlan> holidayPlans)
    {
        return holidayPlans.Select(ToDataModel);
    }
}