using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

[Table("HolidayPlan")]
public class HolidayPlanDataModel : IHolidayPlanVisitor
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