using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IAssociationProjectCollaboratorRepository : IGenericRepository<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>
{
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAsync(long projectId);
    public Task<IAssociationProjectCollaborator?> FindByProjectAndCollaboratorAsync(long projectId, long collaboratorId);
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndCollaboratorAsync(long projectId, long collaboratorId);
    public Task<IEnumerable<IAssociationProjectCollaborator>> FindAllByProjectAndBetweenPeriodAsync(long projectId, PeriodDate periodDate);
    public Task<bool> CanInsert(PeriodDate periodDate, long collaboratorId, long projectId);
} 
