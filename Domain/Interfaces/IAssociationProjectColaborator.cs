using Domain.Models;

namespace Domain.Interfaces;

public interface IAssociationProjectCollaborator
{
    public long GetId();
    public long GetCollaboratorId();
    public long GetProjectId();
    public ICollaborator GetCollaborator();
    public IPeriodDate GetPeriodDate();
    public bool HasProject(IProject project);
    public bool HasCollaborator(ICollaborator collaborator);
    public bool AssociationIntersectPeriod(IPeriodDate periodDate);
}
