using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class HolidayPlanMapper
{
    public HolidayPlan ToDomain(HolidayPlanDataModel holidayPlanDM)
    {
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>(holidayPlanDM.HolidayPeriods);
        HolidayPlan holidayPlan = new HolidayPlan(holidayPlanDM.CollaboratorId, holidayPlanDM.Collaborator, holidayPeriods);

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