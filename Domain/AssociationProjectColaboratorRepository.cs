namespace Domain;

public class AssociationProjectCollaboratorRepository : IAssociationProjectCollaboratorRepository
{
    private List<IAssociationProjectCollaborator> _associationsProjectCollaborator;

    public AssociationProjectCollaboratorRepository(List<IAssociationProjectCollaborator> associationsProjectCollaborator)
    {
        _associationsProjectCollaborator = associationsProjectCollaborator;
    }

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProject(IProject project)
    {
        return _associationsProjectCollaborator.Where(a => a.HasProject(project));
    }
    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(IProject project, ICollaborator collaborator)
    {
        return _associationsProjectCollaborator.Where(a => a.HasProject(project) && a.HasCollaborator(collaborator)).FirstOrDefault();
    }

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriod(IProject project, DateOnly InitDate, DateOnly FinalDate)
    {
        return _associationsProjectCollaborator
                .Where(a => a.HasProject(project)
                    && a.AssociationIntersectDates(InitDate, FinalDate));
    }

    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(IProject project, ICollaborator collaborator)
    {
        return _associationsProjectCollaborator.Where(a => a.HasProject(project) && a.HasCollaborator(collaborator)).FirstOrDefault();

    }

    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(IProject project, ICollaborator collaborator)
    {
        return _associationsProjectCollaborator.Where(a => a.HasProject(project) && a.HasCollaborator(collaborator)).FirstOrDefault();

    }
}
