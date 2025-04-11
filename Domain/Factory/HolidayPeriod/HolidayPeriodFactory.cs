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

    public HolidayPeriod Create(long holidayPlanId, PeriodDate periodDate)
    {
        if (!_holidayPlanRepository.HolidayPeriodExists(holidayPlanId, periodDate))
            throw new ArgumentException("Holiday Period already exists for this Holiday Plan.");

        var holidayPlan = _holidayPlanRepository.GetById(holidayPlanId);
        if (holidayPlan == null)
            throw new ArgumentException("Holiday Plan doesn't exist.");

        long collaboratorId = holidayPlan.GetCollaboratorId();
        var collaborator = _collaboratorRepository.GetById(collaboratorId);
        if (collaborator == null)
            throw new ArgumentException("Collaborator doesn't exist.");

        PeriodDateTime periodDateTime = new PeriodDateTime(periodDate);
        if (!collaborator.ContractContainsDates(periodDateTime))
            throw new ArgumentException("Collaborator's contract out of bounds.");

        return new HolidayPeriod(periodDate);
    }

    public HolidayPeriod Create(IHolidayPeriodVisitor visitor)
    {
        return new HolidayPeriod(visitor.Id, visitor.PeriodDate);
    }
}