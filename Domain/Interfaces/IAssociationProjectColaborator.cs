using Domain.Models;

namespace Domain.Interfaces;

public interface IAssociationProjectCollaborator
{
    public long GetId();
    public long GetCollaboratorId();
    public long GetProjectId();
    public PeriodDate _periodDate { get; set; }
    public bool AssociationIntersectPeriod(PeriodDate periodDate);
}
