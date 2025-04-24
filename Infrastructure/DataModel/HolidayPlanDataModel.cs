using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

public class HolidayPlanDataModel : IHolidayPlanVisitor
{
    private IMapper _mapper;
    public Guid Id { get; set; }
    public Guid CollaboratorId { get; set; }
    public List<HolidayPeriodDataModel> HolidayPeriodsDM { get; set; }

    public List<IHolidayPeriod> GetHolidayPeriods()
    {
        return HolidayPeriodsDM.Select(hp => (IHolidayPeriod)_mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(hp)).ToList();
    }

    public HolidayPlanDataModel(IHolidayPlan holidayPlan, IMapper mapper)
    {
        _mapper = mapper;
        Id = holidayPlan.Id;
        CollaboratorId = holidayPlan.CollaboratorId;
        HolidayPeriodsDM = holidayPlan.HolidayPeriods
            .Select(h => mapper.Map<HolidayPeriod, HolidayPeriodDataModel>((HolidayPeriod)h)).ToList();
    }

    public HolidayPlanDataModel()
    {
    }
}