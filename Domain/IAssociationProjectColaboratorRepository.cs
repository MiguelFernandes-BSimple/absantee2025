namespace Domain;

public interface IAssociationProjectCollaboratorRepository
{
    public IEnumerable<IAssociationProjectCollaborator> FindAllByProject(IProject project);
    public IAssociationProjectCollaborator? FindByProjectandCollaborator(IProject project, ICollaborator collaborator);

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriod(IProject project, DateOnly InitDate, DateOnly FinalDate);
}
