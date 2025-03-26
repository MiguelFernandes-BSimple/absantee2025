namespace Domain;

public interface IAssociationProjectColaborator
{
    public IColaborator GetColaborator();
    public DateOnly GetInitDate();
    public DateOnly GetFinalDate();
    public bool HasProject(IProject project);
    public bool AssociationIntersectDates(DateOnly initDate, DateOnly finalDate);
}
