namespace Domain;

public interface IAssociationProjectColaboratorRepository{
    public IEnumerable<IColaborator> FindAllProjectCollaborators(IProject project);
    public IEnumerable<IColaborator> FindAllProjectCollaboratorsBetween(IProject project, DateOnly InitDate, DateOnly FinalDate);
}