using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface IAssociationProjectCollaboratorFactory
{
    public Task<AssociationProjectCollaborator> Create(IPeriodDate periodDate, long collaboratorId, long projectId);

    public AssociationProjectCollaborator Create(IAssociationProjectCollaboratorVisitor associationProjectCollaboratorVisitor);
}