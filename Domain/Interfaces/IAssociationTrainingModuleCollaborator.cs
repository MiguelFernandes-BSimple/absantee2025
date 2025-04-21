namespace Domain.Interfaces;

public class IAssociationTrainingModuleCollaborator
{
    public long Id { get; }
    public long TrainingModuleId { get; }
    public long CollaboratorId { get; }
}