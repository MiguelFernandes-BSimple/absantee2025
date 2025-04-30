using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

public class HolidayPlanDataModel : IHolidayPlanVisitor
{
    public Guid Id { get; set; }
    public Guid CollaboratorId { get; set; }
    public List<HolidayPeriodDataModel> HolidayPeriods { get; set; } = new List<HolidayPeriodDataModel>();

    public List<HolidayPeriod> GetHolidayPeriods(IMapper _mapper)
    {
        if (HolidayPeriods == null)
            return new List<HolidayPeriod>();

        return HolidayPeriods.Select(_mapper.Map<HolidayPeriodDataModel, HolidayPeriod>).ToList();
    }

    public HolidayPlanDataModel()
    {
    }
}