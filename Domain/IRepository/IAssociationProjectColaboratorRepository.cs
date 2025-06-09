using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IAssociationProjectCollaboratorRepository : IGenericRepositoryEF<IAssociationProjectCollaborator, AssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>
{
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByCollaboratorAsync(Guid collabId);
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAsync(Guid projectId);
    public Task<IAssociationProjectCollaborator?> FindByProjectAndCollaboratorAsync(Guid projectId, Guid collaboratorId);
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndCollaboratorAsync(Guid projectId, Guid collaboratorId);
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndIntersectingPeriodAsync(Guid projectId, PeriodDate periodDate);
    Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndCollaboratorAndBetweenPeriodAsync(Guid projectId, Guid collaboratorId, PeriodDate periodDate);
    public Task<bool> CanInsert(PeriodDate periodDate, Guid collaboratorId, Guid projectId);
}
