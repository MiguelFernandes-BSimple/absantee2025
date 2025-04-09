using Domain.Interfaces;

namespace Domain.IRepository;

public interface IAssociationProjectCollaboratorRepository : IGenericRepository<IAssociationProjectCollaborator>
{
    public IEnumerable<IAssociationProjectCollaborator> FindAllByProject(IProject project);
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAsync(IProject project);
    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(IProject project, ICollaborator collaborator);
    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(long projectId, long collaboratorId);
    public Task<IAssociationProjectCollaborator?> FindByProjectAndCollaboratorAsync(IProject project, ICollaborator collaborator);
    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriod(IProject project, IPeriodDate periodDate);
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndBetweenPeriodAsync(IProject project, IPeriodDate periodDate);
    public Task<bool> AddAsync(IAssociationProjectCollaborator newAssociation);
}
