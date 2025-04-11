using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public class HolidayPlanFactory : IHolidayPlanFactory
{
    private readonly ICollaboratorRepository _collaboratorRepository;

    public HolidayPlanFactory(ICollaboratorRepository collaboratorRepository)
    {
        _collaboratorRepository = collaboratorRepository;
    }

    public HolidayPlan Create(long collaboratorId)
    {
        if (_collaboratorRepository.GetById(collaboratorId) == null)
            throw new ArgumentException("Collaborator doesn't exist.");

        return new HolidayPlan(collaboratorId);
    }

    public HolidayPlan Create(IHolidayPlanVisitor visitor)
    {
        return new HolidayPlan(visitor.Id, visitor.CollaboratorId);
    }
}