using System.Threading.Tasks;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class HolidayPeriodFactory : IHolidayPeriodFactory
{
    private readonly IHolidayPlanRepository _holidayPlanRepository;
    private readonly ICollaboratorRepository _collaboratorRepository;

    public HolidayPeriodFactory(IHolidayPlanRepository holidayPlanRepository, ICollaboratorRepository collaboratorRepository)
    {
        _holidayPlanRepository = holidayPlanRepository;
        _collaboratorRepository = collaboratorRepository;
    }

    public async Task<HolidayPeriod> Create(Guid holidayPlanId, DateOnly initDate, DateOnly finalDate)
    {
        PeriodDate periodDate = new PeriodDate(initDate, finalDate);
        HolidayPeriod holidayPeriod = new HolidayPeriod(periodDate);

        if (!await _holidayPlanRepository.CanInsertHolidayPeriod(holidayPlanId, holidayPeriod))
            throw new ArgumentException("Holiday Period already exists for this Holiday Plan.");

        var holidayPlan = await _holidayPlanRepository.GetByIdAsync(holidayPlanId);
        if (holidayPlan == null)
            throw new ArgumentException("Holiday Plan doesn't exist.");

        Guid collaboratorId = holidayPlan.CollaboratorId;
        var collaborator = await _collaboratorRepository.GetByIdAsync(collaboratorId);
        if (collaborator == null)
            throw new ArgumentException("Collaborator doesn't exist.");

        PeriodDateTime periodDateTime = new PeriodDateTime(periodDate);
        if (!collaborator.ContractContainsDates(periodDateTime))
            throw new ArgumentException("Collaborator's contract out of bounds.");

        return holidayPeriod;
    }

    public async Task<HolidayPeriod> CreateWithoutHolidayPlan(Guid collaboratorId, DateOnly initDate, DateOnly finalDate)
    {
        PeriodDate periodDate = new PeriodDate(initDate, finalDate);

        var collaborator = await _collaboratorRepository.GetByIdAsync(collaboratorId);
        if (collaborator == null)
            throw new ArgumentException("Collaborator doesn't exist.");

        PeriodDateTime periodDateTime = new PeriodDateTime(periodDate);
        if (!collaborator.ContractContainsDates(periodDateTime))
            throw new ArgumentException("Collaborator's contract out of bounds.");
        
        //Falta verificar se os periodos não se intersetam

        return new HolidayPeriod(periodDate);
    }

    public HolidayPeriod Create(IHolidayPeriodVisitor visitor)
    {
        return new HolidayPeriod(visitor.Id, visitor.PeriodDate);
    }
}