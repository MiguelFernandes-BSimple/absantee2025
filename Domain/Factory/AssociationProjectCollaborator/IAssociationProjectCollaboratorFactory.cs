using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface IAssociationProjectCollaboratorFactory
{
    public Task<AssociationProjectCollaborator> Create(PeriodDate periodDate, long collaboratorId, long projectId);

    public AssociationProjectCollaborator Create(IAssociationProjectCollaboratorVisitor associationProjectCollaboratorVisitor);
}