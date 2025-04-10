using Domain.Interfaces;

namespace Domain.IRepository;

public interface IAssociationProjectCollaboratorRepository : IGenericRepository<IAssociationProjectCollaborator>
{
    public Task<bool> AddAsync(IAssociationProjectCollaborator newAssociation);
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAsync(long projectId);
    public Task<IAssociationProjectCollaborator?> FindByProjectAndCollaboratorAsync(long projectId, long collaboratorId);
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndBetweenPeriodAsync(long projectId, IPeriodDate periodDate);
}
