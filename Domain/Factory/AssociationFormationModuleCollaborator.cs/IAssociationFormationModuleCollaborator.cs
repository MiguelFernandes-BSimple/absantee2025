using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface IAssociationFormationModuleCollaboratorFactory
{
    public Task<AssociationFormationModuleCollaborator> Create(long collaboratorId, long formationModuleId, PeriodDate periodDate);
    public AssociationFormationModuleCollaborator Create(IAssociationFormationModuleCollaboratorVisitor associationModuleCollaboratorVisitor);
}