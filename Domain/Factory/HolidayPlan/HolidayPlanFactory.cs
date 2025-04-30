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

    public HolidayPlanFactory(ICollaboratorRepository collaboratorRepository, IHolidayPlanRepository holidayPlanRepository, IMapper mapper)
    {
        _collaboratorRepository = collaboratorRepository;
        _holidayPlanRepository = holidayPlanRepository;
        _mapper = mapper;
    }

    public async Task<HolidayPlan> Create(Guid collaboratorId, List<HolidayPeriod> holidayPeriods)
    {
        if (_collaboratorRepository.GetById(collaboratorId) == null)
            throw new ArgumentException("Collaborator doesn't exist.");

        if (!await _holidayPlanRepository.CanInsertHolidayPlan(collaboratorId))
            throw new ArgumentException("Holiday plan already exists for this collaborator.");

        return new HolidayPlan(collaboratorId, holidayPeriods);
    }

    public HolidayPlan Create(IHolidayPlanVisitor visitor)
    {
        return new HolidayPlan(visitor.Id, visitor.CollaboratorId, visitor.GetHolidayPeriods(_mapper));
    }
}