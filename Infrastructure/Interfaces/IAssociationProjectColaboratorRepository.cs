using Domain.Interfaces;

namespace Infrastructure.Interfaces;

public interface IAssociationProjectCollaboratorRepository
{
    public IEnumerable<IAssociationProjectCollaborator> FindAllByProject(IProject project);
    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAsync(IProject project);
    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(IProject project, ICollaborator collaborator);
    public IAssociationProjectCollaborator? FindByProjectAndCollaboratorAsync(IProject project, ICollaborator collaborator);

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriod(IProject project, IPeriodDate periodDate);
    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriodAsync(IProject project, IPeriodDate periodDate);
}
