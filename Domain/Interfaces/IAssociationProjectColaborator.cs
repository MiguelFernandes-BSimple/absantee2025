using Domain.Models;

namespace Domain.Interfaces;

public interface IAssociationProjectCollaborator
{
    public long Id { get; set; }
    public long CollaboratorId { get; set; }
    public long ProjectId { get; set; }
    public PeriodDate PeriodDate { get; set; }
    public long GetId();
    public long GetCollaboratorId();
    public long GetProjectId();
    public bool AssociationIntersectPeriod(PeriodDate periodDate);
}
