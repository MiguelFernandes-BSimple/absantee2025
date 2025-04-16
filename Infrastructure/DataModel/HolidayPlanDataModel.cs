using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.Mapper;

namespace Infrastructure.DataModel;

[Table("HolidayPlan")]
public class HolidayPlanDataModel : IHolidayPlanVisitor
{
    private HolidayPeriodMapper _mapper;
    public long Id { get; set; }
    public long CollaboratorId { get; set; }
    private List<HolidayPeriodDataModel> HolidayPeriodsDM;
    
    public List<IHolidayPeriod> GetHolidayPeriods()
    {
        return _mapper.ToDomain(HolidayPeriodsDM).ToList();
    }

    public HolidayPlanDataModel(HolidayPlan holidayPlan, HolidayPeriodMapper mapper)
    {
        _mapper = mapper;
        Id = holidayPlan.GetId();
        CollaboratorId = holidayPlan.GetCollaboratorId();
        HolidayPeriodsDM = mapper.ToDataModel(holidayPlan.GetHolidayPeriods()).ToList();
    }
}