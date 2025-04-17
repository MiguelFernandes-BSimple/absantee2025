using Domain.Models;

namespace Domain.Interfaces;

public interface IAssociationProjectCollaborator
{
    public long _id { get; set; }
    public long _collaboratorId { get; set; }
    public long _projectId { get; set; }
    public PeriodDate _periodDate { get; set; }
    public long GetId();
    public long GetCollaboratorId();
    public long GetProjectId();
    public bool AssociationIntersectPeriod(PeriodDate periodDate);
}
