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
        if (_collaboratorRepository.GetById(collaboratorId) == null)
            throw new ArgumentException("Collaborator doesn't exist.");

        if (!await _holidayPlanRepository.CanInsertHolidayPlan(collaboratorId))
            throw new ArgumentException("Holiday plan already exists for this collaborator.");

        var holidayPeriodTasks = periods
            .Select(p => _holidayPeriodFactory.CreateWithoutHolidayPlan(collaboratorId, p.InitDate, p.FinalDate))
            .ToList();

        var holidayPeriods = (await Task.WhenAll(holidayPeriodTasks)).ToList();

        return new HolidayPlan(collaboratorId, holidayPeriods);
    }

    public HolidayPlan Create(IHolidayPlanVisitor visitor)
    {
        return new HolidayPlan(visitor.Id, visitor.CollaboratorId, visitor.GetHolidayPeriods(_mapper));
    }
}