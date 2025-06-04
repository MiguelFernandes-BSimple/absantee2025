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

    public List<IHolidayPeriod> GetHolidayPeriods(IMapper _mapper)
    {
        if (HolidayPeriods == null)
            return new List<IHolidayPeriod>();

        return HolidayPeriods.Select(h => (IHolidayPeriod)_mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(h)).ToList();
    }

    public HolidayPlanDataModel()
    {
    }
}