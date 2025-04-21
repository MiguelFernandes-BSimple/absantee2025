using Domain.Interfaces;

namespace Domain.Models;

public class AssociationTrainingModuleCollaborator : IAssociationTrainingModuleCollaborator
{
    public long Id { get; set; }
    public long TrainingModuleId { get; set; }
    public long CollaboratorId { get; set; }

    public AssociationTrainingModuleCollaborator(long trainingModuleId, long collaboratorId)
    {
        TrainingModuleId = trainingModuleId;
        CollaboratorId = collaboratorId;
    }
}