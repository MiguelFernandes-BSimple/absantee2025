using Domain.Interfaces;

namespace Domain.Models;

public class AssociationProjectCollaborator : IAssociationProjectCollaborator
{
    public long Id { get; set; }
    public long CollaboratorId { get; set; }
    public long ProjectId { get; set; }
    public PeriodDate PeriodDate { get; set; }

    public AssociationProjectCollaborator(long collaboratorId, long projectId, PeriodDate periodDate)
    {
        CollaboratorId = collaboratorId;
        ProjectId = projectId;
        PeriodDate = periodDate;
    }

    public void SetId(long id)
    {
        Id = id;
    }

    public long GetId()
    {
        return Id;
    }

    public long GetCollaboratorId()
    {
        return CollaboratorId;
    }

    public long GetProjectId()
    {
        return ProjectId;
    }

    public bool AssociationIntersectPeriod(PeriodDate periodDate)
    {
        return PeriodDate.Intersects(periodDate);
    }
}
