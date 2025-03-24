namespace Domain;

public interface IAssociationProjectColaboratorRepository{
    public IEnumerable<IColaborator> FindAllProjectCollaborators(IProject project);
    public IEnumerable<IColaborator> FindAllProjectCollaboratorsIn(IProject project, DateOnly InitDate, DateOnly FinalDate);
}