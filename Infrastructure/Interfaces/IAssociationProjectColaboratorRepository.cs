using Domain.Interfaces;

namespace Infrastructure.Interfaces;

public interface IAssociationProjectCollaboratorRepository
{
    public IEnumerable<IAssociationProjectCollaborator> FindAllByProject(IProject project);
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAsync(IProject project);
    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(IProject project, ICollaborator collaborator);
    public Task<IAssociationProjectCollaborator?> FindByProjectAndCollaboratorAsync(IProject project, ICollaborator collaborator);

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriod(IProject project, IPeriodDate periodDate);
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndBetweenPeriodAsync(IProject project, IPeriodDate periodDate);
}
