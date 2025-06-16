using Domain.Interfaces;

namespace Domain.Models;

public class AssociationProjectCollaborator : IAssociationProjectCollaborator
{
    public Guid Id { get; }
    public Guid CollaboratorId { get; }
    public Guid ProjectId { get; }
    public PeriodDate PeriodDate { get; }

    public AssociationProjectCollaborator(Guid collaboratorId, Guid projectId, PeriodDate periodDate)
    {
        Id = Guid.NewGuid();
        CollaboratorId = collaboratorId;
        ProjectId = projectId;
        PeriodDate = periodDate;
    }

}
