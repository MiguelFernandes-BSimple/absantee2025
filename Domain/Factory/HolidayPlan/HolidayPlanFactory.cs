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

    public async Task<HolidayPlan> Create(Guid collaboratorId, List<PeriodDate> periods)
    {
        var collab = _collaboratorRepository.GetById(collaboratorId);
        if (collab == null)
            throw new ArgumentException("Collaborator doesn't exist.");

        if (!await _holidayPlanRepository.CanInsertHolidayPlan(collaboratorId))
            throw new ArgumentException("Holiday plan already exists for this collaborator.");

        var holidayPeriods = new List<HolidayPeriod>();

        foreach (var period in periods)
        {
            HolidayPeriod newPeriod;
            try
            {
                newPeriod = _holidayPeriodFactory.CreateWithoutHolidayPlan(
                    collab, period.InitDate, period.FinalDate);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error while creating holiday period for dates {period.InitDate} to {period.FinalDate}: {ex.Message}", ex);
            }

            // Check against already-added periods
            if (holidayPeriods.Any(existing => existing.Intersects(newPeriod)))
            {
                throw new ArgumentException($"Holiday periods must not intersect.");
            }

            holidayPeriods.Add(newPeriod);
        }

        return new HolidayPlan(collaboratorId, holidayPeriods);
    }

    public HolidayPlan Create(IHolidayPlanVisitor visitor)
    {
        return new HolidayPlan(visitor.Id, visitor.CollaboratorId, visitor.GetHolidayPeriods(_mapper));
    }
}