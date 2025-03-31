namespace Domain;

public interface IAssociationProjectCollaboratorRepository
{
    public IEnumerable<IAssociationProjectCollaborator> FindAllByProject(IProject project);
    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriod(IProject project, DateOnly InitDate, DateOnly FinalDate);
}
