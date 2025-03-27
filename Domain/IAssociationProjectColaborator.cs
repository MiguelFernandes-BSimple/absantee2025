namespace Domain;

public interface IAssociationProjectCollaborator
{
    public ICollaborator GetCollaborator();
    public DateOnly GetInitDate();
    public DateOnly GetFinalDate();
    public bool HasProject(IProject project);
    public bool AssociationIntersectDates(DateOnly initDate, DateOnly finalDate);
}
