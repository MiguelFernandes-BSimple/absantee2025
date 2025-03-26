namespace Domain;

public class AssociationProjectColaboratorRepository : IAssociationProjectColaboratorRepository
{
    private List<IAssociationProjectColaborator> _associationsProjectColaborator;

    public AssociationProjectColaboratorRepository(List<IAssociationProjectColaborator> associationsProjectColaborator)
    {
        _associationsProjectColaborator = associationsProjectColaborator;
    }

    public IEnumerable<IColaborator> FindAllProjectCollaborators(IProject project)
    {
        return _associationsProjectColaborator.Where(a => a.HasProject(project)).Select(a => a.GetColaborator());
    }

    public IEnumerable<IColaborator> FindAllProjectCollaboratorsBetween(IProject project, DateOnly InitDate, DateOnly FinalDate)
    {
        return _associationsProjectColaborator
                .Where(a => a.HasProject(project)
                    && a.AssociationIntersectDates(InitDate, FinalDate))
                .Select(a => a.GetColaborator());
    }
}