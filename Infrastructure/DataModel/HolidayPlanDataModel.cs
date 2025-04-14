using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.DataModel;

[Table("HolidayPlan")]
public class HolidayPlanDataModel
{
    public long Id { get; set; }
    public long CollaboratorId { get; set; }
    public List<IHolidayPeriod> HolidayPeriods { get; set; }

    public HolidayPlanDataModel(HolidayPlan holidayPlan)
    {
        Id = holidayPlan.GetId();
        CollaboratorId = holidayPlan.GetCollaboratorId();
        HolidayPeriods = holidayPlan.GetHolidayPeriods().Cast<IHolidayPeriod>().ToList();
    }
}