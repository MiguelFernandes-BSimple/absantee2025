namespace Domain.Interfaces;

public interface IAssociationProjectCollaborator
{
    public long GetId();
    public long GetCollaboratorId();
    public long GetProjectId();
    public IPeriodDate GetPeriodDate();
    public bool AssociationIntersectPeriod(IPeriodDate periodDate);
}
