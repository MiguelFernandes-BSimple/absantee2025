namespace Domain;

public interface IAssociationProjectColaborator
{
    IColaborator GetColaborator();
    IProject GetProject();
    public bool HasProject(IProject project);
    public bool AssociationIntersectDates(DateOnly initDate, DateOnly finalDate);
}
