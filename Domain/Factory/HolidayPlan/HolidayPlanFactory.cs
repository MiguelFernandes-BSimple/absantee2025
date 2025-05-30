using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public class HolidayPlanFactory : IHolidayPlanFactory
{
    private readonly ICollaboratorRepository _collaboratorRepository;
    private readonly IHolidayPlanRepository _holidayPlanRepository;
    private readonly IMapper _mapper;
    private readonly IHolidayPeriodFactory _holidayPeriodFactory;

    public HolidayPlanFactory(ICollaboratorRepository collaboratorRepository, IHolidayPlanRepository holidayPlanRepository, IMapper mapper, IHolidayPeriodFactory holidayPeriodFactory)
    {
        _collaboratorRepository = collaboratorRepository;
        _holidayPlanRepository = holidayPlanRepository;
        _mapper = mapper;
        _holidayPeriodFactory = holidayPeriodFactory;
    }

    public async Task<HolidayPlan> Create(ICollaborator collaborator, List<PeriodDate> periods)
    {
        if (!await _holidayPlanRepository.CanInsertHolidayPlan(collaborator.UserId))
            throw new ArgumentException("Holiday plan already exists for this collaborator.");

        var holidayPeriods = new List<HolidayPeriod>();

        foreach (var period in periods)
        {
            HolidayPeriod newPeriod;
            try
            {
                newPeriod = _holidayPeriodFactory.CreateWithoutHolidayPlan(
                    collaborator, period.InitDate, period.FinalDate);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error while creating holiday period for dates {period.InitDate} to {period.FinalDate}: {ex.Message}", ex);
            }

            // Check against already-added periods
            if (holidayPeriods.Any(existing => newPeriod.Intersects(existing)))
            {
                throw new ArgumentException($"Holiday periods must not intersect.");
            }

            holidayPeriods.Add(newPeriod);
        }

        return new HolidayPlan(collaborator.Id, holidayPeriods);
    }

    public HolidayPlan Create(IHolidayPlanVisitor visitor)
    {
        return new HolidayPlan(visitor.Id, visitor.CollaboratorId, visitor.GetHolidayPeriods(_mapper));
    }
}