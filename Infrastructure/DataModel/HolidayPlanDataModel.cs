using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

[Table("HolidayPlan")]
public class HolidayPlanDataModel : IHolidayPlanVisitor
{
    private IMapper _mapper;
    public long Id { get; set; }
    public long CollaboratorId { get; set; }
    public List<HolidayPeriodDataModel> HolidayPeriodsDM { get; set; }

    public List<IHolidayPeriod> GetHolidayPeriods()
    {
        return HolidayPeriodsDM.Select(hp => (IHolidayPeriod)_mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(hp)).ToList();
    }

    public HolidayPlanDataModel(IHolidayPlan holidayPlan, IMapper mapper)
    {
        _mapper = mapper;
        Id = holidayPlan.GetId();
        CollaboratorId = holidayPlan.GetCollaboratorId();
        HolidayPeriodsDM = holidayPlan.GetHolidayPeriods()
            .Select(h => mapper.Map<HolidayPeriod, HolidayPeriodDataModel>((HolidayPeriod)h)).ToList();
    }

    public HolidayPlanDataModel()
    {
    }
}