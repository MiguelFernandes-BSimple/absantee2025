namespace Domain;

public interface IAssociationProjectColaborator
{
    public IColaborator GetCollaborator();
    public DateOnly GetInitDate();
    public DateOnly GetFinalDate();



}
