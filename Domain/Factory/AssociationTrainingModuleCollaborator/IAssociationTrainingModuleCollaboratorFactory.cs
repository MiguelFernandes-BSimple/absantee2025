using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface IAssociationTrainingModuleCollaboratorFactory
{
    public Task<AssociationTrainingModuleCollaborator> Create(long collaboratorId, long trainingModuleId, PeriodDateTime periodDateTime);

    public AssociationTrainingModuleCollaborator Create(IAssociationTrainingModuleCollaboratorVisitor assocVisitor);

}