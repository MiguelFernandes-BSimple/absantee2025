namespace Domain;

public class AssociationProjectColaboratorRepository : IAssociationProjectColaboratorRepository
{
    private List<IAssociationProjectColaborator> _associationsProjectColaborator;
    public IEnumerable<IColaborator> FindAllProjectCollaborators(IProject project)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IColaborator> FindAllProjectCollaboratorsIn(IProject project, DateOnly InitDate, DateOnly FinalDate)
    {
        throw new NotImplementedException();
    }
}