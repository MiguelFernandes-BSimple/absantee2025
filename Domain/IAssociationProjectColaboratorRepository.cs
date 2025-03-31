namespace Domain;

public interface IAssociationProjectCollaboratorRepository
{
    public IEnumerable<ICollaborator> FindAllProjectCollaborators(IProject project);
    public IEnumerable<ICollaborator> FindAllProjectCollaboratorsBetween(IProject project, DateOnly InitDate, DateOnly FinalDate);
    public IEnumerable<IAssociationProjectCollaborator> FindAllByProject(IProject project);
    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriod(IProject project, DateOnly InitDate, DateOnly FinalDate);
}
