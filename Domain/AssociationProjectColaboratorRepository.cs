namespace Domain;

public class AssociationProjectCollaboratorRepository : IAssociationProjectCollaboratorRepository
{
    private List<IAssociationProjectCollaborator> _associationsProjectCollaborator;

    public AssociationProjectCollaboratorRepository(List<IAssociationProjectCollaborator> associationsProjectCollaborator)
    {
        _associationsProjectCollaborator = associationsProjectCollaborator;
    }

    public IEnumerable<ICollaborator> FindAllProjectCollaborators(IProject project)
    {
        return _associationsProjectCollaborator.Where(a => a.HasProject(project)).Select(a => a.GetCollaborator());
    }

    public IEnumerable<ICollaborator> FindAllProjectCollaboratorsBetween(IProject project, DateOnly InitDate, DateOnly FinalDate)
    {
        return _associationsProjectCollaborator
                .Where(a => a.HasProject(project)
                    && a.AssociationIntersectDates(InitDate, FinalDate))
                .Select(a => a.GetCollaborator());
    }

    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(IProject project, ICollaborator collaborator)
    {
        return _associationsProjectCollaborator.Where(a => a.HasProject(project) && a.HasCollaborator(collaborator)).FirstOrDefault();

    }
}
