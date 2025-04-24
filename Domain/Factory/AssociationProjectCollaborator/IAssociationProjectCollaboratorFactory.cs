using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface IAssociationProjectCollaboratorFactory
{
    public Task<AssociationProjectCollaborator> Create(PeriodDate periodDate, Guid collaboratorId, Guid projectId);

    public AssociationProjectCollaborator Create(IAssociationProjectCollaboratorVisitor associationProjectCollaboratorVisitor);
}