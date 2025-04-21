using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class AssociationTrainingModuleCollaboratorFactory : IAssociationTrainingModuleCollaboratorFactory
{
    public AssociationTrainingModuleCollaboratorFactory()
    {

    }
    public AssociationTrainingModuleCollaborator Create(long trainingModuleId, long collaboratorId)
    {
        throw new NotImplementedException();
    }

    public AssociationTrainingModuleCollaborator Create(IAssociationTrainingModuleCollaboratorVisitor associationTrainingModuleCollaboratorVisitor)
    {
        throw new NotImplementedException();
    }
}