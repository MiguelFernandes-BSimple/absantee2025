using Domain.Models;

namespace Domain.Interfaces;

public interface IAssociationProjectCollaborator
{
    public ICollaborator GetCollaborator();
    public IProject GetProject();
    public IPeriodDate GetPeriodDate();
    public bool HasProject(IProject project);
    public bool HasCollaborator(ICollaborator collaborator);
    public bool AssociationIntersectPeriod(IPeriodDate periodDate);
}
