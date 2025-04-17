using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.Mapper;

namespace Infrastructure.DataModel;

[Table("HolidayPlan")]
public class HolidayPlanDataModel : IHolidayPlanVisitor
{
    private IMapper<IHolidayPeriod, HolidayPeriodDataModel> _mapper;
    public long Id { get; set; }
    public long CollaboratorId { get; set; }
    public List<HolidayPeriodDataModel> HolidayPeriodsDM { get; set; }

    public List<IHolidayPeriod> GetHolidayPeriods()
    {
        return _mapper.ToDomain(HolidayPeriodsDM).ToList();
    }

    public HolidayPlanDataModel(IHolidayPlan holidayPlan, IMapper<IHolidayPeriod, HolidayPeriodDataModel> mapper)
    {
        _mapper = mapper;
        Id = holidayPlan.GetId();
        CollaboratorId = holidayPlan.GetCollaboratorId();
        HolidayPeriodsDM = mapper.ToDataModel(holidayPlan.GetHolidayPeriods()).ToList();
    }

    public HolidayPlanDataModel()
    {
    }
}