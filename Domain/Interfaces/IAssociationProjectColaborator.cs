namespace Domain.Interfaces;

public interface IAssociationProjectCollaborator
{
    public ICollaborator GetCollaborator();
    public IProject GetProject();
    public DateOnly GetInitDate();
    public DateOnly GetFinalDate();
    public bool HasProject(IProject project);
    public bool HasCollaborator(ICollaborator collaborator);
    public bool AssociationIntersectDates(DateOnly initDate, DateOnly finalDate);
}
