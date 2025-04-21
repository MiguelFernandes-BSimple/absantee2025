using Domain.Models;

namespace Domain.Factory;

public class AssociationTrainingModuleCollaboratorFactory : IAssociationTrainingModuleCollaboratorFactory
{
    public AssociationTrainingModuleCollaboratorFactory() {

    }
    
    public async Task<AssociationTrainingModuleCollaborator> Create(long collabId, long moduleId)
    {
        AssociationTrainingModuleCollaborator amc = new AssociationTrainingModuleCollaborator(collabId, moduleId);

        return amc;
    }

    /*public AssociationTrainingModuleCollaborator Create(IAssociationTrainingModuleCollaboratorVisitor visitor) {
        return new AssociationTrainingModuleCollaborator(visitor.id, visitor._collaboratorId, visitor._trainingModuleId)
    }*/
}
