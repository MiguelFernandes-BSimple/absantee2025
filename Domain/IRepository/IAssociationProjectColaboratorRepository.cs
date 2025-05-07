using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IAssociationProjectCollaboratorRepository : IGenericRepository<AssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>
{
    public Task<IEnumerable<AssociationProjectCollaborator>> FindAllByProjectAsync(Guid projectId);
    public Task<AssociationProjectCollaborator?> FindByProjectAndCollaboratorAsync(Guid projectId, Guid collaboratorId);
    public Task<IEnumerable<AssociationProjectCollaborator>> FindAllByProjectAndCollaboratorAsync(Guid projectId, Guid collaboratorId);
    public Task<IEnumerable<AssociationProjectCollaborator>> FindAllByProjectAndIntersectingPeriodAsync(Guid projectId, PeriodDate periodDate);
    Task<IEnumerable<AssociationProjectCollaborator>> FindAllByProjectAndCollaboratorAndBetweenPeriodAsync(Guid projectId, Guid collaboratorId, PeriodDate periodDate);
    public Task<bool> CanInsert(PeriodDate periodDate, Guid collaboratorId, Guid projectId);
}
