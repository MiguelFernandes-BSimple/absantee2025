using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface IAssociationTrainingModuleCollaboratorFactory
{
    public AssociationTrainingModuleCollaborator Create(long trainingModuleId, long collaboratorId);
    public AssociationTrainingModuleCollaborator Create(IAssociationTrainingModuleCollaboratorVisitor associationTrainingModuleCollaboratorVisitor);
}