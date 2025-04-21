namespace Domain.Interfaces;

public interface IAssociationTrainingModuleCollaborator
{
    public long Id { get; }
    public long TrainingModuleId { get; }
    public long CollaboratorId { get; }
}