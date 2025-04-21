using Domain.Interfaces;

namespace Domain.Models;

public class AssociationTrainingModuleCollaborator : IAssociationTrainingModuleCollaborator
{
    public long Id { get; set; }
    public long CollaboratorId { get; set; }
    public long TrainingModuleId { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public AssociationTrainingModuleCollaborator(long collaboratorId, long trainingModuleId, PeriodDateTime periodDateTime)
    {
        CollaboratorId = collaboratorId;
        TrainingModuleId = trainingModuleId;
        PeriodDateTime = periodDateTime;
    }
}
